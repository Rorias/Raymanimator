using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class SpritesetSettings : Settings
{
    public TMP_InputField spritesetPathIF;
    public TMP_Dropdown currentSpritesetDD;

    private void Awake()
    {
        spritesetPathIF.onValueChanged.AddListener(delegate { SetSpritesetPathViaBrowse(); });
        spritesetPathIF.onEndEdit.AddListener(delegate { SetSpritesetPath(); });

        currentSpritesetDD.onValueChanged.AddListener(delegate { SetCurrentSpriteset(); });
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
            uiUtility.ReloadDropdownSpriteOptions(settings.spritesetsPath, currentSpritesetDD);
            SetCurrentSpriteset();
            return true;
        }

        Debug.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
        DebugHelper.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
        currentSpritesetDD.ClearOptions();
        return false;
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
