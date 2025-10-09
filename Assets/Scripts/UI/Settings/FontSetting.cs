using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class FontSetting : Settings
{
    public Toggle normalFontToggle;

    private ThemeController themeController;

    protected override void Awake()
    {
        base.Awake();

        themeController = FindObjectOfType<ThemeController>();

        normalFontToggle.onValueChanged.AddListener((_state) => SetFont(_state));
    }

    private void Start()
    {
        LoadFontSettings();
    }

    public void LoadFontSettings()
    {
        normalFontToggle.isOn = settings.normalFont;
    }

    public void SetFont(bool _state)
    {
        settings.normalFont = _state;
        settings.SaveSettings();
        themeController.UpdateFonts(_state);
    }
}
