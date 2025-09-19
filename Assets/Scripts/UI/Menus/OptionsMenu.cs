using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private GameManager gameManager;
    private GameSettings settings;
    private ThemeController themeController;

    //Settings UI elements
    //Personal
    private Dropdown currentLanguageDD;
    private TMP_InputField animationsPathIF;
    //Editor
    public TMP_Dropdown resolutionDD;
    private TMP_Dropdown editorThemeDD;

    private void Awake()
    {
        themeController = FindObjectOfType<ThemeController>();

        //animationsPathIF = GameObject.Find("AnimationsPath").GetComponent<TMP_InputField>();
        //animationsPathIF.onValueChanged.AddListener(delegate { SetAnimationsPathViaBrowse(); });
        //animationsPathIF.onEndEdit.AddListener(delegate { SetAnimationsPath(); });
        //
        //editorThemeDD = GameObject.Find("ThemeDD").GetComponent<TMP_Dropdown>();
        //editorThemeDD.onValueChanged.AddListener(delegate { SetEditorTheme(); });
    }

    public void Initialize()
    {
        LoadAnimationsSettings();
        LoadResSettings();
        LoadThemeSettings();
    }

    private void LoadAnimationsSettings()
    {
        if (!string.IsNullOrWhiteSpace(settings.animationsPath))
        {
            if (Directory.Exists(settings.animationsPath))
            {
                animationsPathIF.text = settings.animationsPath;
            }
            else
            {
                UnityEngine.Debug.Log("Animations path doesn't exist or has been changed.");
                DebugHelper.Log("Animations path doesn't exist or has been changed.");
            }
        }
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

    private void LoadThemeSettings()
    {
        editorThemeDD.ClearOptions();

        List<string> themes = new List<string>();

        for (int i = 0; i < Enum.GetNames(typeof(GameSettings.Themes)).Length; i++)
        {
            themes.Add(Enum.GetNames(typeof(GameSettings.Themes))[i]);
        }

        editorThemeDD.AddOptions(themes);
        editorThemeDD.value = (int)settings.editorTheme;
    }

    public void SetEditorTheme()
    {
        settings.editorTheme = (GameSettings.Themes)editorThemeDD.value;
        settings.SaveSettings();
        themeController.UpdateTheme();
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
