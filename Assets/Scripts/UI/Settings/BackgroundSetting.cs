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

        colorSlider.SetRed += BGColorRed;
        colorSlider.SetGreen += BGColorGreen;
        colorSlider.SetBlue += BGColorBlue;
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

    public void BGColorRed()
    {
        cam.backgroundColor = colorSlider.GetColorBarRed(cam.backgroundColor);
        settings.bgColor = cam.backgroundColor;
        settings.SaveSettings();
    }

    public void BGColorGreen()
    {
        cam.backgroundColor = colorSlider.GetColorBarGreen(cam.backgroundColor);
        settings.bgColor = cam.backgroundColor;
        settings.SaveSettings();
    }

    public void BGColorBlue()
    {
        cam.backgroundColor = colorSlider.GetColorBarBlue(cam.backgroundColor);
        settings.bgColor = cam.backgroundColor;
        settings.SaveSettings();
    }

    public void ResetColor()
    {
        cam.backgroundColor = new Color(0.196f, 0.294f, 0.627f, 1);
        settings.bgColor = cam.backgroundColor;
        settings.SaveSettings();

        colorSlider.SetColorSliders(cam.backgroundColor);
    }
}
