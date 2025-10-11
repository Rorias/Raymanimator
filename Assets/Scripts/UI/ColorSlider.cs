using System;

using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour
{
    public Action UpdateRed;
    public Action UpdateGreen;
    public Action UpdateBlue;
    public Action UpdateAlpha;

    public Action SetRed;
    public Action SetGreen;
    public Action SetBlue;
    public Action SetAlpha;

    public Action Reset;

    public Image redHandle;
    public Image greenHandle;
    public Image blueHandle;
    public Image alphaHandle;

    public SliderPlus redSlider;
    public SliderPlus greenSlider;
    public SliderPlus blueSlider;
    public SliderPlus alphaSlider;

    public ButtonPlus resetButton;

    private void Awake()
    {
        redSlider.onValueChanged.AddListener(delegate { UpdateRed?.Invoke(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateGreen?.Invoke(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateBlue?.Invoke(); });
        alphaSlider.onValueChanged.AddListener(delegate { UpdateAlpha?.Invoke(); });

        redSlider.onMouseUp.AddListener(delegate { SetRed?.Invoke(); });
        greenSlider.onMouseUp.AddListener(delegate { SetGreen?.Invoke(); });
        blueSlider.onMouseUp.AddListener(delegate { SetBlue?.Invoke(); });
        alphaSlider.onMouseUp.AddListener(delegate { SetAlpha?.Invoke(); });

        resetButton.onClick.AddListener(delegate { Reset?.Invoke(); });
    }

    public void SetColorSliders(Color _color)
    {
        redSlider.value = _color.r * 255f;
        greenSlider.value = _color.g * 255f;
        blueSlider.value = _color.b * 255f;
        alphaSlider.value = _color.a * 255f;
    }

    public Color GetColorBarRed(Color _color)
    {
        Color red = _color;
        red.r = redSlider.value / 255.0f;
        redHandle.color = new Color(redHandle.color.r, 1 - red.r, 1 - red.r);

        return red;
    }

    public Color GetColorBarGreen(Color _color)
    {
        Color green = _color;
        green.g = greenSlider.value / 255.0f;
        greenHandle.color = new Color(1 - green.g, greenHandle.color.g, 1 - green.g);

        return green;
    }

    public Color GetColorBarBlue(Color _color)
    {
        Color blue = _color;
        blue.b = blueSlider.value / 255.0f;
        blueHandle.color = new Color(1 - blue.b, 1 - blue.b, blueHandle.color.b);

        return blue;
    }

    public Color GetColorBarAlpha(Color _color)
    {
        Color alpha = _color;
        alpha.a = alphaSlider.value / 255.0f;

        return alpha;
    }
}
