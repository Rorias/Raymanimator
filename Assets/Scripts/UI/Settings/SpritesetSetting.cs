using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class SpritesetSetting : Settings
{
    public TMP_InputField spritesetPathIF;
    public TMP_Dropdown currentSpritesetDD;

    protected override void Awake()
    {
        base.Awake();

        spritesetPathIF.onValueChanged.AddListener(delegate { SetSpritesetPathViaBrowse(); });
        spritesetPathIF.onEndEdit.AddListener(delegate { SetSpritesetPath(); });

        currentSpritesetDD.onValueChanged.AddListener(delegate { SetCurrentSpriteset(); });
    }

    public void LoadSpritesetSettings()
    {
        if (string.IsNullOrWhiteSpace(settings.spritesetsPath) || !Directory.Exists(settings.spritesetsPath))
        {
            Debug.Log("Spriteset path doesn't exist or has been changed.");
            DebugHelper.Log("Spriteset path doesn't exist or has been changed.");
            return;
        }

        spritesetPathIF.text = settings.spritesetsPath;
    }

    public void SetSpritesetPathViaBrowse()
    {
        if (!SetSpritesetPath())
        {
            settings.lastSpriteset = "";
            settings.SaveSettings();
        }
    }

    public bool SetSpritesetPath()
    {
        string spritesetPath = spritesetPathIF.text;

        if (!string.IsNullOrWhiteSpace(spritesetPath) && Directory.Exists(spritesetPath))
        {
            settings.spritesetsPath = spritesetPath;
            UpdateSpritesetDropdown();
            return true;
        }

        Debug.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
        DebugHelper.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
        currentSpritesetDD.ClearOptions();
        return false;
    }

    private void UpdateSpritesetDropdown()
    {
        uiUtility.ReloadDropdownSpriteOptions(settings.spritesetsPath, currentSpritesetDD);
        int index = currentSpritesetDD.options.FindIndex(x => x.text == settings.lastSpriteset);
        currentSpritesetDD.value = index == -1 ? 0 : index;
    }

    public void SetCurrentSpriteset()
    {
        Debug.Log("setting spriteset");
        if (currentSpritesetDD.value >= currentSpritesetDD.options.Count)
        {
            Debug.Log("No spritesets found in selected folder.");
            return;
        }

        settings.lastSpriteset = currentSpritesetDD.options[currentSpritesetDD.value].text;
        uiUtility.LoadSpriteset();
        settings.SaveSettings();
    }
}
