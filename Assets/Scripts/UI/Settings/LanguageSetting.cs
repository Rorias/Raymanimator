using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class LanguageSetting : Settings
{
    public TMP_Dropdown languagesDD;
    private LanguageController languageController;

    protected override void Awake()
    {
        base.Awake();

        languageController = FindObjectOfType<LanguageController>();

        languagesDD.onValueChanged.AddListener(delegate { SetLanguage(); });
    }

    private void Start()
    {
        LoadLanguages();
    }

    public void LoadLanguages()
    {
        languagesDD.ClearOptions();

        List<string> languages = new List<string>();

        for (int i = 0; i < Enum.GetNames(typeof(GameSettings.Languages)).Length; i++)
        {
            languages.Add(Enum.GetNames(typeof(GameSettings.Languages))[i]);
        }

        languagesDD.AddOptions(languages);
        languagesDD.value = (int)settings.editorLanguage;
    }

    public void SetLanguage()
    {
        settings.editorLanguage = (GameSettings.Languages)languagesDD.value;
        settings.SaveSettings();
        languageController.UpdateLanguage();
    }
}
