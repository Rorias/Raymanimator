using System.IO;

using UnityEngine;

public class InitialisationMenu : MonoBehaviour
{
    public Menus menus;
    public MenuItem startMenu;
    public SpritesetSetting spritesetSetting;

    private GameSettings settings;

    private void Start()
    {
        settings = GameSettings.Instance;
        spritesetSetting.LoadSpritesetSettings();
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

        settings.firstLoad = false;
        settings.SaveSettings();
        menus.ActivateNextMenu(startMenu);
    }
}
