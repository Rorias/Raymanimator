using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ExportBinaryWindow : Raymanimator
{
    public Toggle animDataToggle;
    public Toggle spritesetVisualsToggle;
    public Toggle spritesetCollisionsToggle;
    public Toggle advancedSettingsToggle;
    [Header("Advanced settings")]
    public GameObject pixelSizeIFHolder;
    public TMP_InputField pixelSizeIF;
    [Space]
    public ButtonPlus exportButton;
    public ButtonPlus cancelButton;

    private GameManager gameManager;
    private AnimationManager animManager;
    private GameSettings settings;
    private ConfirmWindow exportConfirmWindow;

    private bool animations = false;
    private bool visuals = false;
    private bool collisions = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        animManager = AnimationManager.Instance;
        settings = GameSettings.Instance;

        spritesetVisualsToggle.onValueChanged.AddListener((_state) => { SetVisualsBool(_state); });
        spritesetCollisionsToggle.onValueChanged.AddListener((_state) => { SetCollisionsBool(_state); });
        advancedSettingsToggle.onValueChanged.AddListener((_state) => { SetAdvancedSettings(_state); });

        exportConfirmWindow = FindObjectOfType<ConfirmWindow>(true);
    }

    private void Start()
    {
        cancelButton.onClick.AddListener(() => { CloseWindow(); });
        SetAdvancedSettings(false);
        CloseWindow();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        exportButton.onClick.RemoveAllListeners();
        exportButton.onClick.AddListener(delegate
        { //TODO: Add check for changes in animation to show popup, otherwise no need
            if (settings.showExportBackupWarning)
            {
                exportConfirmWindow.OpenWindow("Saving to binary will overwrite original gamefiles. Please make a backup.", ExportToBinary);
                CloseWindow();
                exportConfirmWindow.noButton.onClick.RemoveAllListeners();
                exportConfirmWindow.noButton.onClick.AddListener(delegate { OpenWindow(); exportConfirmWindow.CloseWindow(); });
            }
            else
            {
                ExportToBinary(false);
            }
        });
    }

    public void ExportToBinary(bool _showWarning)
    {
        animations = animDataToggle.isOn;
        visuals = spritesetVisualsToggle.isOn;
        collisions = spritesetCollisionsToggle.isOn;
        float pixelSize = string.IsNullOrWhiteSpace(pixelSizeIF.text) ? 16.0f : gameManager.ParseToSingle(pixelSizeIF.text);
        animManager.SaveToBinary(gameManager.currentAnimation, animations, visuals, collisions, pixelSize);

        if (!_showWarning && settings.showExportBackupWarning)
        {
            settings.showExportBackupWarning = _showWarning;
            settings.SaveSettings();
        }

        CloseWindow();
    }

    private void SetVisualsBool(bool _state)
    {
        if (!_state && spritesetCollisionsToggle.isOn)
        {
            spritesetCollisionsToggle.isOn = false;
            return;
        }
    }

    private void SetCollisionsBool(bool _state)
    {
        if (_state && !spritesetVisualsToggle.isOn)
        {
            spritesetVisualsToggle.isOn = true;
            return;
        }
    }

    private void SetAdvancedSettings(bool _state)
    {
        pixelSizeIFHolder.SetActive(_state);
    }
}
