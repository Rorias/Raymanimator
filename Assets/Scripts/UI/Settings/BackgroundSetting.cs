using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class BackgroundSettings : Settings
{
    public TMP_InputField backgroundImagePathIF;
    public TMP_Dropdown backgroundsDD;

    public Image redHandle;
    public Image greenHandle;
    public Image blueHandle;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public ButtonPlus resetButton;

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

        redSlider.onValueChanged.AddListener(delegate { BGColorRed(); });
        greenSlider.onValueChanged.AddListener(delegate { BGColorGreen(); });
        blueSlider.onValueChanged.AddListener(delegate { BGColorBlue(); });
        resetButton.onClick.AddListener(delegate { ResetColor(); });
    }

    private void Start()
    {
        redSlider.value = settings.bgColor.r;
        greenSlider.value = settings.bgColor.g;
        blueSlider.value = settings.bgColor.b;
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
        Color bgRed = Camera.main.backgroundColor;
        bgRed.r = redSlider.value / 255.0f;
        Camera.main.backgroundColor = bgRed;

        redHandle.color = new Color(redHandle.color.r, 1 - bgRed.r, 1 - bgRed.r);
        settings.bgColor = Camera.main.backgroundColor;
        settings.SaveSettings();
    }

    public void BGColorGreen()
    {
        Color bgGreen = Camera.main.backgroundColor;
        bgGreen.g = greenSlider.value / 255.0f;
        Camera.main.backgroundColor = bgGreen;

        greenHandle.color = new Color(1 - bgGreen.g, greenHandle.color.g, 1 - bgGreen.g);
        settings.bgColor = Camera.main.backgroundColor;
        settings.SaveSettings();
    }

    public void BGColorBlue()
    {
        Color bgBlue = Camera.main.backgroundColor;
        bgBlue.b = blueSlider.value / 255.0f;
        Camera.main.backgroundColor = bgBlue;

        blueHandle.color = new Color(1 - bgBlue.b, 1 - bgBlue.b, blueHandle.color.b);
        settings.bgColor = Camera.main.backgroundColor;
        settings.SaveSettings();
    }

    public void ResetColor()
    {
        Camera.main.backgroundColor = new Color(0.196f, 0.294f, 0.627f, 1);
        settings.bgColor = Camera.main.backgroundColor;
        settings.SaveSettings();

        redSlider.value = settings.bgColor.r;
        greenSlider.value = settings.bgColor.g;
        blueSlider.value = settings.bgColor.b;
    }
}
