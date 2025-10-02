using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public UnityEvent initializeSettings;

    private Dropdown currentLanguageDD;

    private void Awake()
    {
    }

    public void Initialize()
    {
        initializeSettings.Invoke();
    }

    public void OpenSettingsFile()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            Arguments = Application.persistentDataPath + "/RaymanimatorSettings.json",
            FileName = "notepad.exe",
        };

        Process.Start(startInfo);
    }
}
