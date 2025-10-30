using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ImportWindow : Raymanimator
{
    public AnimatorController animatorC;
    [Space]
    public TMP_DropdownPlus loadAnimsDD;
    public TMP_InputField startIF;
    public TMP_InputField endIF;
    public TMP_InputField insertIF;
    public ButtonPlus importButton;
    public ButtonPlus cancelButton;

    private GameManager gameManager;
    private GameSettings settings;
    private AnimationManager animManager;

    private List<string> loadableAnims = new List<string>();
    private Animation importAnim;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
        animManager = AnimationManager.Instance;

        importAnim = new Animation();
        loadAnimsDD.onValueChanged.AddListener(delegate { GetAnimationName(); animManager.LoadAnimation(importAnim); ShowValues(); });
    }

    private void Start()
    {
        cancelButton.onClick.AddListener(() => { CloseWindow(); });
        GetLoadableAnimations();
        CloseWindow();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        importButton.onClick.RemoveAllListeners();
        importButton.onClick.AddListener(() => { Import(); });
    }

    private void GetLoadableAnimations()
    {
        if (string.IsNullOrWhiteSpace(settings.animationsPath) || !Directory.Exists(settings.animationsPath))
        {
            Debug.Log("Animations path is not set. Please select a path in the options menu.");
            DebugHelper.Log("Animations path is not set. Please select a path in the options menu.");
            return;
        }

        string[] fileNames = Directory.GetFiles(settings.animationsPath, "*.xml");

        if (loadableAnims.Count > 0) { loadableAnims.Clear(); }

        foreach (string s in fileNames)
        {
            loadableAnims.Add(Path.GetFileNameWithoutExtension(s));
        }

        InitializeDropdown();
    }

    private void InitializeDropdown()
    {
        if (loadAnimsDD.options.Count > 0) { loadAnimsDD.ClearOptions(); }

        loadAnimsDD.AddOptions(loadableAnims);

        for (int i = 0; i < loadableAnims.Count; i++)
        {
            loadAnimsDD.options[i].text = loadableAnims[i];
        }
    }

    private void GetAnimationName()
    {
        importAnim.animationName = loadAnimsDD.captionText.text;
    }

    private void ShowValues()
    {
        startIF.text = "0";
        endIF.text = (importAnim.maxFrameCount - 1).ToString();
        insertIF.text = "0";
    }

    private void Import()
    {
        if (string.IsNullOrWhiteSpace(startIF.text) || string.IsNullOrWhiteSpace(endIF.text) || string.IsNullOrWhiteSpace(insertIF.text))
        {
            DebugHelper.Log("All fields need to be set for a proper import.", DebugHelper.Severity.warning);
            return;
        }

        int startFrame = Convert.ToInt32(startIF.text);
        int endFrame = Convert.ToInt32(endIF.text);
        int insertFrame = Convert.ToInt32(insertIF.text);

        if (startFrame > endFrame)
        {
            DebugHelper.Log("Start frame can't be after end frame... duh.", DebugHelper.Severity.warning);
            return;
        }

        if (endFrame > importAnim.maxFrameCount)
        {
            DebugHelper.Log("End frame can't be past max frames. Set to max.", DebugHelper.Severity.warning);
            endIF.text = (importAnim.maxFrameCount).ToString();
            return;
        }

        if (insertFrame > gameManager.currentAnimation.maxFrameCount)
        {
            DebugHelper.Log("Insert frame can't be past max frames. Set to max.", DebugHelper.Severity.warning);
            insertIF.text = (gameManager.currentAnimation.maxFrameCount).ToString();
            return;
        }

        animManager.ImportFrames(importAnim, gameManager.currentAnimation, startFrame, endFrame, insertFrame);
        animatorC.UpdateFrameUI();
        CloseWindow();
    }
}
