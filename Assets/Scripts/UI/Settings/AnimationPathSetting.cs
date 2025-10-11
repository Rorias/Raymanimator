using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class AnimationPathSetting : Settings
{
    public TMP_InputField animationPathIF;

    protected override void Awake()
    {
        base.Awake();

        animationPathIF.onValueChanged.AddListener(delegate { SetAnimationPathViaBrowse(); });
        animationPathIF.onEndEdit.AddListener(delegate { SetAnimationPath(); });
    }

    private void Start()
    {
        LoadAnimationsSettings();
    }

    public void LoadAnimationsSettings()
    {
        if (string.IsNullOrWhiteSpace(settings.animationsPath) || !Directory.Exists(settings.animationsPath))
        {
            Debug.Log("Animations path doesn't exist or has been changed.");
            DebugHelper.Log("Animations path doesn't exist or has been changed.");
            return;
        }

        animationPathIF.text = settings.animationsPath;
    }

    public void SetAnimationPathViaBrowse()
    {
        if (!SetAnimationPath())
        {
            settings.animationsPath = "";
            settings.SaveSettings();
        }
    }

    public bool SetAnimationPath()
    {
        string animationPath = animationPathIF.text;

        if (string.IsNullOrWhiteSpace(animationPath) || !Directory.Exists(animationPath))
        {
            Debug.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
            DebugHelper.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
            return false;
        }

        settings.animationsPath = animationPath;
        settings.SaveSettings();
        return true;
    }
}
