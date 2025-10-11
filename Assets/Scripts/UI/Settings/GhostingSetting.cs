using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GhostingSetting : Settings
{
    public AnimatorController animatorC;

    public Toggle ghostingPreviousToggle;
    public Toggle ghostingNextToggle;

    public ColorSlider previousGhostColorSlider;
    public ColorSlider nextGhostColorSlider;

    protected override void Awake()
    {
        base.Awake();

        ghostingPreviousToggle.onValueChanged.AddListener((_state) => { SetPreviousGhosting(_state); });
        ghostingNextToggle.onValueChanged.AddListener((_state) => { SetNextGhosting(_state); });

        previousGhostColorSlider.SetRed += PrevGhostColorRed;
        previousGhostColorSlider.SetGreen += PrevGhostColorGreen;
        previousGhostColorSlider.SetBlue += PrevGhostColorBlue;
        previousGhostColorSlider.SetAlpha += PrevGhostColorAlpha;
        previousGhostColorSlider.Reset += ResetPrevGhostColor;

        nextGhostColorSlider.SetRed += NextGhostColorRed;
        nextGhostColorSlider.SetGreen += NextGhostColorGreen;
        nextGhostColorSlider.SetBlue += NextGhostColorBlue;
        nextGhostColorSlider.SetAlpha += NextGhostColorAlpha;
        nextGhostColorSlider.Reset += ResetNextGhostColor;
    }

    private void Start()
    {
        previousGhostColorSlider.SetColorSliders(settings.previousGhostColor);
        ghostingPreviousToggle.isOn = settings.previousGhostOn;

        nextGhostColorSlider.SetColorSliders(settings.nextGhostColor);
        ghostingNextToggle.isOn = settings.nextGhostOn;
    }

    public void SetPreviousGhosting(bool _state)
    {
        animatorC.ghostingPrevious = _state;
        settings.previousGhostOn = _state;
        settings.SaveSettings();
    }

    public void SetNextGhosting(bool _state)
    {
        animatorC.ghostingNext = _state;
        settings.nextGhostOn = _state;
        settings.SaveSettings();
    }

    public void PrevGhostColorRed()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarRed(settings.previousGhostColor);
        animatorC.UpdatePrevGhostColor();
        settings.SaveSettings();
    }

    public void PrevGhostColorGreen()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarGreen(settings.previousGhostColor);
        animatorC.UpdatePrevGhostColor();
        settings.SaveSettings();
    }

    public void PrevGhostColorBlue()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarBlue(settings.previousGhostColor);
        animatorC.UpdatePrevGhostColor();
        settings.SaveSettings();
    }

    public void PrevGhostColorAlpha()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarAlpha(settings.previousGhostColor);
        animatorC.UpdatePrevGhostColor();
        settings.SaveSettings();
    }

    public void ResetPrevGhostColor()
    {
        settings.previousGhostColor = new Color(0, 1, 1, 0.25f);
        animatorC.UpdatePrevGhostColor();
        settings.SaveSettings();

        previousGhostColorSlider.SetColorSliders(settings.previousGhostColor);
    }

    public void NextGhostColorRed()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarRed(settings.nextGhostColor);
        animatorC.UpdateNextGhostColor();
        settings.SaveSettings();
    }

    public void NextGhostColorGreen()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarGreen(settings.nextGhostColor);
        animatorC.UpdateNextGhostColor();
        settings.SaveSettings();
    }

    public void NextGhostColorBlue()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarBlue(settings.nextGhostColor);
        animatorC.UpdateNextGhostColor();
        settings.SaveSettings();
    }

    public void NextGhostColorAlpha()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarAlpha(settings.nextGhostColor);
        animatorC.UpdateNextGhostColor();
        settings.SaveSettings();
    }

    public void ResetNextGhostColor()
    {
        settings.nextGhostColor = new Color(1, 0.2f, 0.2f, 0.25f);
        animatorC.UpdateNextGhostColor();
        settings.SaveSettings();

        nextGhostColorSlider.SetColorSliders(settings.nextGhostColor);
    }
}
