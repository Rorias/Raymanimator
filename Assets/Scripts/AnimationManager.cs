using System;
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

    private GameManager gameManager = GameManager.Instance;
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

    public void SaveToBinary(Animation _anim, bool _animData, bool _visuals, bool _colls, float _pixelSize)
    {
        Rayman1MSDOS.DesignObjects currObject = (Rayman1MSDOS.DesignObjects)Enum.Parse(typeof(Rayman1MSDOS.DesignObjects), _anim.usedSpriteset);
        Rayman1BinaryAnimation rayBinary = Rayman1BinaryAnimation.Instance;
        rayBinary.SaveRaymAnimationToBinary(
            _anim, gameManager.spritesetImages,
            (int)currObject,
            _anim.binaryAnimationIndex,
            _animData,
            _visuals,
            _colls,
            _pixelSize);
        DebugHelper.Log(_anim.animationName + " saved to binary!");
    }

    public void ImportFrames(Animation _import, Animation _current, int _startFrame, int _endFrame, int _insertFrame)
    {
        int offset = 0;
        for (int i = _startFrame; i < _endFrame; i++)
        {
            int newMax = _current.maxFrameCount + 1;
            _current.maxFrameCount = Math.Min(Math.Max(newMax, 1), 9999);

            Frame n = new Frame(_import.frames[i]) { frameID = _insertFrame + offset };

            for (int p = 0; p < n.frameParts.Count; p++)
            {
                if (n.frameParts[p].partIndex > -1)
                {
                    n.frameParts[p].part = gameManager.spritesetImages[n.frameParts[p].partIndex];
                }
            }

            _current.frames.Insert(_insertFrame + offset, n);
            offset++;
        }

        for (int i = _insertFrame + (_endFrame - _startFrame); i < _current.maxFrameCount; i++)
        {
            _current.frames[i].frameID = i;
        }
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
