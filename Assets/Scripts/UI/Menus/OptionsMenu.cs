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

    private GameManager gameManager;
    private GameSettings settings;

    //Settings UI elements
    //Personal
    private Dropdown currentLanguageDD;
    //Editor
    public TMP_Dropdown resolutionDD;

    private void Awake()
    {
        settings = GameSettings.Instance;
    }

    public void Initialize()
    {
        LoadResSettings();
        initializeSettings.Invoke();
    }

    private void LoadResSettings()
    {
        resolutionDD.ClearOptions();

        Resolution[] resolutions = Screen.resolutions;

        List<string> resOptions = new List<string>();

        foreach (Resolution res in resolutions)
        {
            if (res.height % 9 == 0 && res.width % 16 == 0 && Mathf.Approximately((float)res.width / (float)res.height, 1.777778f))
            {
                resOptions.Add(res.width + "x" + res.height + " : " + res.refreshRateRatio.value);
            }
        }

        resolutionDD.AddOptions(resOptions);

        for (int i = 0; i < resOptions.Count; i++)
        {
            resolutionDD.options[i].text = resOptions[i];
        }

        resolutionDD.value = settings.resNumber;

        SetResolution();
    }

    public void SetResolution()
    {
        settings.resNumber = resolutionDD.value;

        Screen.fullScreenMode = settings.fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Resolution[] resolutions = Screen.resolutions;
        List<int> acceptedResNumbers = new List<int>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].height % 9 == 0 && resolutions[i].width % 16 == 0 && Mathf.Approximately((float)resolutions[i].width / (float)resolutions[i].height, 1.777778f))
            {
                acceptedResNumbers.Add(i);
            }
        }

        for (int i = 0; i < acceptedResNumbers.Count; i++)
        {
            if (i == settings.resNumber)
            {
                Screen.SetResolution(resolutions[acceptedResNumbers[i]].width, resolutions[acceptedResNumbers[i]].height, Screen.fullScreenMode, resolutions[acceptedResNumbers[i]].refreshRateRatio);
                break;
            }
        }

        settings.SaveSettings();
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
