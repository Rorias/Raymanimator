using BinarySerializer;
using BinarySerializer.Ray1;
using BinarySerializer.Ray1.PC;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public sealed class Rayman1BinaryAnimation : MonoBehaviour
{
    #region Singleton
    private static Rayman1BinaryAnimation _instance;
    private static object _lock = new object();

    private Rayman1BinaryAnimation() { }

    public static Rayman1BinaryAnimation Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new Rayman1BinaryAnimation();
                LoadBinaryAnimations();
            }
            return _instance;
        }
    }
    #endregion

    private static string basePath = @"C:\UbiSoft\UBISOFT\RAYMAN";
    private static string allfixFilePath = @"PCMAP\ALLFIX.DAT";
    private static string world1FilePath = @"PCMAP\RAY1.WLD";

    [NonSerialized] private static Context context;
    [NonSerialized] private static AllfixFile allfix;
    [NonSerialized] private static WorldFile world1;

    private static void LoadBinaryAnimations()
    {
        context = new Context(basePath);
        context.AddSettings(new Ray1Settings(Ray1EngineVersion.PC));

        using (context)
        {
            context.AddFile(new LinearFile(context, allfixFilePath));
            allfix = FileFactory.Read<AllfixFile>(context, allfixFilePath);
        }

        using (context)
        {
            context.AddFile(new LinearFile(context, world1FilePath));
            world1 = FileFactory.Read<WorldFile>(context, world1FilePath);
        }
    }

    public Animation GetRaymAnimationFromBinary(string _animName, int _objectIndex, int _animIndex)
    {
        BinarySerializer.Ray1.Animation binaryAnim = allfix.DesItems[_objectIndex].Animations[_animIndex];

        Debug.Log("Succesfully retrieved animation data.");

        Animation rayman1Anim = new Animation();
        rayman1Anim.animationName = _animName;
        rayman1Anim.maxFrameCount = binaryAnim.FramesCount;
        rayman1Anim.maxPartCount = binaryAnim.LayersCount;
        rayman1Anim.usedSpriteset = "Rayman";

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
                p.xPos = animLayer.XPosition / 16f;
                p.yPos = (255 - (animLayer.YPosition / 16f)) - 255;

                f.frameParts.Add(p);
            }

            rayman1Anim.frames.Add(f);
        }

        return rayman1Anim;
    }

    public void SaveRaymAnimationToBinary(Animation _rayman1Anim)
    {
        BinarySerializer.Ray1.Animation binaryAnim = allfix.DesItems[0].Animations[0];

        //Design raymanDes = allfix.DesItems[0];
        //BinarySerializer.Ray1.Sprite sprite = raymanDes.Sprites[1];
        //byte[] pixels = new byte[sprite.Width * sprite.Height];
        //Array.Copy(raymanDes.ImageData, sprite.ImageBufferOffset, pixels, 0, pixels.Length);

        for (int frameIndex = 0; frameIndex < binaryAnim.FramesCount; frameIndex++)
        {
            Frame f = _rayman1Anim.frames[frameIndex];
            for (int layerIndex = 0; layerIndex < binaryAnim.LayersCount; layerIndex++)
            {
                Part p = f.frameParts[layerIndex];
                AnimationLayer animLayer = binaryAnim.Layers[frameIndex * binaryAnim.LayersCount + layerIndex];

                layerIndex = p.partID;
                animLayer.SpriteIndex = (byte)(p.partIndex + 1);
                animLayer.FlipX = p.flipX;
                animLayer.FlipY = p.flipY;
                animLayer.XPosition = (byte)(p.xPos * 16f);
                animLayer.YPosition = (byte)((255 - (p.yPos + 255)) * 16f);
            }
        }

        using (context)
        {
            FileFactory.Write<AllfixFile>(context, allfixFilePath);
        }
    }
}
