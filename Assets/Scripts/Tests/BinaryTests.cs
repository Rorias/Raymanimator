using BinarySerializer;
using BinarySerializer.Ray1;
using BinarySerializer.Ray1.PC;

using NUnit.Framework;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class BinaryTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator TestConvertSpriteFromBinaryToUnityAndBack()
    {
        GameSettings.file = Application.persistentDataPath + "/" + GameSettings.fileName + ".json";
        var instance = Rayman1BinaryAnimation.Instance;

        Design raymanDes = Rayman1BinaryAnimation.allfix.DesItems[0];

        BinarySerializer.Ray1.Sprite sprite = raymanDes.Sprites[1];
        byte[] pixels = new byte[sprite.Width * sprite.Height];
        Array.Copy(raymanDes.ImageData, sprite.ImageBufferOffset, pixels, 0, pixels.Length);

        string pixelDataString = string.Empty;
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] > 160) { pixels[i] = 255; }
            pixelDataString += pixels[i].ToString() + ",";
        }
        Debug.Log(pixelDataString);

        yield return new WaitForSeconds(3f);

        var palette = Rayman1BinaryAnimation.jungleLvls[0].MapInfo.Palettes.First();
        List<Color> paletteColors = new List<Color>();
        paletteColors.Add(new Color(0, 0, 0, 0));
        for (int i = 1; i < 112; i++)
        {
            paletteColors.Add(new Color(palette[i].Red, palette[i].Green, palette[i].Blue, palette[i].Alpha));
        }

        Texture2D sampleTexture = instance.GetBinarySpriteTexture(sprite, palette, pixels);
        UnityEngine.Sprite newSprite = UnityEngine.Sprite.Create(sampleTexture, new Rect(0, 0, sampleTexture.width, sampleTexture.height), new Vector2(0f, 1f), 16, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), true);

        string spritePixelDataString = string.Empty;
        for (int y = newSprite.texture.height - 1; y >= 0; y--)
        {
            for (int x = 0; x < newSprite.texture.width; x++)
            {
                Color color = newSprite.texture.GetPixel(x, y);
                if (color.a == 0)
                {
                    spritePixelDataString += "255,";
                    continue;
                }
                int paletteIndex = ClosestColor(paletteColors, color);

                if (paletteIndex != -1 && paletteIndex != 0)
                {
                    spritePixelDataString += ((byte)paletteIndex).ToString() + ",";
                }
            }
        }
        Debug.Log(spritePixelDataString);

        Assert.AreEqual(pixelDataString, spritePixelDataString);
        yield return new WaitForEndOfFrame();
    }

    int ClosestColor(List<Color> colors, Color target)
    {
        var colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
        return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs && n.a != 0);
    }

    int ColorDiff(Color32 c1, Color32 c2)
    {
        return (int)Math.Sqrt((c1.r - c2.r) * (c1.r - c2.r)
                               + (c1.g - c2.g) * (c1.g - c2.g)
                               + (c1.b - c2.b) * (c1.b - c2.b));
    }

    [UnityTest]
    public IEnumerator TestIsSpritesetDataImageData()
    {
        GameSettings.file = Application.persistentDataPath + "/" + GameSettings.fileName + ".json";
        var instance = Rayman1BinaryAnimation.Instance;

        Design raymanDes = Rayman1BinaryAnimation.allfix.DesItems[0];

        yield return new WaitForSeconds(3f);

        Dictionary<int, UnityEngine.Sprite> spriteset = instance.LoadSpritesetFromBinary(0);
        byte[] newImageData = instance.SaveSpriteset(spriteset, raymanDes, false);
        instance.ConvertImageData(ref newImageData);

        Debug.Log(raymanDes.ImageData.Length + " original length, " + newImageData.Length + " new length");
        Assert.AreNotEqual(raymanDes.ImageData, newImageData);
        yield return new WaitForEndOfFrame();
    }

    [UnityTest]
    public IEnumerator TestSpriteSizeRecalculationResult()
    {
        GameSettings.file = Application.persistentDataPath + "/" + GameSettings.fileName + ".json";
        var instance = Rayman1BinaryAnimation.Instance;

        Design raymanDes = Rayman1BinaryAnimation.allfix.DesItems[0];

        byte[][] originalSpriteData = new byte[raymanDes.Sprites.Length][];
        for (int i = 0; i < originalSpriteData.Length; i++)
        {
            originalSpriteData[i] = new byte[]
            {
                raymanDes.Sprites[i].SpriteWidth,
                raymanDes.Sprites[i].SpriteHeight,
                raymanDes.Sprites[i].SpriteXPosition,
                raymanDes.Sprites[i].SpriteYPosition,
            };
        }

        Dictionary<int, UnityEngine.Sprite> spriteset = instance.LoadSpritesetFromBinary(0);
        byte[] newImageData = instance.SaveSpriteset(spriteset, raymanDes, true);
        instance.ConvertImageData(ref newImageData);
        raymanDes.ImageData = newImageData;

        byte[][] newSpriteData = new byte[raymanDes.Sprites.Length][];
        for (int i = 0; i < newSpriteData.Length; i++)
        {
            newSpriteData[i] = new byte[]
            {
                raymanDes.Sprites[i].SpriteWidth,
                raymanDes.Sprites[i].SpriteHeight,
                raymanDes.Sprites[i].SpriteXPosition,
                raymanDes.Sprites[i].SpriteYPosition,
            };
        }

        Assert.AreEqual(originalSpriteData, newSpriteData);
        yield return new WaitForEndOfFrame();
    }

    [UnityTest]
    public IEnumerator TestGetAnimationDesigns()
    {
        GameSettings.file = Application.persistentDataPath + "/" + GameSettings.fileName + ".json";
        var instance = Rayman1BinaryAnimation.Instance;

        for (int i = 0; i < Rayman1BinaryAnimation.allfix.DesItems.Length; i++)
        {
            if (Rayman1BinaryAnimation.allfix.DesItems[i].IsAnimatedSprite)
            {
                Debug.Log(i + " allfix is animated");
            }
            else
            {
                Debug.Log(i + " allfix is NOT animated");
            }
        }

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < Rayman1BinaryAnimation.worlds[0].DesItems.Length; i++)
        {
            if (Rayman1BinaryAnimation.worlds[0].DesItems[i].IsAnimatedSprite)
            {
                Debug.Log("1 - " + i + " is animated");
            }
            else
            {
                Debug.Log("1 - " + i + " is NOT animated");
            }
        }

        for (int i = 0; i < Rayman1BinaryAnimation.worlds[1].DesItems.Length; i++)
        {
            if (Rayman1BinaryAnimation.worlds[1].DesItems[i].IsAnimatedSprite)
            {
                Debug.Log("2 - " + i + " is animated");
            }
            else
            {
                Debug.Log("2 - " + i + " is NOT animated");
            }
        }


        yield return new WaitForEndOfFrame();
    }
}
