using System.IO;

using TMPro;

using UnityEngine;

public class SpritesetSetting : Settings
{
    public TMP_InputField spritesetPathIF;
    public TMP_DropdownPlus currentSpritesetDD;

    private GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();
        gameManager = GameManager.Instance;

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
        if (!string.IsNullOrWhiteSpace(spritesetPathIF.text) && (spritesetPathIF.text[^1] == '/' || spritesetPathIF.text[^1] == '\\'))
        {
            SetSpritesetPath();
        }
    }

    public void SetSpritesetPath()
    {
        string spritesetPath = spritesetPathIF.text;

        if (string.IsNullOrWhiteSpace(spritesetPath) || !Directory.Exists(spritesetPath))
        {
            Debug.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
            DebugHelper.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
            currentSpritesetDD.ClearOptions();
            return;
        }

        settings.spritesetsPath = spritesetPath;
        settings.SaveSettings();
        UpdateSpritesetDropdown();
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
        gameManager.spritesetImages = uiUtility.LoadSpriteset(settings.lastSpriteset);
        settings.SaveSettings();
    }
}
