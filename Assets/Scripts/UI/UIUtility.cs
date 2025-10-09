using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIUtility : MonoBehaviour
{
    public static readonly List<string> imageTypes = new List<string> { ".jpg", ".jpeg", ".png" };

    private GameManager gameManager;
    private GameSettings settings;

    private void Start()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
    }

    public void ReloadDropdownSpriteOptions(string _path, TMP_Dropdown _dropdown)
    {
        List<string> spritesetNames = new List<string>();
        string[] spritesets = Directory.GetDirectories(_path);

        foreach (string s in spritesets)
        {
            string[] files;

            try
            {
                files = Directory.GetFiles(s);
            }
            catch
            {
                continue;
            }

            if (files.Length <= 0) { continue; }

            bool unacceptedTypes = false;

            foreach (string f in files)
            {
                if (f.Contains("Thumbs.db") || Path.GetExtension(f).ToLowerInvariant() == ".meta")
                {
                    continue;
                }

                if (!imageTypes.Contains(Path.GetExtension(f).ToLowerInvariant()))
                {
                    unacceptedTypes = true;
                    break;
                }
            }

            if (unacceptedTypes)
            {
                continue;
            }

            string spritesetName = s.Substring(s.LastIndexOf('\\') + 1);
            if(spritesetName.Length == s.Length)
            {
                spritesetName = s.Substring(s.LastIndexOf('/') + 1);
            }
            spritesetNames.Add(spritesetName);
        }

        if (_dropdown.options.Count > 0) { _dropdown.ClearOptions(); }

        _dropdown.AddOptions(spritesetNames);

        for (int i = 0; i < spritesetNames.Count; i++)
        {
            _dropdown.options[i].text = spritesetNames[i];
        }
    }

    public void LoadSpriteset()
    {
        if (!string.IsNullOrWhiteSpace(settings.spritesetsPath))
        {
            if (Directory.Exists(settings.spritesetsPath))
            {
                string[] spritesetFolders = Directory.GetDirectories(settings.spritesetsPath);

                if (spritesetFolders.Length > 0)
                {
                    foreach (string folder in spritesetFolders)
                    {
                        int index = folder.LastIndexOf('\\') + 1;
                        if(index == 0)
                        {
                            index = folder.LastIndexOf('/') + 1;
                        }

                        if (folder.Substring(index) == settings.lastSpriteset)
                        {
                            string[] files = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly)
                                                      .Where(s => s.EndsWith(imageTypes[0]) || s.EndsWith(imageTypes[1]) || s.EndsWith(imageTypes[2])).ToArray();

                            ImportImagesAsSprites(files);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void ImportImagesAsSprites(string[] _images)
    {
        gameManager.spritesetImages.Clear();

        for (int i = 0; i < _images.Length; i++)
        {
            byte[] byteArray = File.ReadAllBytes(_images[i]);
            Texture2D sampleTexture = new Texture2D(2, 2);
            sampleTexture.LoadImage(byteArray);
            sampleTexture.filterMode = FilterMode.Point;
            Sprite newSprite = Sprite.Create(sampleTexture, new Rect(0, 0, sampleTexture.width, sampleTexture.height), new Vector2(0.5f, 0.5f), 16, 0, SpriteMeshType.FullRect);

            string spriteName = _images[i];

            if (spriteName.Contains(imageTypes[0]) || spriteName.Contains(imageTypes[1]) || spriteName.Contains(imageTypes[2]))
            {
                spriteName = _images[i].Remove(_images[i].Length - 4);
            }

            string newName = spriteName.Substring(spriteName.LastIndexOf('\\') + 1);
            if (newName.Length == spriteName.Length)
            {
                newName = spriteName.Substring(spriteName.LastIndexOf('/') + 1);
            }
            newSprite.name = newName;

            gameManager.spritesetImages.Add(i, newSprite);
        }
    }
}
