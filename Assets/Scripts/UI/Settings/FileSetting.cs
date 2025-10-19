using ImageMagick;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class FileSetting : Settings
{
    public Camera gifCamera;
    public AnimatorController animatorC;

    public ImportWindow importWindow;
    public ButtonPlus importButton;

    private GameManager gameManager;
    private AnimationManager animManager;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameManager.Instance;
        animManager = AnimationManager.Instance;

        importButton.onClick.AddListener(delegate { importWindow.OpenWindow(); });
    }

    public void Save()
    {
        animManager.SaveFile(gameManager.currentAnimation);
    }

    public void SaveAsReverse()
    {
        animManager.SaveFileReversed(gameManager.currentAnimation);
    }

    public void SaveAsDouble()
    {
        animManager.SaveFileDoubled(gameManager.currentAnimation);
    }

    public void SaveToBinary()
    {
        animManager.SaveToBinary(gameManager.currentAnimation);
    }

    public void SaveAsImageListZip()
    {
        //Set GIF view to real view
        gifCamera.orthographicSize = Camera.main.orthographicSize;
        gifCamera.transform.position = Camera.main.transform.position;

        animatorC.AnimToZipStart();

        StartCoroutine(CreateImageList());
    }

    private IEnumerator CreateImageList()
    {
        Animation anim = gameManager.currentAnimation;

        float multiplier = Camera.main.orthographicSize / 2.0f;

        float xRatio = (float)anim.gridSizeX / anim.gridSizeY;
        float yRatio = (float)anim.gridSizeY / anim.gridSizeX;

        if (xRatio > yRatio) { yRatio = 1; }
        if (yRatio > xRatio) { xRatio = 1; }

        int xSize = Mathf.RoundToInt(64f * xRatio * multiplier);
        int ySize = Mathf.RoundToInt(64f * yRatio * multiplier);

        using MagickImageCollection imgColl = new MagickImageCollection();

        for (int animFrames = 0; animFrames < anim.maxFrameCount; animFrames++)
        {
            animatorC.frameSelectSlider.value = animFrames;
            yield return new WaitForEndOfFrame();

            RenderTexture screenTexture = new RenderTexture(xSize, ySize, 16);
            gifCamera.targetTexture = screenTexture;
            RenderTexture.active = screenTexture;
            gifCamera.Render();
            Texture2D renderedTexture = new Texture2D(xSize, ySize);
            renderedTexture.ReadPixels(new Rect(0, 0, xSize, ySize), 0, 0);
            RenderTexture.active = null;
            gifCamera.targetTexture = null;
            byte[] byteArray = renderedTexture.EncodeToPNG();
            imgColl.Add(new MagickImage(byteArray));
            imgColl[animFrames].AnimationDelay = 1;
            imgColl[animFrames].AnimationTicksPerSecond = settings.lastPlaybackSpeed;
            imgColl[animFrames].GifDisposeMethod = GifDisposeMethod.Background;
        }

        imgColl.Write(settings.animationsPath + "/" + anim.animationName + ".gif");

        DebugHelper.Log(gameManager.currentAnimation.animationName + " saved as gif!");
        animatorC.AnimToZipEnd();
    }
}
