using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

public class Mapping
{
    public Mapping() { }

    public bool Enabled { get { return on; } set { on = value; } }
    [SerializeField] private bool on = false;
    public string MapFromSet { get { return mfs; } set { mfs = value; } }
    [SerializeField] private string mfs = "Custom";
    public string MapToSet { get { return mts; } set { mts = value; } }
    [SerializeField] private string mts = "Rayman";
    public float PixelSize { get { return pd; } set { pd = value; } }
    [SerializeField] private float pd = 16.0f;

    public Mapper[] MappingValues { get { return mv; } set { mv = value; } }
    [SerializeField] private Mapper[] mv;

    [Serializable]
    public class Mapper
    {
        [SerializeField] public int s;
        [SerializeField] public int t;
    }

    public static string filePath { get; private set; } = Application.streamingAssetsPath + @"/Mappings/";

    public void SaveMapping(string _fileName)
    {
        File.WriteAllText(filePath + _fileName + ".json", JsonUtility.ToJson(this));

        //string json = JsonUtility.ToJson(this);
        //Debug.Log(json);
    }

    public static Mapping LoadMapping(string _fileName)
    {
        if (File.Exists(filePath + _fileName + ".json"))
        {
            string json = File.ReadAllText(filePath + _fileName + ".json");
            return (Mapping)JsonUtility.FromJson(json, typeof(Mapping));
        }
        else
        {
            string json = CreateEmptyMapping(filePath + _fileName + ".json");
            return (Mapping)JsonUtility.FromJson(json, typeof(Mapping));
        }
    }

    private static string CreateEmptyMapping(string _path)
    {
        if (_path != null)
        {
            File.WriteAllText(_path, @"{}");
            return File.ReadAllText(_path);
        }

        return string.Empty;
    }

    public Dictionary<int, Sprite> GenerateMappingSpriteset(string _sourceSpriteset, string _targetSpriteset, string _spritePath, UIUtility _getSprites)
    {
        if (string.IsNullOrWhiteSpace(_spritePath) || !Directory.Exists(_spritePath))
        {
            DebugHelper.Log("No spriteset path was found or given.", DebugHelper.Severity.error);
            return null;
        }

        string[] spritesetFolders = Directory.GetDirectories(_spritePath);

        if (spritesetFolders.Length <= 0)
        {
            DebugHelper.Log("No spriteset folders were found in the given path.", DebugHelper.Severity.error);
            return null;
        }

        string[] sourceFiles = new string[] { };
        string[] targetFiles = new string[] { };

        foreach (string folder in spritesetFolders)
        {
            int index = folder.LastIndexOf('\\') + 1;
            if (index == 0)
            {
                index = folder.LastIndexOf('/') + 1;
            }

            if (folder.Substring(index) == _sourceSpriteset)
            {
                sourceFiles = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(s => s.EndsWith(UIUtility.imageTypes[0]) || s.EndsWith(UIUtility.imageTypes[1]) || s.EndsWith(UIUtility.imageTypes[2])).ToArray();
            }

            if (folder.Substring(index) == _targetSpriteset)
            {
                targetFiles = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(s => s.EndsWith(UIUtility.imageTypes[0]) || s.EndsWith(UIUtility.imageTypes[1]) || s.EndsWith(UIUtility.imageTypes[2])).ToArray();
            }
        }

        Dictionary<int, Sprite> spritesetImages = new Dictionary<int, Sprite>();
        Dictionary<int, Sprite> sourceImages = _getSprites.ImportImagesAsSprites(sourceFiles, 16);
        Dictionary<int, Sprite> targetImages = _getSprites.ImportImagesAsSprites(targetFiles, 16);

        //Assume if there were no target images found that this is a binary spriteset
        if (targetImages.Count == 0)
        {
            Rayman1MSDOS.DesignObjects currObject = (Rayman1MSDOS.DesignObjects)Enum.Parse(typeof(Rayman1MSDOS.DesignObjects), _targetSpriteset);
            targetImages = Rayman1BinaryAnimation.Instance.LoadSpritesetFromBinary(currObject);
        }

        for (int i = 0; i < MappingValues.Length; i++)
        {
            if (MappingValues[i].s == -1)
            {
                spritesetImages.Add(i, targetImages[MappingValues[i].t]);
            }
            else
            {
                spritesetImages.Add(i, sourceImages[MappingValues[i].s]);
            }
        }

        return spritesetImages;
    }
}
