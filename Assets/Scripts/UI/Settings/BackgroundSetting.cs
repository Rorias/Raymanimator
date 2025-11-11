using System.Collections.Generic;
using System.IO;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackgroundSettings : Settings
{
    public GameObject backgroundPrefab;

    public TMP_InputField backgroundImagePathIF;
    public TMP_DropdownPlus backgroundsDD;

    public ColorSlider colorSlider;

    private GameManager gameManager;
    private Camera cam;

    private Background background;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameManager.Instance;
        cam = Camera.main;

        if (backgroundImagePathIF != null)
        {
            backgroundImagePathIF.onValueChanged.AddListener(delegate { SetBackgroundPathViaBrowse(); });
            backgroundImagePathIF.onEndEdit.AddListener(delegate { SetBackgroundPath(); });
        }

        if (backgroundsDD != null)
        {
            backgroundsDD.onValueChanged.AddListener(delegate { SetCurrentBackground(); });
        }

        colorSlider.UpdateRed += SetColorRed;
        colorSlider.UpdateGreen += BGColorGreen;
        colorSlider.UpdateBlue += BGColorBlue;

        colorSlider.SetRed += SaveColor;
        colorSlider.SetGreen += SaveColor;
        colorSlider.SetBlue += SaveColor;

        colorSlider.Reset += ResetColor;
    }

    private void Start()
    {
        colorSlider.SetColorSliders(settings.bgColor);
        if (backgroundImagePathIF != null)
        {
            LoadBackgroundSettings();
        }
        if (backgroundsDD != null)
        {
            uiUtility.LoadBackgrounds(gameManager.backgroundImages);
            InitializeBackgroundsDropdown();
            CreateBackground(0);
        }
    }

    public void LoadBackgroundSettings()
    {
        if (string.IsNullOrWhiteSpace(settings.backgroundPath) || !Directory.Exists(settings.backgroundPath))
        {
            Debug.Log("Background path doesn't exist or has been changed.");
            DebugHelper.Log("Background path doesn't exist or has been changed.");
            return;
        }

        backgroundImagePathIF.text = settings.backgroundPath;
    }

    public void SetBackgroundPathViaBrowse()
    {
        if (!string.IsNullOrWhiteSpace(backgroundImagePathIF.text) && (backgroundImagePathIF.text[^1] == '/' || backgroundImagePathIF.text[^1] == '\\'))
        {
            SetBackgroundPath();
        }
    }

    public void SetBackgroundPath()
    {
        string backgroundPath = backgroundImagePathIF.text;

        if (string.IsNullOrWhiteSpace(backgroundPath) || !Directory.Exists(backgroundPath))
        {
            Debug.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
            DebugHelper.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
            return;
        }

        settings.backgroundPath = backgroundPath;
        settings.SaveSettings();
    }

    public void InitializeBackgroundsDropdown()
    {
        Dictionary<int, Sprite> possibleBackgrounds = new Dictionary<int, Sprite>();

        if (gameManager.backgroundImages.Count > 0)
        {
            foreach (KeyValuePair<int, Sprite> mapping in gameManager.backgroundImages)
            {
                possibleBackgrounds.Add(mapping.Key, mapping.Value);
            }
        }
        else
        {
            Debug.Log("No backgrounds found in selected folder.");
            DebugHelper.Log("No backgrounds found in selected folder.");
            return;
        }

        if (possibleBackgrounds.Count > 0)
        {
            backgroundsDD.AddOptions(possibleBackgrounds.Select(x => x.Value).ToList());
        }
        else
        {
            Debug.Log("Possible backgrounds list is empty.");
            DebugHelper.Log("Possible backgrounds list is empty.");
            return;
        }

        foreach (KeyValuePair<int, Sprite> mapping in possibleBackgrounds)
        {
            backgroundsDD.options[mapping.Key].text = possibleBackgrounds[mapping.Key].name;
            backgroundsDD.options[mapping.Key].image = possibleBackgrounds[mapping.Key];
        }
    }

    private void CreateBackground(int _part)
    {
        GameObject bg = Instantiate(backgroundPrefab);
        bg.name = "Background" + _part;
        background = bg.GetComponent<Background>();
    }

    public void SetCurrentBackground()
    {
        if (string.IsNullOrWhiteSpace(settings.backgroundPath) || !Directory.Exists(settings.backgroundPath))
        {
            Debug.Log("Background path cannot be found. Check if you set it correctly.");
            DebugHelper.Log("Background path cannot be found. Check if you set it correctly.");
            return;
        }

        background.sr.sprite = backgroundsDD.options[backgroundsDD.value].image;
    }

    public void SetColorRed()
    {
        cam.backgroundColor = colorSlider.GetColorBarRed(cam.backgroundColor);
    }

    public void BGColorGreen()
    {
        cam.backgroundColor = colorSlider.GetColorBarGreen(cam.backgroundColor);
    }

    public void BGColorBlue()
    {
        cam.backgroundColor = colorSlider.GetColorBarBlue(cam.backgroundColor);
    }

    public void SaveColor()
    {
        settings.bgColor = cam.backgroundColor;
        settings.SaveSettings();
    }

    public void ResetColor()
    {
        cam.backgroundColor = new Color32(50, 75, 150, 255);
        colorSlider.SetColorSliders(cam.backgroundColor);
        SaveColor();
    }
}
