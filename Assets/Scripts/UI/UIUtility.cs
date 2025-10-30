using System.Collections.Generic;
using System.IO;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class UIUtility : MonoBehaviour
{
    public static readonly List<string> imageTypes = new List<string> { ".jpg", ".jpeg", ".png" };
    public static readonly List<RaycastResult> rayResults = new List<RaycastResult>();
    private static PointerEventData pointerData;

    private GameSettings settings;

    private void Awake()
    {
        settings = GameSettings.Instance;

        pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };
    }

    public static void GetRayResults()
    {
        pointerData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerData, rayResults);
    }

    public void ReloadDropdownSpriteOptions(string _path, TMP_DropdownPlus _dropdown)
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
            if (spritesetName.Length == s.Length)
            {
                spritesetName = s.Substring(s.LastIndexOf('/') + 1);
            }
            spritesetNames.Add(spritesetName);
        }

        if (_dropdown.options.Count > 0) { _dropdown.ClearOptions(); }

        _dropdown.AddOptions(spritesetNames);
    }

    public Dictionary<int, Sprite> LoadSpriteset(string _spritesetName)
    {
        if (!string.IsNullOrWhiteSpace(settings.spritesetsPath) && Directory.Exists(settings.spritesetsPath))
        {
            string[] spritesetFolders = Directory.GetDirectories(settings.spritesetsPath);

            if (spritesetFolders.Length > 0)
            {
                Dictionary<int, Sprite> spritesetImages = new Dictionary<int, Sprite>();

                foreach (string folder in spritesetFolders)
                {
                    int index = folder.LastIndexOf('\\') + 1;
                    if (index == 0)
                    {
                        index = folder.LastIndexOf('/') + 1;
                    }

                    if (folder.Substring(index) == _spritesetName)
                    {
                        string[] files = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly)
                                                  .Where(s => s.EndsWith(imageTypes[0]) || s.EndsWith(imageTypes[1]) || s.EndsWith(imageTypes[2])).ToArray();

                        spritesetImages.Clear();
                        spritesetImages = ImportImagesAsSprites(files, 16);
                        break;
                    }
                }

                return spritesetImages;
            }
        }

        return null;
    }

    public void LoadBackgrounds(Dictionary<int, Sprite> _backgroundImages)
    {
        if (!string.IsNullOrWhiteSpace(settings.backgroundPath) && Directory.Exists(settings.spritesetsPath))
        {
            string[] files = Directory.GetFiles(settings.backgroundPath, "*.*", SearchOption.TopDirectoryOnly)
                                      .Where(s => s.EndsWith(imageTypes[0]) || s.EndsWith(imageTypes[1]) || s.EndsWith(imageTypes[2])).ToArray();

            _backgroundImages.Clear();
            _backgroundImages = ImportImagesAsSprites(files, 16);
        }
    }

    public Dictionary<int, Sprite> ImportImagesAsSprites(string[] _images, int _pixelsPerUnit)
    {
        Dictionary<int, Sprite> folderImages = new Dictionary<int, Sprite>();

        for (int i = 0; i < _images.Length; i++)
        {
            byte[] byteArray = File.ReadAllBytes(_images[i]);
            Texture2D sampleTexture = new Texture2D(2, 2);
            sampleTexture.LoadImage(byteArray);
            sampleTexture.filterMode = FilterMode.Point;
            Sprite newSprite = Sprite.Create(sampleTexture, new Rect(0, 0, sampleTexture.width, sampleTexture.height), new Vector2(0f, 1f), _pixelsPerUnit, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), true);

            newSprite.name = Path.GetFileNameWithoutExtension(_images[i]);

            folderImages.Add(i, newSprite);
        }

        return folderImages;
    }
}
