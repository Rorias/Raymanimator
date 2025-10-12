using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : Settings
{
    public TMP_Dropdown resolutionDD;

    protected override void Awake()
    {
        base.Awake();

        resolutionDD.onValueChanged.AddListener(delegate { SetResolution(); });
    }

    private void Start()
    {
        LoadResSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(Screen.fullScreenMode);
        }
    }

    public void LoadResSettings()
    {
        resolutionDD.ClearOptions();

        Dictionary<int, string> resOptions = CreateResolutions();

        resolutionDD.AddOptions(resOptions.Values.ToList());

        int i = 0;
        foreach (KeyValuePair<int, string> kvp in resOptions)
        {
            resolutionDD.options[i].text = kvp.Value;
            i++;
        }

        int option = settings.resNumber;
        if (settings.resNumber == -1)
        {
            option = resOptions.First(x => x.Value.StartsWith(Screen.currentResolution.width.ToString()) && x.Value.EndsWith(Screen.currentResolution.refreshRateRatio.ToString())).Key;
        }

        settings.fullScreen = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
        settings.SaveSettings();
        resolutionDD.value = option;
    }

    public void SetResolution()
    {
        settings.resNumber = resolutionDD.value;

        Screen.fullScreenMode = settings.fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Resolution[] resolutions = Screen.resolutions;
        Dictionary<int, string> resOptions = CreateResolutions();

        foreach (KeyValuePair<int, string> kvp in resOptions)
        {
            if (kvp.Key == settings.resNumber)
            {
                Screen.SetResolution(resolutions[kvp.Key].width, resolutions[kvp.Key].height, Screen.fullScreenMode, resolutions[kvp.Key].refreshRateRatio);
                break;
            }
        }

        settings.SaveSettings();
    }

    private Dictionary<int, string> CreateResolutions()
    {
        Dictionary<int, string> resOptions = new Dictionary<int, string>();

        Resolution[] resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].height % 9 == 0 && resolutions[i].width % 16 == 0 && Mathf.Approximately((float)resolutions[i].width / (float)resolutions[i].height, 1.777778f))
            {
                resOptions.Add(i, resolutions[i].width + "x" + resolutions[i].height + " : " + resolutions[i].refreshRateRatio.value);
            }
        }

        return resOptions;
    }
}
