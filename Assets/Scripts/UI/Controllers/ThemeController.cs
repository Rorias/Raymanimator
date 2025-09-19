using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThemeController : MonoBehaviour
{
    private GameSettings settings;

    //Editor style settings
    private GameObject[] panels;
    private TMP_Dropdown[] dropdowns;
    private TMP_InputField[] inputfields;
    private Button[] buttons;
    private Toggle[] toggles;
    private TMP_Text[] labels;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _lsm)
    {
        settings = GameSettings.Instance;

        panels = GameObject.FindGameObjectsWithTag("MenuPanel");
        dropdowns = FindObjectsOfType<TMP_Dropdown>();
        inputfields = FindObjectsOfType<TMP_InputField>();
        buttons = FindObjectsOfType<Button>();
        toggles = FindObjectsOfType<Toggle>();
        labels = FindObjectsOfType<TMP_Text>();

        UpdateTheme();
    }

    public void UpdateTheme()
    {
        Color[] colorScheme = new Color[6];

        switch (settings.editorTheme)
        {
            case GameSettings.Themes.Light:
                colorScheme[0] = new Color(1, 1, 1);
                colorScheme[1] = new Color(1, 1, 1);
                colorScheme[2] = new Color(1, 1, 1);
                colorScheme[3] = new Color(1, 1, 1, 0.2f);
                colorScheme[4] = new Color(1, 1, 1);
                colorScheme[5] = new Color(0.15f, 0.15f, 0.15f, 0);
                break;
            case GameSettings.Themes.Dark:
                colorScheme[0] = new Color(0.15f, 0.15f, 0.15f);
                colorScheme[1] = new Color(0.1f, 0.1f, 0.1f);
                colorScheme[2] = new Color(0.1f, 0.1f, 0.1f);
                colorScheme[3] = new Color(0.1f, 0.1f, 0.1f, 0.65f);
                colorScheme[4] = new Color(0.15f, 0.15f, 0.15f);
                colorScheme[5] = new Color(0.85f, 0.85f, 0.85f, 0);
                break;
            default:
                Debug.Log("Settings file is missing or has been tampered with.");
                break;
        }

        foreach (Button b in buttons)
        {
            if (b.colors.normalColor == Color.white)
            {
                b.GetComponent<Image>().color = colorScheme[0];
            }
        }

        foreach (TMP_InputField IF in inputfields)
        {
            IF.GetComponent<Image>().color = colorScheme[1];
        }

        foreach (TMP_Dropdown dd in dropdowns)
        {
            dd.GetComponent<Image>().color = colorScheme[2];

            dd.transform.Find("Template").GetComponent<Image>().color = colorScheme[0];
            dd.transform.Find("Template").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = colorScheme[0];
            TMP_Text t = dd.transform.Find("Template").GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<TMP_Text>();
            t.color = colorScheme[5] + new Color(0, 0, 0, t.color.a);
        }

        foreach (GameObject panel in panels)
        {
            panel.GetComponent<Image>().color = colorScheme[3];
        }

        foreach (Toggle t in toggles)
        {
            t.transform.GetChild(0).GetComponent<Image>().color = colorScheme[4];
        }

        foreach (TMP_Text t in labels)
        {
            if (t.name != "TooltipText")
            {
                t.color = colorScheme[5] + new Color(0, 0, 0, t.color.a);
            }
        }
    }
}
