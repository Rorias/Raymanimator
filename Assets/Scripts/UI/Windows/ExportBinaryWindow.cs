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
    public TMP_InputField pixelSizeIF;
    public ButtonPlus exportButton;
    public ButtonPlus cancelButton;

    private GameManager gameManager;
    private AnimationManager animManager;

    private bool animations = false;
    private bool visuals = false;
    private bool collisions = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        animManager = AnimationManager.Instance;

        spritesetVisualsToggle.onValueChanged.AddListener((_state) => { SetVisualsBool(_state); });
        spritesetCollisionsToggle.onValueChanged.AddListener((_state) => { SetCollisionsBool(_state); });
    }

    private void Start()
    {
        cancelButton.onClick.AddListener(() => { CloseWindow(); });
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
        exportButton.onClick.AddListener(() => { ExportToBinary(); });
    }

    public void ExportToBinary()
    {
        animations = animDataToggle.isOn;
        visuals = spritesetVisualsToggle.isOn;
        collisions = spritesetCollisionsToggle.isOn;
        float pixelSize = string.IsNullOrWhiteSpace(pixelSizeIF.text) ? 16.0f : gameManager.ParseToSingle(pixelSizeIF.text);
        animManager.SaveToBinary(gameManager.currentAnimation, animations, visuals, collisions, pixelSize);
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
}
