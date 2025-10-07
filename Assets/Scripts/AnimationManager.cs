using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

public sealed class AnimationManager
{
    #region Singleton
    private static AnimationManager _instance;
    private static object _lock = new object();

    private AnimationManager() { }

    public static AnimationManager Instance
    {
        get
        {
            if (null == _instance)
            {
                lock (_lock)
                {
                    if (null == _instance)
                    {
                        _instance = new AnimationManager();
                    }
                }
            }
            return _instance;
        }
    }
    #endregion

    private GameSettings settings = GameSettings.Instance;

    private XDocument model;
    private XElement XModel;

    public void SaveFile(Animation _anim)
    {
        SaveAnimationXMLFile(settings.animationsPath + "\\" + _anim.animationName, _anim);
        SaveAnimationCsFile(settings.animationsPath + "\\" + _anim.animationName, _anim);
        DebugHelper.Log(_anim.animationName + " saved!");
    }

    public void SaveFileReversed(Animation _anim)
    {
        List<Frame> copyFrames = new List<Frame>(_anim.frames);
        List<Frame> newFrames = new List<Frame>();

        for (int i = copyFrames.Count - 1; i >= 0; i--)
        {
            Frame n = new Frame(copyFrames[i]);
            n.frameID = _anim.maxFrameCount - 1 - copyFrames[i].frameID;
            newFrames.Add(n);
        }

        _anim.frames = newFrames;
        _anim.animationName = _anim.animationName + "Reverse";
        SaveFile(_anim);
        DebugHelper.Log(_anim.animationName + " saved as reverse!");
        _anim.frames = copyFrames;
        _anim.animationName = _anim.animationName.Replace("Reverse", "");
    }

    public void SaveFileDoubled(Animation _anim)
    {
        List<Frame> copyFrames = new List<Frame>(_anim.frames);
        List<Frame> newFrames = new List<Frame>();
        for (int i = 0; i < copyFrames.Count; i++)
        {
            Frame n = new Frame(copyFrames[i]);
            newFrames.Add(n);
            newFrames[^1].frameID = newFrames.Count - 1;

            n = new Frame(copyFrames[i]);
            n.frameID = newFrames.Count;
            newFrames.Add(n);
        }

        _anim.frames = newFrames;
        _anim.maxFrameCount = _anim.maxFrameCount * 2;
        _anim.animationName = _anim.animationName + "Double";
        SaveFile(_anim);
        DebugHelper.Log(_anim.animationName + " saved as double!");
        _anim.frames = copyFrames;
        _anim.maxFrameCount = _anim.maxFrameCount / 2;
        _anim.animationName = _anim.animationName.Replace("Double", "");
    }

    private void SaveAnimationXMLFile(string _path, Animation _anim)
    {
        XModel = _anim.Save();
        model = XDocument.Parse(XModel.ToString());
        model.Save(_path + ".xml");
    }

    /// <summary>
    /// Export as .cs file readable by my rayman fangame
    /// </summary>
    private void SaveAnimationCsFile(string _path, Animation _anim)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("public sealed class " + _anim.animationName + "\n{\n");

        sb.Append("#region Singleton\n");
        sb.Append("private static " + _anim.animationName + " _instance;\n");
        sb.Append("private static object _lock = new object();\n\n");

        sb.Append("private " + _anim.animationName + "() { }\n\n");

        sb.Append("public static " + _anim.animationName + " Instance\n{\n");
        sb.Append("get\n{\nif (null == _instance)\n{\nlock (_lock)\n{\nif (null == _instance)\n{\n_instance = new " + _anim.animationName + "();\n}\n}\n}\nreturn _instance;\n}\n}\n");
        sb.Append("#endregion\n\n");

        sb.Append("public AnimationStateMachine asm;\n\n");

        sb.Append("public void " + _anim.animationName + "(int subFrame)\n{\n");
        sb.Append("switch(subFrame)\n{\n");

        for (int frame = 0; frame < _anim.maxFrameCount; frame++)
        {
            sb.Append("case " + frame + ":\n");
            for (int framepart = 0; framepart < _anim.maxPartCount; framepart++)
            {
                sb.Append("asm.SetPart(" + framepart + ", " + _anim.frames[frame].frameParts[framepart].partIndex + ", " + _anim.frames[frame].frameParts[framepart].xPos + "f, " + _anim.frames[frame].frameParts[framepart].yPos + "f, " + _anim.frames[frame].frameParts[framepart].flipX.ToString().ToLower() + ");\n");
            }
            sb.Append("break;\n");
        }
        sb.Append("}\n");
        sb.Append("}\n");
        sb.Append("}");

        string animationInfo = sb.ToString();

        File.WriteAllText(_path + ".cs", animationInfo);
    }

    public bool LoadAnimation(Animation _a)
    {
        if (File.Exists(settings.animationsPath + "\\" + _a.animationName + ".xml"))
        {
            model = XDocument.Load(settings.animationsPath + "\\" + _a.animationName + ".xml");
            XModel = model.Root;
            if (!_a.Load(XModel))
            {
                return false;
            }
        }

        return true;
    }
}
