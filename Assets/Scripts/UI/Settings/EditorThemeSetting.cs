using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class EditorThemeSetting : Settings
{
    public TMP_Dropdown editorThemeDD;

    private ThemeController themeController;

    protected override void Awake()
    {
        base.Awake();

        themeController = FindObjectOfType<ThemeController>();

        editorThemeDD.onValueChanged.AddListener(delegate { SetEditorTheme(); });
    }

    public void LoadThemeSettings()
    {
        editorThemeDD.ClearOptions();

        List<string> themes = new List<string>();

        for (int i = 0; i < Enum.GetNames(typeof(GameSettings.Themes)).Length; i++)
        {
            themes.Add(Enum.GetNames(typeof(GameSettings.Themes))[i]);
        }

        editorThemeDD.AddOptions(themes);
        editorThemeDD.value = (int)settings.editorTheme;
    }

    public void SetEditorTheme()
    {
        settings.editorTheme = (GameSettings.Themes)editorThemeDD.value;
        settings.SaveSettings();
        themeController.UpdateTheme();
    }
}
