using BinarySerializer;
using BinarySerializer.Ray1;
using BinarySerializer.Ray1.PC;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

using UnityEngine;

public sealed class Rayman1BinaryAnimation
{
    #region Singleton
    private static Rayman1BinaryAnimation _instance;
    private Rayman1BinaryAnimation() { }

    public static Rayman1BinaryAnimation Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new Rayman1BinaryAnimation();
                _instance.InitializeBinary();
                if (BinaryFilesExist(GameSettings.Instance.binaryBasePath))
                {
                    LoadBinaryFiles(GameSettings.Instance.binaryBasePath);
                }
            }
            return _instance;
        }
    }
    #endregion

    private static string basePath = "";
    private static string allfixFile = @"ALLFIX.DAT";

    private static Thread LoadBinaryFilesThreaded = null;

    private static Context context;
    private static string[][] files = new string[7][];
    public static AllfixFile allfix;
    public static WorldFile[] worlds = new WorldFile[6];
    public static LevelFile[] jungleLvls = new LevelFile[22];
    public static LevelFile[] musicLvls = new LevelFile[18];
    public static LevelFile[] stoneLvls = new LevelFile[13];
    public static LevelFile[] imageLvls = new LevelFile[13];
    public static LevelFile[] caveLvls = new LevelFile[12];
    public static LevelFile[] candyLvls = new LevelFile[4];

    private GameSettings settings;

    public static bool BinaryFilesExist(string _path)
    {
        if (string.IsNullOrWhiteSpace(_path) || !File.Exists(_path + "/" + allfixFile))
        {
            DebugHelper.Log("Rayman 1 for MS-DOS could not be found in this folder.", DebugHelper.Severity.error);
            return false;
        }

        return true;
    }

    public static void LoadBinaryFiles(string _path)
    {
        if (context == null || basePath != _path)
        {
            basePath = _path;
            context = new Context(basePath);
            context.AddSettings(new Ray1Settings(Ray1EngineVersion.PC));

            using (context)
            {
                //Allfix
                context.AddFile(new LinearFile(context, allfixFile));
                allfix = FileFactory.Read<AllfixFile>(context, allfixFile);

                //Worlds
                files[0] = Directory.GetFiles(basePath, "*.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < files[0].Length; i++)
                {
                    files[0][i] = Path.GetFileName(files[0][i]);
                    context.AddFile(new LinearFile(context, files[0][i]));
                }

                //Levels
                files[1] = Directory.GetFiles(basePath + "/JUNGLE", "*.LEV", SearchOption.TopDirectoryOnly).OrderBy
                    (
                    f => Convert.ToInt32(new FileInfo(f).Name.Substring(3, new FileInfo(f).Name.IndexOf(".") - 3)).ToString("00")
                    ).ToArray();
                for (int i = 0; i < files[1].Length; i++)
                {
                    files[1][i] = Path.GetFileName(files[1][i]);
                    context.AddFile(new LinearFile(context, "JUNGLE/" + files[1][i]));
                }

                files[2] = Directory.GetFiles(basePath + "/MUSIC", "*.LEV", SearchOption.TopDirectoryOnly).OrderBy
                    (
                    f => Convert.ToInt32(new FileInfo(f).Name.Substring(3, new FileInfo(f).Name.IndexOf(".") - 3)).ToString("00")
                    ).ToArray();
                for (int i = 0; i < files[2].Length; i++)
                {
                    files[2][i] = Path.GetFileName(files[2][i]);
                    context.AddFile(new LinearFile(context, "MUSIC/" + files[2][i]));
                }

                files[3] = Directory.GetFiles(basePath + "/MOUNTAIN", "*.LEV", SearchOption.TopDirectoryOnly).OrderBy
                    (
                    f => Convert.ToInt32(new FileInfo(f).Name.Substring(3, new FileInfo(f).Name.IndexOf(".") - 3)).ToString("00")
                    ).ToArray();
                for (int i = 0; i < files[3].Length; i++)
                {
                    files[3][i] = Path.GetFileName(files[3][i]);
                    context.AddFile(new LinearFile(context, "MOUNTAIN/" + files[3][i]));
                }

                files[4] = Directory.GetFiles(basePath + "/IMAGE", "*.LEV", SearchOption.TopDirectoryOnly).OrderBy
                    (
                    f => Convert.ToInt32(new FileInfo(f).Name.Substring(3, new FileInfo(f).Name.IndexOf(".") - 3)).ToString("00")
                    ).ToArray();
                for (int i = 0; i < files[4].Length; i++)
                {
                    files[4][i] = Path.GetFileName(files[4][i]);
                    context.AddFile(new LinearFile(context, "IMAGE/" + files[4][i]));
                }

                files[5] = Directory.GetFiles(basePath + "/CAVE", "*.LEV", SearchOption.TopDirectoryOnly).OrderBy
                    (
                    f => Convert.ToInt32(new FileInfo(f).Name.Substring(3, new FileInfo(f).Name.IndexOf(".") - 3)).ToString("00")
                    ).ToArray();
                for (int i = 0; i < files[5].Length; i++)
                {
                    files[5][i] = Path.GetFileName(files[5][i]);
                    context.AddFile(new LinearFile(context, "CAVE/" + files[5][i]));
                }

                files[6] = Directory.GetFiles(basePath + "/CAKE", "*.LEV", SearchOption.TopDirectoryOnly).OrderBy
                    (
                    f => Convert.ToInt32(new FileInfo(f).Name.Substring(3, new FileInfo(f).Name.IndexOf(".") - 3)).ToString("00")
                    ).ToArray();
                for (int i = 0; i < files[6].Length; i++)
                {
                    files[6][i] = Path.GetFileName(files[6][i]);
                    context.AddFile(new LinearFile(context, "CAKE/" + files[6][i]));
                }
                Debug.Log("Loaded fileNames into context.");
            }

            LoadBinaryFilesThreaded = new Thread(LoadBinaryFiles);
            LoadBinaryFilesThreaded.IsBackground = true;
            LoadBinaryFilesThreaded.Start();
        }
    }

    private static void LoadBinaryFiles()
    {
        using (context)
        {
            for (int i = 0; i < files[0].Length; i++)
            {
                worlds[i] = FileFactory.Read<WorldFile>(context, files[0][i]);
            }
            Debug.Log("Loaded worlds");

            for (int i = 0; i < files[1].Length; i++)
            {
                jungleLvls[i] = FileFactory.Read<LevelFile>(context, "JUNGLE/" + files[1][i]);
            }
            Debug.Log("Loaded JUNGLE");

            for (int i = 0; i < files[2].Length; i++)
            {
                musicLvls[i] = FileFactory.Read<LevelFile>(context, "MUSIC/" + files[2][i]);
            }
            Debug.Log("Loaded MUSIC");

            for (int i = 0; i < files[3].Length; i++)
            {
                stoneLvls[i] = FileFactory.Read<LevelFile>(context, "MOUNTAIN/" + files[3][i]);
            }
            Debug.Log("Loaded MOUNTAIN");

            for (int i = 0; i < files[4].Length; i++)
            {
                imageLvls[i] = FileFactory.Read<LevelFile>(context, "IMAGE/" + files[4][i]);
            }
            Debug.Log("Loaded IMAGE");

            for (int i = 0; i < files[5].Length; i++)
            {
                caveLvls[i] = FileFactory.Read<LevelFile>(context, "CAVE/" + files[5][i]);
            }
            Debug.Log("Loaded CAVE");

            for (int i = 0; i < files[6].Length; i++)
            {
                candyLvls[i] = FileFactory.Read<LevelFile>(context, "CAKE/" + files[6][i]);
            }
            Debug.Log("Loaded CAKE");
        }

        LoadBinaryFilesThreaded.Abort();
    }

    private void InitializeBinary()
    {
        //This is initialized here in case this is the first time settings are loaded (so only for test functions, basically)
        settings = GameSettings.Instance;
    }

    public Animation LoadRaymAnimationFromBinary(string _animName, Rayman1MSDOS.DesignObjects _object, int _animIndex, string _spriteset, Mapping _map)
    {
        Design des = GetR1MSDOSDesignByIndex((int)_object);
        if (des == null)
        {
            DebugHelper.Log("You need to choose a binary path first.", DebugHelper.Severity.warning);
            return null;
        }

        BinarySerializer.Ray1.Animation binaryAnim = des.Animations[_animIndex];
        //Debug.Log("Animation count for object " + _object.ToString() + ": " + des.Animations.Length);
        //Debug.Log("Succesfully retrieved animation data: " + des.Animations.Length);

        Animation rayman1Anim = new Animation();
        rayman1Anim.animationName = _animName;
        rayman1Anim.maxFrameCount = binaryAnim.FramesCount;
        rayman1Anim.maxPartCount = binaryAnim.LayersCount;
        rayman1Anim.gridSizeX = binaryAnim.Frames[0].Width;
        rayman1Anim.gridSizeY = binaryAnim.Frames[0].Height;
        rayman1Anim.usedSpriteset = _spriteset;
        rayman1Anim.binaryAnimationIndex = _animIndex;
        float pixelSize = _map == null ? 16f : _map.PixelSize;

        for (int frameIndex = 0; frameIndex < binaryAnim.FramesCount; frameIndex++)
        {
            Frame f = new Frame();
            f.frameID = frameIndex;
            for (int layerIndex = 0; layerIndex < binaryAnim.LayersCount; layerIndex++)
            {
                AnimationLayer animLayer = binaryAnim.Layers[frameIndex * binaryAnim.LayersCount + layerIndex];

                Part p = new Part();
                p.partID = layerIndex;
                p.partIndex = animLayer.SpriteIndex - 1;
                p.flipX = animLayer.FlipX;
                p.flipY = animLayer.FlipY;
                p.xPos = animLayer.XPosition / pixelSize;
                p.yPos = (255 - (animLayer.YPosition / pixelSize)) - 255;

                f.frameParts.Add(p);
            }

            rayman1Anim.frames.Add(f);
        }

        return rayman1Anim;
    }

    public void SaveRaymAnimationToBinary(
        Animation _rayman1Anim, Dictionary<int, UnityEngine.Sprite> _spriteset,
        int _objectIndex, int _animIndex,
        bool _animData, bool _visuals, bool _colls, float _pixelSize)
    {
        Design des = GetR1MSDOSDesignByIndex(_objectIndex);
        if (des == null)
        {
            DebugHelper.Log("You need to choose a binary path first.", DebugHelper.Severity.warning);
            return;
        }

        BinarySerializer.Ray1.Animation binaryAnim = des.Animations[_animIndex];
        if (_animData)
        {
            SaveFrames(_rayman1Anim, binaryAnim, _pixelSize);
        }

        if (_visuals)
        {
            byte[] imageData = SaveSpriteset(_spriteset, des, _colls);
            ConvertImageData(ref imageData);
            des.ImageData = imageData;
            Debug.Log("Recalculated imagedata size: " + des.ImageData.Length);
        }

        using (context)
        {
            if (_objectIndex <= Rayman1MSDOS.allfixEndIndex)
            {
                FileFactory.Write<AllfixFile>(context, allfixFile);
            }

            if (_objectIndex > Rayman1MSDOS.allfixEndIndex && _objectIndex <= Rayman1MSDOS.jungleEndIndex)
            {
                string[] worldFiles = Directory.GetFiles(basePath, "RAY1.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < worldFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(worldFiles[0]);
                    FileFactory.Write<WorldFile>(context, fileName);
                }
            }

            if (_objectIndex > Rayman1MSDOS.jungleEndIndex && _objectIndex <= Rayman1MSDOS.musicEndIndex)
            {
                string[] worldFiles = Directory.GetFiles(basePath, "RAY2.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < worldFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(worldFiles[0]);
                    FileFactory.Write<WorldFile>(context, fileName);
                }
            }

            if (_objectIndex > Rayman1MSDOS.musicEndIndex && _objectIndex <= Rayman1MSDOS.mountainEndIndex)
            {
                string[] worldFiles = Directory.GetFiles(basePath, "RAY3.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < worldFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(worldFiles[0]);
                    FileFactory.Write<WorldFile>(context, fileName);
                }
            }

            if (_objectIndex > Rayman1MSDOS.mountainEndIndex && _objectIndex <= Rayman1MSDOS.imageEndIndex)
            {
                string[] worldFiles = Directory.GetFiles(basePath, "RAY4.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < worldFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(worldFiles[0]);
                    FileFactory.Write<WorldFile>(context, fileName);
                }
            }

            if (_objectIndex > Rayman1MSDOS.imageEndIndex && _objectIndex <= Rayman1MSDOS.caveEndIndex)
            {
                string[] worldFiles = Directory.GetFiles(basePath, "RAY5.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < worldFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(worldFiles[0]);
                    FileFactory.Write<WorldFile>(context, fileName);
                }
            }

            if (_objectIndex > Rayman1MSDOS.caveEndIndex && _objectIndex <= Rayman1MSDOS.candyEndIndex)
            {
                string[] worldFiles = Directory.GetFiles(basePath, "RAY6.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < worldFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(worldFiles[0]);
                    FileFactory.Write<WorldFile>(context, fileName);
                }
            }
        }
    }

    private void SaveFrames(Animation _rayman1Anim, BinarySerializer.Ray1.Animation _binaryAnim, float _pixelSize)
    {
        //if (_binaryAnim.FramesCount < _rayman1Anim.maxFrameCount)
        //{
        //    for (int i = _binaryAnim.FramesCount - 1; i < _rayman1Anim.maxFrameCount; i++)
        //    {
        //        AnimationFrame af = new AnimationFrame()
        //        {

        //        }
        //    }
        //}

        for (int frameIndex = 0; frameIndex < _binaryAnim.FramesCount; frameIndex++)
        {
            Frame f = _rayman1Anim.frames[frameIndex];
            for (int layerIndex = 0; layerIndex < _binaryAnim.LayersCount; layerIndex++)
            {
                Part p = f.frameParts[layerIndex];
                AnimationLayer animLayer = _binaryAnim.Layers[frameIndex * _binaryAnim.LayersCount + layerIndex];

                layerIndex = p.partID;
                animLayer.SpriteIndex = (byte)(p.partIndex + 1);//Apply mapping where applicable
                animLayer.FlipX = p.flipX;
                animLayer.FlipY = p.flipY;
                animLayer.XPosition = (byte)(p.xPos * _pixelSize);
                animLayer.YPosition = (byte)((255 - (p.yPos + 255)) * _pixelSize);
            }
        }
    }

    public byte[] SaveSpriteset(Dictionary<int, UnityEngine.Sprite> _spriteset, Design _imageData, bool _colls)
    {
        var palette = jungleLvls[1].MapInfo.Palettes.First();
        List<Color> paletteColors = new List<Color>();
        paletteColors.Add(new Color(0, 0, 0, 0));
        for (int i = 1; i < 160; i++)
        {
            paletteColors.Add(new Color(palette[i].Red, palette[i].Green, palette[i].Blue, palette[i].Alpha));
        }

        List<byte> imageDataArray = new List<byte>(_imageData.ImageData);
        int prevBufferOffset = 0;

        foreach (KeyValuePair<int, UnityEngine.Sprite> sprite in _spriteset)
        {
            byte[] spriteData = new byte[sprite.Value.texture.width * sprite.Value.texture.height];

            //load sprite data into byte array
            for (int y = sprite.Value.texture.height - 1; y >= 0; y--)
            {
                for (int x = 0; x < sprite.Value.texture.width; x++)
                {
                    var index = (sprite.Value.texture.height - 1 - y) * sprite.Value.texture.width + x;

                    Color color = sprite.Value.texture.GetPixel(x, y);
                    if (color.a == 0)
                    {
                        spriteData[index] = 0;
                        continue;
                    }

                    int paletteIndex = ClosestColor(paletteColors, color);
                    if (paletteIndex != -1 && paletteIndex != 0)
                    {
                        spriteData[index] = (byte)paletteIndex;
                    }
                }
            }

            //Set the Width/Height of the current sprite to their new sizes
            _imageData.Sprites[sprite.Key + 1].Width = (short)sprite.Value.texture.width;
            _imageData.Sprites[sprite.Key + 1].Height = (short)sprite.Value.texture.height;

            if (_colls)
            {
                RecalculateSpriteSize(_imageData.Sprites[sprite.Key + 1], spriteData);
            }

            //Set the next sprite's imageBufferOffset based on the size of the current one 
            if (_imageData.Sprites.Length > sprite.Key + 2)
            {
                _imageData.Sprites[sprite.Key + 2].ImageBufferOffset = prevBufferOffset + spriteData.Length;
                prevBufferOffset = _imageData.Sprites[sprite.Key + 2].ImageBufferOffset;
            }

            //Load spritedata into imagedata, adding room as needed
            for (int i = 0; i < spriteData.Length; i++)
            {
                if (imageDataArray.Count <= _imageData.Sprites[sprite.Key + 1].ImageBufferOffset + i)
                {
                    imageDataArray.Add(0);
                }

                imageDataArray[_imageData.Sprites[sprite.Key + 1].ImageBufferOffset + i] = spriteData[i];
            }
        }

        return imageDataArray.ToArray();
    }

    //Originally written by RayCarrot
    public void ConvertImageData(ref byte[] _data)
    {
        int flag = -1;

        for (int i = _data.Length - 1; i >= 0; i--)
        {
            var val = _data[i];

            // Check if it should be transparent
            if (val == 0)
            {
                if (flag == -1)
                    flag = 0xA1;
                else
                    flag++;

                if (flag > 0xFF)
                    flag = 0xFF;

                _data[i] = (byte)flag;
            }
            else
            {
                flag = -1;
            }
        }
    }

    public Dictionary<int, UnityEngine.Sprite> LoadSpritesetFromBinary(Rayman1MSDOS.DesignObjects _object, int _paletteIndex)
    {
        Design des = GetR1MSDOSDesignByIndex((int)_object);
        if (des == null)
        {
            DebugHelper.Log("You need to choose a binary path first.", DebugHelper.Severity.warning);
            return null;
        }

        Dictionary<int, UnityEngine.Sprite> spriteset = new Dictionary<int, UnityEngine.Sprite>();

        IList<BaseColor> palette = GetR1MSDOSPaletteByIndex(_paletteIndex);
        int offset = 1;
        for (int i = 1; i < des.Sprites.Length; i++)
        {
            BinarySerializer.Ray1.Sprite sprite = des.Sprites[i];
            byte[] pixels = new byte[sprite.Width * sprite.Height];
            if (sprite.ImageBufferOffset >= des.ImageData.Length)
            {
                Debug.Log("<color=red>object is simply out of bounds of the image data array. How? I don't know either.</color>\n");
                continue;
            }
            Array.Copy(des.ImageData, sprite.ImageBufferOffset, pixels, 0, pixels.Length);
            if (sprite.Id != 0 && i + 1 < des.Sprites.Length)
            {
                if (des.Sprites[i + 1].ImageBufferOffset < sprite.ImageBufferOffset + pixels.Length)
                {
                    Debug.Log("<color=red>object overwrites previous objects bytes.</color>\n" +
                              "<color=red>offset " + des.Sprites[i + 1].ImageBufferOffset + " is wrong!</color>\n");
                    Debug.Log("<color=red>offset should be " + (sprite.ImageBufferOffset + pixels.Length) + ".</color>");
                }
                if (des.Sprites[i + 1].ImageBufferOffset > sprite.ImageBufferOffset + pixels.Length)
                {
                    Debug.Log("<color=red>object has empty bytes between.</color>\n" +
                              "<color=red>offset " + des.Sprites[i + 1].ImageBufferOffset + " is wrong!</color>\n");
                    Debug.Log("<color=red>offset should be " + (sprite.ImageBufferOffset + pixels.Length) + ".</color>");
                }
            }

            Texture2D sampleTexture = GetBinarySpriteTexture(sprite, palette, pixels);
            if (sampleTexture == null)
            {
                sampleTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Point,
                    wrapMode = TextureWrapMode.Clamp
                };
                sampleTexture.SetPixel(0, 0, Color.clear);
                sampleTexture.Apply();
            }

            UnityEngine.Sprite newSprite = UnityEngine.Sprite.Create(sampleTexture, new Rect(0, 0, sampleTexture.width, sampleTexture.height), new Vector2(0f, 1f), 16, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), true);
            spriteset.Add(i - offset, newSprite);
        }

        return spriteset;
    }

    private Design GetR1MSDOSDesignByIndex(int _objectIndex)
    {
        if (_objectIndex <= Rayman1MSDOS.allfixEndIndex && allfix != null)
        {
            return allfix.DesItems[_objectIndex];
        }

        if (worlds != null && worlds.Length > 0)
        {
            if (_objectIndex > Rayman1MSDOS.allfixEndIndex && _objectIndex <= Rayman1MSDOS.jungleEndIndex)
            {
                return worlds[0].DesItems[_objectIndex - Rayman1MSDOS.allfixEndIndex - 1];
            }

            if (_objectIndex > Rayman1MSDOS.jungleEndIndex && _objectIndex <= Rayman1MSDOS.musicEndIndex)
            {
                return worlds[1].DesItems[_objectIndex - Rayman1MSDOS.jungleEndIndex - 1];
            }

            if (_objectIndex > Rayman1MSDOS.musicEndIndex && _objectIndex <= Rayman1MSDOS.mountainEndIndex)
            {
                return worlds[2].DesItems[_objectIndex - Rayman1MSDOS.musicEndIndex - 1];
            }

            if (_objectIndex > Rayman1MSDOS.mountainEndIndex && _objectIndex <= Rayman1MSDOS.imageEndIndex)
            {
                return worlds[3].DesItems[_objectIndex - Rayman1MSDOS.mountainEndIndex - 1];
            }

            if (_objectIndex > Rayman1MSDOS.imageEndIndex && _objectIndex <= Rayman1MSDOS.caveEndIndex)
            {
                return worlds[4].DesItems[_objectIndex - Rayman1MSDOS.imageEndIndex - 1];
            }

            if (_objectIndex > Rayman1MSDOS.caveEndIndex && _objectIndex <= Rayman1MSDOS.candyEndIndex)
            {
                Debug.Log(worlds[5].DesItems.Length + " candy designs count");
                return worlds[5].DesItems[_objectIndex - Rayman1MSDOS.caveEndIndex - 1];
            }
        }

        return null;
    }

    private IList<BaseColor> GetR1MSDOSPaletteByIndex(int _paletteIndex)
    {
        switch (_paletteIndex)
        {
            case 0:
                return jungleLvls[0].MapInfo.Palettes.First();
            case 1:
                return jungleLvls[21].MapInfo.Palettes.First();
            case 2:
                return jungleLvls[14].MapInfo.Palettes.First();
            case 3:
                return jungleLvls[15].MapInfo.Palettes.First();
            case 4:
                return musicLvls[1].MapInfo.Palettes.First();
            case 5:
                return musicLvls[3].MapInfo.Palettes.First();
            case 6:
                return musicLvls[11].MapInfo.Palettes.First();
            case 7:
                return musicLvls[0].MapInfo.Palettes.First();
            case 8:
                return musicLvls[15].MapInfo.Palettes.First();
            case 9:
                return stoneLvls[0].MapInfo.Palettes.First();
            case 10:
                return stoneLvls[5].MapInfo.Palettes.First();
            case 11:
                return stoneLvls[9].MapInfo.Palettes.First();
            case 12:
                return imageLvls[0].MapInfo.Palettes.First();
            case 13:
                return imageLvls[3].MapInfo.Palettes.First();
            case 14:
                return imageLvls[10].MapInfo.Palettes.First();
            case 15:
                return caveLvls[1].MapInfo.Palettes.First();
            case 16:
                return caveLvls[0].MapInfo.Palettes.First();
            case 17:
                return caveLvls[2].MapInfo.Palettes.First();
            case 18:
                return caveLvls[10].MapInfo.Palettes.First();
            case 19:
                return candyLvls[0].MapInfo.Palettes.First();
            case 20:
                return candyLvls[3].MapInfo.Palettes.First();
            default:
                Debug.Log("<color=orange>THIS ISN'T SUPPOSED TO HAPPEN!!</color>");
                return jungleLvls[0].MapInfo.Palettes.First();
        }
    }

    public Texture2D GetBinarySpriteTexture(BinarySerializer.Ray1.Sprite s, IList<BaseColor> palette, byte[] processedImageData)
    {
        if (s.IsDummySprite())
            return null;

        short width = s.Width;
        short height = s.Height;

        Texture2D tex = CreateTexture2D(width, height);

        try
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixelOffset = y * width + x;

                    var pixel = processedImageData[pixelOffset];
                    if (pixel == 0 || pixel >= 160) { continue; }

                    BaseColor color = palette[pixel];
                    tex.SetPixel(x, height - 1 - y, new Color(color.Red, color.Green, color.Blue, color.Alpha));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Couldn't load sprite for DES: {ex.Message}");
            return null;
        }

        tex.Apply();
        return tex;
    }

    private Texture2D CreateTexture2D(int width, int height)
    {
        var tex = new Texture2D(width, height, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };

        tex.SetPixels(new Color[width * height]);
        return tex;
    }

    private int ClosestColor(List<Color> colors, Color target)
    {
        var colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
        return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs && n.a != 0);
    }

    private int ColorDiff(Color32 c1, Color32 c2)
    {
        return (int)Math.Sqrt((c1.r - c2.r) * (c1.r - c2.r)
                               + (c1.g - c2.g) * (c1.g - c2.g)
                               + (c1.b - c2.b) * (c1.b - c2.b));
    }

    //Originally written by RayCarrot
    public void RecalculateSpriteSize(BinarySerializer.Ray1.Sprite sprite, byte[] imgData)
    {
        int firstPixelX = 256;
        int firstPixelY = 256;

        int lastPixelX = -1;
        int lastPixelY = -1;

        for (int y = 0; y < sprite.Height; y++)
        {
            for (int x = 0; x < sprite.Width; x++)
            {
                byte pixel = imgData[y * sprite.Width + x];
                bool isTransparent = pixel == 0;

                if (!isTransparent)
                {
                    if (x < firstPixelX)
                        firstPixelX = x;

                    if (y < firstPixelY)
                        firstPixelY = y;

                    if (x > lastPixelX)
                        lastPixelX = x;

                    if (y > lastPixelY)
                        lastPixelY = y;
                }
            }
        }

        if (firstPixelX == 256 || firstPixelY == 256 || lastPixelX == -1 || lastPixelY == -1)
        {
            sprite.SpriteXPosition = 0;
            sprite.SpriteYPosition = 0;
            sprite.SpriteWidth = 0;
            sprite.SpriteHeight = 0;
        }
        else
        {
            sprite.SpriteXPosition = (byte)firstPixelX;
            sprite.SpriteYPosition = (byte)firstPixelY;
            sprite.SpriteWidth = (byte)(lastPixelX - firstPixelX + 1);
            sprite.SpriteHeight = (byte)(lastPixelY - firstPixelY + 1);
        }
    }
}
