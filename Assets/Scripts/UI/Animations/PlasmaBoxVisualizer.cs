using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PlasmaBoxVisualizer : MonoBehaviour
{
    //95F70, 48 bytes assigned to the array itself originally. Mine has 64 bytes due to alpha.
    private Color32[] PalPlasma = new Color32[16] {
      new Color32(32, 7, 63, 255),
      new Color32(31, 7, 61, 255),
      new Color32(32, 7, 58, 255),
      new Color32(33, 7, 56, 255),
      new Color32(36, 10, 53, 255),
      new Color32(37, 12, 51, 255),
      new Color32(45, 15, 48, 255),
      new Color32(52, 17, 46, 255),
      new Color32(45, 15, 48, 255),
      new Color32(40, 15, 51, 255),
      new Color32(35, 12, 53, 255),
      new Color32(35, 10, 56, 255),
      new Color32(32, 7, 58, 255),
      new Color32(30, 5, 61, 255),
      new Color32(27, 5, 61, 255),
      new Color32(32, 7, 63, 255),
        };

    private unsafe byte* draw_buffer;
    private byte plasma_palette_color_index = 0;

    private byte[] byte_DA43C = new byte[128 * 128];

    public RawImage box;

    public short x;
    public short y;
    public short width;
    public short height;

    private void Awake()
    {
        box.rectTransform.anchoredPosition = new Vector2(x, y);
        box.rectTransform.sizeDelta = new Vector2(width, height);
        box.texture.width = width;
        box.texture.height = height;
    }

    private void Update()
    {
        PlasmaBox(x, y, width, height, 1);
        Texture2D sampleTexture = new Texture2D(2, 2);
        sampleTexture.filterMode = FilterMode.Point;
        //sampleTexture.LoadImage(byteArray);
        box.texture = new Texture2D(width, height, TextureFormat.RGB24, 0, false);
    }

    //i16 = short, u8 = byte. i16 is signed 16bit integer, u8 is unsigned 8bit integer
    private void PlasmaBox(short x, short y, short width, short height, byte a5)
    {
        byte word_DE748 = 0;
        byte word_DE746 = 0;
        byte word_DE74C = 0;
        byte word_DE74A = 0;
        byte word_DE744 = 0;
        byte word_DE742 = 0;
        byte word_DE740 = 0;
        byte word_DE73E = 0;

        if (a5 != 0)
        {
            //memory address indexes of the palette used
            PalPlasma[0][0] += 1;//word_95FA0
            PalPlasma[0][2] -= 2;//word_95FA2
            PalPlasma[1][0] += 1;//word_95FA4
            PalPlasma[1][2] -= 2;//word_95FA6
            PalPlasma[2][0] += 3;//word_95FA8
            PalPlasma[2][2] -= 2;//word_95FAA
            PalPlasma[3][0] += 1;//word_95FAC
            PalPlasma[3][2] -= 3;//word_95FAE
            PalPlasma[4][0] += 5;//word_95FB0
            //?? some addresses, not sure what
            word_DE748 = (byte)(((byte)Math.Sin(PalPlasma[0][0]) + 32) >> 4);
            word_DE746 = (byte)(((byte)Math.Cos(PalPlasma[0][2]) + 32) >> 4);
            word_DE74C = (byte)(((byte)Math.Cos(PalPlasma[1][0]) + 32) >> 4);
            word_DE74A = (byte)(((byte)Math.Sin(PalPlasma[1][2]) + 32) >> 4);
            word_DE744 = (byte)(((byte)Math.Cos(PalPlasma[2][0]) + 32) >> 4);
            word_DE742 = (byte)(((byte)Math.Sin(PalPlasma[2][2]) + 32) >> 4);
            word_DE740 = (byte)(((byte)Math.Cos(PalPlasma[3][0]) + 32) >> 4);
            word_DE73E = (byte)(((byte)Math.Sin(PalPlasma[3][2]) + 32) >> 4);
        }
        x -= 3;
        y -= 3;
        width += 6;
        height += 6;
        if (x < 0)
        {
            width += x;
            x = 0;
        }
        else if (x + width > 320)
        {
            width = (short)(320 - x);
        }
        if (y < 0)
        {
            height += y;
            y = 0;
        }
        else if (y + height < 200)
        {
            height = (short)(200 + y);
        }
        //weird that the last entry is a regular one from the palette
        Plasma(x, y, width, height, word_DE74C, word_DE74A, word_DE748, word_DE746, word_DE744, word_DE742, word_DE740, word_DE73E, PalPlasma[4][0]);
    }

    private void Plasma(short x, short y, short width, short height, byte a5, byte a6, byte a7, byte a8, byte a9, byte a10, byte a11, byte a12, byte a13)
    {
        unsafe
        {
            byte* row = draw_buffer + 320 * y + x;
            uint v17 = (uint)(a6 | (a8 << 8) | (a10 << 16) | (a12 << 24));
            for (int j = 0; j < height; ++j)
            {
                v17 = (v17 + 0x01010101) & 0x7F7F7F7F;
                uint v18 = (uint)(a5 | (a7 << 8) | (a9 << 16) | (a11 << 24));
                //byte* pos = row; we change this to an index counter for the row in the direct call, since this is not C.
                for (int i = 0; i < width; ++i)
                {
                    v18 = (v18 + 0x01010101) & 0x7F7F7F7F;
                    byte v22 = (byte)(a13 + byte_DA43C[128 * (byte)v17 + (byte)v18]);
                    v22 += byte_DA43C[128 * (byte)(v17 >> 8) + (byte)(v18 >> 8)];
                    v22 += byte_DA43C[128 * (byte)(v17 >> 16) + (byte)(v18 >> 16)];
                    v22 += byte_DA43C[128 * (byte)(v17 >> 24) + (byte)(v18 >> 24)];
                    v22 >>= 4;
                    v22 += plasma_palette_color_index;
                    row[j++] = v22;
                }
                row += 320;
            }
        }
    }
}
