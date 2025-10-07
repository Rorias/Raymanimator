using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

using UnityEngine;

public class Frame
{
    public Frame() { }
    public Frame(Frame f)
    {
        frameID = f.frameID;
        frameParts = new List<Part>();

        foreach (Part p in f.frameParts)
        {
            frameParts.Add(new Part(p));
        }
    }

    public int frameID { get; set; }
    public List<Part> frameParts = new List<Part>();

    public const string xFrame = "Frame";

    public const string xFrameID = "FrameID";
    public const string xParts = "Parts";

    public XElement Save()
    {
        XElement xframe = new XElement(xFrame);
        xframe.Add(new XAttribute("ID", frameID));

        XElement xparts = new XElement(xParts);

        for (int i = 0; i < frameParts.Count; i++)
        {
            if (frameParts[i] != null)
            {
                xparts.Add(frameParts[i].Save());
            }
        }
        xframe.Add(xparts);

        return xframe;
    }

    public void Load(XElement _model)
    {
        if (_model.FirstAttribute != null)
        {
            frameID = Convert.ToInt32(_model.FirstAttribute.Value);
        }
        else
        {
            Debug.Log("No FrameID found for animation frame.");
            return;
        }

        XElement xparts = _model.XPathSelectElement("./" + xParts);

        foreach (XElement xpart in xparts.XPathSelectElements("./Part"))
        {
            int partID = Convert.ToInt32(xpart.FirstAttribute.Value);

            Part part = frameParts.Find(x => x.partID == partID);

            if (part == null)
            {
                part = new Part();
                frameParts.Add(part);
            }
            part.Load(xpart);
        }
    }
}
