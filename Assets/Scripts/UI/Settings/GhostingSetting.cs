using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GhostingSetting : Settings
{
    public Toggle ghostingPreviousToggle;
    public Toggle ghostingNextToggle;

    public ColorSlider previousGhostColorSlider;
    public ColorSlider nextGhostColorSlider;

    protected override void Awake()
    {
        base.Awake();

        //ghostingNextToggle.onValueChanged.AddListener(delegate { SetSpritesetPath(); });
        //ghostingPreviousToggle.onValueChanged.AddListener(delegate { SetSpritesetPath(); });

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
        nextGhostColorSlider.SetColorSliders(settings.nextGhostColor);
    }

    public void PrevGhostColorRed()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarRed(settings.previousGhostColor);
        settings.SaveSettings();
    }

    public void PrevGhostColorGreen()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarGreen(settings.previousGhostColor);
        settings.SaveSettings();
    }

    public void PrevGhostColorBlue()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarBlue(settings.previousGhostColor);
        settings.SaveSettings();
    }

    public void PrevGhostColorAlpha()
    {
        settings.previousGhostColor = previousGhostColorSlider.GetColorBarAlpha(settings.previousGhostColor);
        settings.SaveSettings();
    }

    public void ResetPrevGhostColor()
    {
        settings.previousGhostColor = new Color(0, 1, 1, 0.25f);
        settings.SaveSettings();

        previousGhostColorSlider.SetColorSliders(settings.previousGhostColor);
    }

    public void NextGhostColorRed()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarRed(settings.nextGhostColor);
        settings.SaveSettings();
    }

    public void NextGhostColorGreen()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarGreen(settings.nextGhostColor);
        settings.SaveSettings();
    }

    public void NextGhostColorBlue()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarBlue(settings.nextGhostColor);
        settings.SaveSettings();
    }

    public void NextGhostColorAlpha()
    {
        settings.nextGhostColor = nextGhostColorSlider.GetColorBarAlpha(settings.nextGhostColor);
        settings.SaveSettings();
    }

    public void ResetNextGhostColor()
    {
        settings.nextGhostColor = new Color(0, 1, 1, 0.25f);
        settings.SaveSettings();

        nextGhostColorSlider.SetColorSliders(settings.nextGhostColor);
    }
}
