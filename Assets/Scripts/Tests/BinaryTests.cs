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

        var palette = Rayman1BinaryAnimation.world1levels[0].MapInfo.Palettes.First();
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
        yield return null;
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

        Dictionary<int, UnityEngine.Sprite> spriteset = instance.LoadSpritesetFromBinary(0);
        byte[] newImageData = instance.SaveSpriteset(spriteset, raymanDes);
        instance.ConvertImageData(0);

        //byte[] tempPrintOld = new byte[1200];
        //Array.Copy(raymanDes.ImageData, 39500, tempPrintOld, 0, tempPrintOld.Length);
       // byte[] tempPrintNew = new byte[1200];
        //Array.Copy(newImageData, 39500, tempPrintNew, 0, tempPrintNew.Length);
        //string t1 = "";
        //for (int i = 0; i < tempPrintOld.Length; i++)
        //{
        //    t1 += tempPrintOld[i].ToString("00") + ",";
        //    if (i > 0 & i % 24 == 0) { t1 += "\n"; }
        //}
        //Debug.Log(t1);
        //string t2 = "";
        //for (int i = 0; i < tempPrintNew.Length; i++)
        //{
        //    t2 += tempPrintNew[i].ToString("00") + ",";
        //    if (i > 0 & i % 24 == 0) { t2 += "\n"; }
        //}
        //Debug.Log(t2);

        //int a1 = 0;
        //int a2 = 0;
        //for (int i = 0; i < Rayman1BinaryAnimation.newLengths.Count; i++)
        //{
        //    bool wrong = false;
        //    if (Rayman1BinaryAnimation.oldLengths[i] != Rayman1BinaryAnimation.newLengths[i])
        //    {
        //        wrong = true;
        //    }
        //    a1 += Rayman1BinaryAnimation.oldLengths[i];
        //    a2 += Rayman1BinaryAnimation.newLengths[i];
        //    if (i > 0 && Rayman1BinaryAnimation.bufferOffsetIndex[i] - Rayman1BinaryAnimation.oldLengths[i - 1] != Rayman1BinaryAnimation.bufferOffsetIndex[i - 1])
        //    {
        //        Debug.Log("Buffer offset differs by: " + (Rayman1BinaryAnimation.bufferOffsetIndex[i] - Rayman1BinaryAnimation.bufferOffsetIndex[i - 1]));
        //    }

        //    Debug.Log((wrong ? "<color=red>" : "") + i + ": " + Rayman1BinaryAnimation.oldLengths[i] + ", " + Rayman1BinaryAnimation.newLengths[i] + ", offset: " + Rayman1BinaryAnimation.bufferOffsetIndex[i]);
        //}

        //Debug.Log(a1 + " old length, " + a2 + " new length");
        Debug.Log(raymanDes.ImageData.Length + " original length, " + newImageData.Length + " new length");
        Assert.AreEqual(raymanDes.ImageData, newImageData);

        yield return null;
    }
}
