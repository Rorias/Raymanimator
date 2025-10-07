using System;
using System.Xml.Linq;
using System.Xml.XPath;

using UnityEngine;

public class Part
{
    private GameManager gameManager;
    public Part() { gameManager = GameManager.Instance; }
    public Part(Part p)
    {
        partID = p.partID;
        partIndex = p.partIndex;
        xPos = p.xPos;
        yPos = p.yPos;
        flipX = p.flipX;
        flipY = p.flipY;
    }
    public Sprite part { get; set; }

    public int partID { get; set; }
    public int partIndex { get; set; }
    public float xPos { get; set; }
    public float yPos { get; set; }
    public bool flipX { get; set; }
    public bool flipY { get; set; }

    public const string xPart = "Part";

    public const string xPartID = "PartID";
    public const string xPartIndex = "PartIndex";
    public const string xXpos = "Xpos";
    public const string xYpos = "Ypos";
    public const string xFlipX = "FlipX";
    public const string xFlipY = "FlipY";

    public XElement Save()
    {
        XElement xpart = new XElement(xPart);
        xpart.Add(new XAttribute("ID", partID));

        if (part != null || partIndex >= 0)
        {
            xpart.Add(new XElement(xPartIndex, partIndex));
        }
        else
        {
            xpart.Add(new XElement(xPartIndex, -1));
        }

        xpart.Add(new XElement(xXpos, xPos));
        xpart.Add(new XElement(xYpos, yPos));
        xpart.Add(new XElement(xFlipX, flipX));
        xpart.Add(new XElement(xFlipY, flipY));

        return xpart;
    }

    public void Load(XElement _model)
    {
        if (_model.FirstAttribute != null)
        {
            partID = Convert.ToInt32(_model.FirstAttribute.Value);
        }
        else
        {
            Debug.Log("No PartID found for frame part.");
            return;
        }

        if (_model.XPathSelectElement("./" + xPartIndex) != null)
        {
            partIndex = Convert.ToInt32(_model.XPathSelectElement("./" + xPartIndex).Value);
        }
        else
        {
            Debug.Log("PartIndex does not exist. Set to 0.");
            partIndex = 0;
        }

        if (_model.XPathSelectElement("./" + xXpos) != null)
        {
            xPos = gameManager.ParseToSingle(_model.XPathSelectElement("./" + xXpos).Value);
        }
        else
        {
            Debug.Log("XPos does not exist. Set to 0.");
            xPos = 0;
        }

        if (_model.XPathSelectElement("./" + xYpos) != null)
        {
            yPos = gameManager.ParseToSingle(_model.XPathSelectElement("./" + xYpos).Value);
        }
        else
        {
            Debug.Log("YPos does not exist. Set to 0.");
            yPos = 0;
        }

        if (_model.XPathSelectElement("./" + xFlipX) != null)
        {
            flipX = Convert.ToBoolean(_model.XPathSelectElement("./" + xFlipX).Value);
        }
        else
        {
            Debug.Log("FlipX does not exist. Set to false.");
            flipX = false;
        }

        if (_model.XPathSelectElement("./" + xFlipY) != null)
        {
            flipY = Convert.ToBoolean(_model.XPathSelectElement("./" + xFlipY).Value);
        }
        else
        {
            Debug.Log("FlipY does not exist. Set to false.");
            flipY = false;
        }
    }
}
