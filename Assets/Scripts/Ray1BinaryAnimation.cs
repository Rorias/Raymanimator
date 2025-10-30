using BinarySerializer;
using BinarySerializer.Ray1;
using BinarySerializer.Ray1.PC;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                LoadBinaryAnimations(GameSettings.Instance);
            }
            return _instance;
        }
    }
    #endregion

    private static string basePath = "";
    private static string allfixFile = @"ALLFIX.DAT";
    private static string level1File = @"JUNGLE\RAY1.LEV";

    private static Context context;
    public static AllfixFile allfix;
    public static WorldFile[] worlds = new WorldFile[6];
    public static List<LevelFile> world1levels = new List<LevelFile>();

    private GameSettings settings;

    public static void LoadBinaryAnimations(GameSettings _settings)
    {
        if ((!string.IsNullOrWhiteSpace(_settings.binaryBasePath) && context == null) || basePath != _settings.binaryBasePath)
        {
            basePath = _settings.binaryBasePath;
            context = new Context(_settings.binaryBasePath);
            context.AddSettings(new Ray1Settings(Ray1EngineVersion.PC));

            using (context)
            {
                context.AddFile(new LinearFile(context, allfixFile));
                allfix = FileFactory.Read<AllfixFile>(context, allfixFile);
            }

            using (context)
            {
                string[] worldFiles = Directory.GetFiles(basePath, "*.WLD", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < worldFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(worldFiles[i]);
                    context.AddFile(new LinearFile(context, fileName));
                    worlds[i] = FileFactory.Read<WorldFile>(context, fileName);
                }
            }

            using (context)
            {
                context.AddFile(new LinearFile(context, level1File));
                world1levels.Add(FileFactory.Read<LevelFile>(context, level1File));
            }
        }
    }

    private void InitializeBinary()
    {
        //This is initialized here in case this is the first time settings are loaded (so only for test functions, basically)
        settings = GameSettings.Instance;
    }

    public Animation LoadRaymAnimationFromBinary(string _animName, int _objectIndex, int _animIndex, string _spriteset, Mapping _map)
    {
        Design des = GetDesignFromIndex(_objectIndex);
        BinarySerializer.Ray1.Animation binaryAnim = des.Animations[_animIndex];
        Debug.Log("Animation count for object " + _objectIndex.ToString() + ": " + des.Animations.Length);
        Debug.Log("Succesfully retrieved animation data: " + des.Animations.Length);

        Animation rayman1Anim = new Animation();
        rayman1Anim.animationName = _animName;
        rayman1Anim.maxFrameCount = binaryAnim.FramesCount;
        rayman1Anim.maxPartCount = binaryAnim.LayersCount;
        rayman1Anim.gridSizeX = 256;
        rayman1Anim.gridSizeY = 256;
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
                p.partIndex = animLayer.SpriteIndex - 1;//Apply mapping where applicable
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
        BinarySerializer.Ray1.Animation binaryAnim = allfix.DesItems[_objectIndex].Animations[_animIndex];
        if (_animData)
        {
            SaveFrames(_rayman1Anim, binaryAnim, _pixelSize);
        }

        if (_visuals)
        {
            byte[] imageData = SaveSpriteset(_spriteset, allfix.DesItems[_objectIndex], _colls);
            ConvertImageData(ref imageData);
            allfix.DesItems[_objectIndex].ImageData = imageData;
        }

        using (context)
        {
            FileFactory.Write<AllfixFile>(context, allfixFile);
        }
    }

    private void SaveFrames(Animation _rayman1Anim, BinarySerializer.Ray1.Animation _binaryAnim, float _pixelSize)
    {
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
        var palette = world1levels[0].MapInfo.Palettes.First();
        List<Color> paletteColors = new List<Color>();
        paletteColors.Add(new Color(0, 0, 0, 0));
        for (int i = 1; i < 160; i++)
        {
            paletteColors.Add(new Color(palette[i].Red, palette[i].Green, palette[i].Blue, palette[i].Alpha));
        }

        List<byte> imageDataArray = new List<byte>(_imageData.ImageData);
        //List<int> bufferOffsetIndex = new List<int>();
        int prevBufferOffset = 0;

        foreach (KeyValuePair<int, UnityEngine.Sprite> sprite in _spriteset)
        {
            //bool outOfBounds = false;
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

            //Set the Width/Height of the current sprite to their new sizes, and set the next sprite's imageBufferOffset based on the size of the current one 
            if (_imageData.Sprites.Length > sprite.Key + 2 && _imageData.Sprites[sprite.Key + 1].Id != 0)
            {
                _imageData.Sprites[sprite.Key + 1].Width = (short)sprite.Value.texture.width;
                _imageData.Sprites[sprite.Key + 1].Height = (short)sprite.Value.texture.height;

                if (_colls)
                {
                    RecalculateSpriteSize(_imageData.Sprites[sprite.Key + 1], spriteData);
                }

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

    public Dictionary<int, UnityEngine.Sprite> LoadSpritesetFromBinary(int _objectIndex)
    {
        Design des = GetDesignFromIndex(_objectIndex);
        Dictionary<int, UnityEngine.Sprite> spriteset = new Dictionary<int, UnityEngine.Sprite>();

        int offset = 1;
        for (int i = 1; i < des.Sprites.Length; i++)
        {
            BinarySerializer.Ray1.Sprite sprite = des.Sprites[i];
            if (sprite.Id == 0)
            {
                offset++;
                continue;
            }

            byte[] pixels = new byte[sprite.Width * sprite.Height];
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

            Texture2D sampleTexture = GetBinarySpriteTexture(sprite, world1levels[0].MapInfo.Palettes.First(), pixels);
            if (sampleTexture == null)
            {
                offset++;
                continue;
            }

            UnityEngine.Sprite newSprite = UnityEngine.Sprite.Create(sampleTexture, new Rect(0, 0, sampleTexture.width, sampleTexture.height), new Vector2(0f, 1f), 16, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), true);
            spriteset.Add(i - offset, newSprite);
        }

        return spriteset;
    }

    public Design GetDesignFromIndex(int _objectIndex)
    {
        int world1Index = 7;

        if (_objectIndex < world1Index)
        {
            return allfix.DesItems[_objectIndex];
        }

        if(_objectIndex >= world1Index)
        {
            return worlds[0].DesItems[_objectIndex - world1Index];
        }

        return null;
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
