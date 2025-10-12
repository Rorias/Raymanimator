using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

using TMPro;

using UnityEngine;

public class Animation
{
    public Animation() { }

    public string animationName { get; set; }
    public int maxFrameCount { get; set; }
    public int maxPartCount { get; set; }
    public int gridSizeX { get; set; }
    public int gridSizeY { get; set; }
    public string usedSpriteset { get; set; }

    public List<Frame> frames = new List<Frame>();

    private const string xAnimation = "Animation";

    private const string xAnimName = "AnimationName";
    private const string xFrameCount = "FrameCount";
    private const string xPartCount = "PartCount";
    private const string xXSize = "Xsize";
    private const string xYSize = "Ysize";
    private const string xUsedSpriteset = "UsedSpriteset";
    private const string xFrames = "Frames";

    public void SetMaxPartCount(TMP_InputField _if)
    {
        int.TryParse(_if.text, out int conv);
        maxPartCount = Mathf.Min(Mathf.Max(conv, 1), 99);
        _if.text = maxPartCount.ToString();
    }

    public void SetMaxFrameCount(TMP_InputField _if)
    {
        int.TryParse(_if.text, out int conv);
        maxFrameCount = Mathf.Min(Mathf.Max(conv, 1), 9999);
        _if.text = maxFrameCount.ToString();
    }

    public XElement Save()
    {
        XElement xanimation = new XElement(xAnimation);

        xanimation.Add(new XElement(xAnimName, animationName));
        xanimation.Add(new XElement(xFrameCount, maxFrameCount));
        xanimation.Add(new XElement(xPartCount, maxPartCount));
        xanimation.Add(new XElement(xXSize, gridSizeX));
        xanimation.Add(new XElement(xYSize, gridSizeY));
        xanimation.Add(new XElement(xUsedSpriteset, usedSpriteset));

        XElement xframes = new XElement(xFrames);

        for (int i = 0; i < frames.Count; i++)
        {
            if (null != frames[i])
            {
                xframes.Add(frames[i].Save());
            }
        }
        xanimation.Add(xframes);

        return xanimation;
    }

    public bool Load(XElement _model)
    {
        if (_model.XPathSelectElement("./" + xAnimName) == null)
        {
            Debug.Log("This XML file is not a PixelAnimation file!");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(_model.XPathSelectElement("./" + xAnimName).Value))
        {
            animationName = _model.XPathSelectElement("./" + xAnimName).Value;
        }
        else
        {
            Debug.Log("The name for this animation is corrupt.");
            return false;
        }

        if (_model.XPathSelectElement("./" + xFrameCount) != null)
        {
            int loadedFrames = Convert.ToInt32(_model.XPathSelectElement("./" + xFrameCount).Value);
            maxFrameCount = Mathf.Min(Mathf.Max(loadedFrames, 1), 999);
        }
        else
        {
            Debug.Log("Framecount does not exist. Set to 1.");
            maxFrameCount = 1;
        }

        if (_model.XPathSelectElement("./" + xPartCount) != null)
        {
            int loadedParts = Convert.ToInt32(_model.XPathSelectElement("./" + xPartCount).Value);
            maxPartCount = Mathf.Min(Mathf.Max(loadedParts, 1), 99);
        }
        else
        {
            Debug.Log("Partcount does not exist. Set to 1.");
            maxPartCount = 1;
        }

        if (_model.XPathSelectElement("./" + xXSize) != null)
        {
            int loadedGridSizeX = Convert.ToInt32(_model.XPathSelectElement("./" + xXSize).Value);
            gridSizeX = Mathf.Min(Mathf.Max(loadedGridSizeX, 1), 2047);
        }
        else
        {
            Debug.Log("XSize does not exist. Set to 1.");
            gridSizeX = 1;
        }

        if (_model.XPathSelectElement("./" + xYSize) != null)
        {
            int loadedGridSizeY = Convert.ToInt32(_model.XPathSelectElement("./" + xYSize).Value);
            gridSizeY = Mathf.Min(Mathf.Max(loadedGridSizeY, 1), 2047);
        }
        else
        {
            Debug.Log("YSize does not exist. Set to 1.");
            gridSizeY = 1;
        }

        if (_model.XPathSelectElement("./" + xUsedSpriteset) != null)
        {
            usedSpriteset = _model.XPathSelectElement("./" + xUsedSpriteset).Value;
        }
        else
        {
            Debug.Log("UsedSpriteset does not exist. Set to none.");
            usedSpriteset = "none";
        }

        if (_model.XPathSelectElement("./" + xFrames) == null)
        {
            Debug.Log("There were no frames found in the animation file!");
            return false;
        }

        XElement xframes = _model.XPathSelectElement("./" + xFrames);

        foreach (XElement xFrame in xframes.XPathSelectElements("./Frame"))
        {
            if (xFrame != null)
            {
                int frameID;

                if (xFrame.FirstAttribute != null)
                {
                    frameID = Convert.ToInt32(xFrame.FirstAttribute.Value);
                }
                else
                {
                    Debug.Log("No FrameID found for animation frame.");
                    return false;
                }

                Frame frame = frames.Find(x => (x == null) ? true : x.frameID == frameID);

                if (frame == null)
                {
                    frame = new Frame();
                    frames.Add(frame);
                }

                frame.Load(xFrame);
            }
        }
        Debug.Log("Finished animation load function.");
        return true;
    }
}
