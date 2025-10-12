using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThemeController : MonoBehaviour
{
    public TMP_FontAsset normalFont;

    private GameSettings settings;

    //Editor style settings
    private Image[] panels;
    private TMP_Dropdown[] dropdowns;
    private TMP_InputField[] inputfields;
    private ButtonPlus[] buttons;
    private Toggle[] toggles;
    private TMP_Text[] labels;
    private List<TMP_FontAsset> defaultFonts = new List<TMP_FontAsset>();

    private Color[] colorSchemeLight = new Color[] { new Color(1, 1, 1), new Color(1, 1, 1), new Color(1, 1, 1), new Color(1, 1, 1, 0.2f), new Color(1, 1, 1), new Color(0.15f, 0.15f, 0.15f, 0) };
    private Color[] colorSchemeDark = new Color[] { new Color(0.15f, 0.15f, 0.15f), new Color(0.1f, 0.1f, 0.1f), new Color(0.1f, 0.1f, 0.1f), new Color(0.1f, 0.1f, 0.1f, 0.65f), new Color(0.15f, 0.15f, 0.15f), new Color(0.85f, 0.85f, 0.85f, 0) };

    private Color[] currentScheme = new Color[6];

    private void Awake()
    {
        settings = GameSettings.Instance;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _lsm)
    {
        panels = FindObjectsByType<Image>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(x => x.tag == "MenuPanel").ToArray();
        dropdowns = FindObjectsByType<TMP_Dropdown>(FindObjectsSortMode.None);
        inputfields = FindObjectsByType<TMP_InputField>(FindObjectsSortMode.None);
        buttons = FindObjectsByType<ButtonPlus>(FindObjectsSortMode.None);
        toggles = FindObjectsByType<Toggle>(FindObjectsSortMode.None);
        labels = FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < inputfields.Length; i++)
        {
            inputfields[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_SelectionCaret>().raycastTarget = false;
        }

        defaultFonts.Clear();
        for (int i = 0; i < labels.Length; i++)
        {
            defaultFonts.Add(labels[i].font);
        }

        UpdateTheme();
        UpdateFonts(settings.normalFont);
    }

    public void UpdateTheme()
    {
        switch (settings.editorTheme)
        {
            case GameSettings.Themes.Light:
                currentScheme = colorSchemeLight;
                break;
            case GameSettings.Themes.Dark:
                currentScheme = colorSchemeDark;
                break;
            case GameSettings.Themes.Colorcoded:
                return;
            default:
                Debug.Log("Settings file is missing or has been tampered with.");
                break;
        }

        foreach (ButtonPlus b in buttons)
        {
            if (b.colors.normalColor == Color.white)
            {
                if (b.GetComponent<Image>())
                {
                    b.GetComponent<Image>().color = currentScheme[0];
                }
            }
        }

        foreach (TMP_InputField IF in inputfields)
        {
            IF.GetComponent<Image>().color = currentScheme[1];
        }

        foreach (TMP_Dropdown dd in dropdowns)
        {
            dd.GetComponent<Image>().color = currentScheme[2];

            dd.transform.Find("Template").GetComponent<Image>().color = currentScheme[0];
            dd.transform.Find("Template").GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = currentScheme[0];
            TMP_Text t = dd.transform.Find("Template").GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<TMP_Text>();
            t.color = currentScheme[5] + new Color(0, 0, 0, t.color.a);
        }

        foreach (Image panel in panels)
        {
            if (panel)
            {
                panel.color = currentScheme[3];
            }
        }

        foreach (Toggle t in toggles)
        {
            t.transform.GetChild(0).GetComponent<Image>().color = currentScheme[4];
        }

        foreach (TMP_Text t in labels)
        {
            if (t.fontStyle != FontStyles.UpperCase)
            {
                t.color = currentScheme[5] + new Color(0, 0, 0, t.color.a);
            }
        }
    }

    public void UpdateFonts(bool _state)
    {
        for (int i = 0; i < labels.Length; i++)
        {
            if (defaultFonts[i] != normalFont)
            {
                labels[i].fontStyle = _state ? FontStyles.Normal : FontStyles.UpperCase;
                labels[i].color = _state ? currentScheme[5] + new Color(0, 0, 0, labels[i].color.a) : new Color(1, 1, 1, 1);
                labels[i].font = _state ? normalFont : defaultFonts[i];
            }
            else
            {
                labels[i].color = currentScheme[5] + new Color(0, 0, 0, labels[i].color.a);
            }
        }
    }
}
