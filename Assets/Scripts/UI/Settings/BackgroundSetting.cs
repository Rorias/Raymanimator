using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class BackgroundSettings : Settings
{
    public TMP_InputField backgroundImagePathIF;
    public TMP_Dropdown backgroundsDD;

    public ColorSlider colorSlider;

    private Camera cam;

    protected override void Awake()
    {
        base.Awake();

        if (backgroundImagePathIF != null)
        {
            backgroundImagePathIF.onValueChanged.AddListener(delegate { SetBackgroundPathViaBrowse(); });
            backgroundImagePathIF.onEndEdit.AddListener(delegate { SetBackgroundPath(); });
        }

        if (backgroundsDD != null)
        {
            backgroundsDD.onValueChanged.AddListener(delegate { SetCurrentBackground(); });
        }

        colorSlider.UpdateRed += SetColorRed;
        colorSlider.UpdateGreen += BGColorGreen;
        colorSlider.UpdateBlue += BGColorBlue;

        colorSlider.SetRed += SaveColor;
        colorSlider.SetGreen += SaveColor;
        colorSlider.SetBlue += SaveColor;

        colorSlider.Reset += ResetColor;

        cam = Camera.main;
    }

    private void Start()
    {
        colorSlider.SetColorSliders(settings.bgColor);
    }

    public void SetBackgroundPathViaBrowse()
    {

    }

    public void SetBackgroundPath()
    {

    }

    public void SetCurrentBackground()
    {

    }

    public void SetColorRed()
    {
        cam.backgroundColor = colorSlider.GetColorBarRed(cam.backgroundColor);
    }

    public void BGColorGreen()
    {
        cam.backgroundColor = colorSlider.GetColorBarGreen(cam.backgroundColor);
    }

    public void BGColorBlue()
    {
        cam.backgroundColor = colorSlider.GetColorBarBlue(cam.backgroundColor);
    }

    public void SaveColor()
    {
        settings.bgColor = cam.backgroundColor;
        settings.SaveSettings();
    }

    public void ResetColor()
    {
        cam.backgroundColor = new Color(0.196f, 0.294f, 0.627f, 1);
        colorSlider.SetColorSliders(cam.backgroundColor);
        SaveColor();
    }
}
