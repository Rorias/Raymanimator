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
        SavePreviousGhostColor();
    }

    public void PrevGhostColorGreen()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarGreen(settings.previousGhostColor);
        SavePreviousGhostColor();
    }

    public void PrevGhostColorBlue()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarBlue(settings.previousGhostColor);
        SavePreviousGhostColor();
    }

    public void PrevGhostColorAlpha()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarAlpha(settings.previousGhostColor);
        SavePreviousGhostColor();
    }

    public void ResetPrevGhostColor()
    {
        settings.previousGhostColor = new Color(0, 1, 1, 0.25f);
        SavePreviousGhostColor();

        previousGhostColorSlider.SetColorSliders(settings.previousGhostColor);
    }

    private void SavePreviousGhostColor()
    {
        animatorC.UpdatePrevGhostColor();
        settings.SaveSettings();
    }

    public void NextGhostColorRed()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarRed(settings.nextGhostColor);
        SaveNextGhostColor();
    }

    public void NextGhostColorGreen()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarGreen(settings.nextGhostColor);
        SaveNextGhostColor();
    }

    public void NextGhostColorBlue()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarBlue(settings.nextGhostColor);
        SaveNextGhostColor();
    }

    public void NextGhostColorAlpha()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarAlpha(settings.nextGhostColor);
        SaveNextGhostColor();
    }

    public void ResetNextGhostColor()
    {
        settings.nextGhostColor = new Color(1, 0.2f, 0.2f, 0.25f);
        SaveNextGhostColor();

        nextGhostColorSlider.SetColorSliders(settings.nextGhostColor);
    }

    private void SaveNextGhostColor()
    {
        animatorC.UpdateNextGhostColor();
        settings.SaveSettings();
    }
}
