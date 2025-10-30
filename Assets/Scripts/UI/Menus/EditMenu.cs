using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditMenu : Raymanimator
{
    public GameObject editOptionsMenu;
    public GameObject renameMenu;
    public ConfirmWindow deleteConfirmWindow;

    public TMP_DropdownPlus loadAnimsDD;
    public ButtonPlus loadBtn;
    public ButtonPlus renameBtn;
    public ButtonPlus copyBtn;
    public ButtonPlus deleteBtn;
    public TMP_InputField renameIF;
    public ButtonPlus backBtn;

    private GameManager gameManager;
    private GameSettings settings;
    private AnimationManager animManager;
    private InputManager input;
    private UIUtility uiUtility;

    private List<string> loadableAnims = new List<string>();

    private string previousAnimName;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
        animManager = AnimationManager.Instance;
        input = InputManager.Instance;
        uiUtility = FindObjectOfType<UIUtility>();

        loadAnimsDD.onValueChanged.AddListener(delegate { GetAnimationName(); });

        renameBtn.onClick.AddListener(delegate { OpenRenameMenu(); });
        copyBtn.onClick.AddListener(delegate { CopyAnimation(); });
        deleteBtn.onClick.AddListener(delegate { deleteConfirmWindow.OpenWindow("Are you sure?", DeleteAnimation); });

        renameIF.onEndEdit.AddListener(delegate { RenameAnimation(); });
    }

    private void Update()
    {
        if (!loadBtn.gameObject.activeInHierarchy)
        {
            return;
        }

        if (input.GetKeyDown(InputManager.InputKey.Confirm))
        {
            loadBtn.onClick.Invoke();
        }
    }

    public void Initialize()
    {
        renameMenu.SetActive(false);

        GetLoadableAnimations();

        //ERROR: No animations to load, but user selected load anyway.
        if (loadableAnims.Count <= 0)
        {
            editOptionsMenu.SetActive(false);
            Debug.Log("No loadable animations were found. Please create a new one instead.");
            DebugHelper.Log("No loadable animations were found. Please create a new one instead.");
            return;
        }

        gameManager.currentAnimation = new Animation();

        InitializeDropdown();
    }

    #region Load settings
    private void GetLoadableAnimations()
    {
        if (string.IsNullOrWhiteSpace(settings.animationsPath))
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
    }

    private void InitializeDropdown()
    {
        if (loadAnimsDD.options.Count > 0) { loadAnimsDD.ClearOptions(); }

        loadAnimsDD.AddOptions(loadableAnims);

        for (int i = 0; i < loadableAnims.Count; i++)
        {
            loadAnimsDD.options[i].text = loadableAnims[i];
        }

        GetAnimationName();
    }

    public void GetAnimationName()
    {
        gameManager.currentAnimation.animationName = loadAnimsDD.captionText.text;
    }

    public void LoadAnimation()
    {
        if (loadableAnims.Count <= 0)
        {
            Debug.Log("No loadable animations were found. Please create a new one instead.");
            DebugHelper.Log("No loadable animations were found. Please create a new one instead.");
            return;
        }

        if (renameMenu.activeSelf)
        {
            DebugHelper.Log("Please finish renaming your animation before loading it.");
            return;
        }

        if (!animManager.LoadAnimation(gameManager.currentAnimation))
        {
            return;
        }

        if (gameManager.mappings.Count > 0)
        {
            Mapping map = gameManager.mappings.FirstOrDefault(x => x.Enabled && x.MapFromSet == settings.lastSpriteset && x.MapToSet == gameManager.currentAnimation.usedSpriteset);
            if (map != null)
            {
                gameManager.spritesetImages = map.GenerateMappingSpriteset(settings.lastSpriteset, gameManager.currentAnimation.usedSpriteset, settings.spritesetsPath, uiUtility);
            }
        }

        SceneManager.LoadScene(1);
    }
    #endregion

    #region Edit settings
    public void OpenRenameMenu()
    {
        renameMenu.SetActive(true);
        loadAnimsDD.interactable = false;
        editOptionsMenu.SetActive(false);
        backBtn.interactable = false;

        if (renameMenu.activeSelf)
        {
            renameIF.text = gameManager.currentAnimation.animationName;
            previousAnimName = renameIF.text;
        }
    }

    public void RenameAnimation()
    {
        string newName = renameIF.text;

        if (string.IsNullOrWhiteSpace(newName))
        {
            Debug.Log("Please give the animation a name.");
            DebugHelper.Log("Please give the animation a name.");
            return;
        }

        if (newName != previousAnimName && File.Exists(settings.animationsPath + "\\" + newName + ".xml"))
        {
            Debug.Log("There is already an animation with this name.");
            DebugHelper.Log("There is already an animation with this name.");
            return;
        }

        if (!animManager.LoadAnimation(gameManager.currentAnimation))
        {
            Debug.Log("Failed to load the current animation. Try selecting a different one.");
            DebugHelper.Log("Failed to load the current animation. Try selecting a different one.");
            return;
        }

        gameManager.currentAnimation.animationName = newName;
        animManager.SaveFile(gameManager.currentAnimation);

        if (newName != previousAnimName)
        {
            //remove old file to pretend like we renamed it
            DeleteAnimation(previousAnimName);
        }

        GetLoadableAnimations();
        InitializeDropdown();
        renameMenu.SetActive(false);
        loadAnimsDD.interactable = true;
        editOptionsMenu.SetActive(true);
        backBtn.interactable = true;
    }

    public void CopyAnimation()
    {
        string animName = gameManager.currentAnimation.animationName;

        if (!animManager.LoadAnimation(gameManager.currentAnimation))
        {
            return;
        }

        gameManager.currentAnimation.animationName = animName + "Copy";
        animManager.SaveFile(gameManager.currentAnimation);

        GetLoadableAnimations();
        InitializeDropdown();
    }

    public void DeleteAnimation()
    {
        string animName = gameManager.currentAnimation.animationName;
        DeleteAnimation(animName);
    }

    private void DeleteAnimation(string _name)
    {
        File.Delete(settings.animationsPath + "\\" + _name + ".xml");
        File.Delete(settings.animationsPath + "\\" + _name + ".cs");

        if (File.Exists(settings.animationsPath + "\\" + _name + ".xml.meta") &&
            File.Exists(settings.animationsPath + "\\" + _name + ".cs.meta"))
        {
            File.Delete(settings.animationsPath + "\\" + _name + ".xml.meta");
            File.Delete(settings.animationsPath + "\\" + _name + ".cs.meta");
        }

        GetLoadableAnimations();
        InitializeDropdown();
    }
    #endregion
}
