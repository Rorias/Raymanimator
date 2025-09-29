using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class InitialisationMenu : MonoBehaviour
{
    public Menus menus;
    public MenuItem startMenu;
    public TMP_InputField spritesetPathIF;

    private GameSettings settings;

    private void Start()
    {
        settings = GameSettings.Instance;

        if (settings.lastSpriteset == "")
        {
            spritesetPathIF.text = settings.spritesetsPath;
        }
    }

    public void ApplySettings()
    {
        if (string.IsNullOrWhiteSpace(settings.spritesetsPath) || !Directory.Exists(settings.spritesetsPath))
        {
            Debug.Log("No spriteset path has been selected, or the selected path is not valid.");
            DebugHelper.Log("No spriteset path has been selected, or the selected path is not valid.");
            return;
        }

        if (settings.lastSpriteset == "")
        {
            Debug.Log("No spriteset has been selected from the list.");
            DebugHelper.Log("No spriteset has been selected from the list.");
            return;
        }

        if (string.IsNullOrWhiteSpace(settings.animationsPath) || !Directory.Exists(settings.animationsPath))
        {
            Debug.Log("No animation save path has been selected, or the selected path is not valid.");
            DebugHelper.Log("No animation save path has been selected, or the selected path is not valid.");
            return;
        }

        menus.ActivateNextMenu(startMenu);
    }
}
