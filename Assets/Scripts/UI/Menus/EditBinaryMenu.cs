using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditBinaryMenu : Raymanimator
{
    public ButtonPlus loadBinaryBtn;
    public TMP_DropdownPlus gameVersionDD;
    public TMP_InputField dataPathIF;
    public TMP_DropdownPlus objectDD;
    public TMP_DropdownPlus animationDD;

    private GameManager gameManager;
    private GameSettings settings;
    private InputManager input;
    private Rayman1BinaryAnimation rayBinary;
    private UIUtility uiUtility;
    private MiniPlaybackController miniPlayback;

    private string selectedSpriteset;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
        input = InputManager.Instance;
        rayBinary = Rayman1BinaryAnimation.Instance;
        uiUtility = FindObjectOfType<UIUtility>();
        miniPlayback = FindObjectOfType<MiniPlaybackController>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (!loadBinaryBtn.gameObject.activeInHierarchy)
        {
            return;
        }

        if (input.GetKeyDown(InputManager.InputKey.Confirm))
        {
            loadBinaryBtn.onClick.Invoke();
        }
    }

    public void Initialize()
    {
        gameVersionDD.onValueChanged.AddListener(delegate { SetObjectsForVersion(); });
        dataPathIF.onValueChanged.AddListener(delegate { SetDataPathViaBrowse(); });
        dataPathIF.onEndEdit.AddListener(delegate { SetDataPath(); });

        objectDD.onValueChanged.AddListener(delegate { SetAnimationsForObject(); });
        animationDD.onValueChanged.AddListener(delegate { CreatePreviewAnimation(); });

        objectDD.interactable = false;
        animationDD.interactable = false;

        InitializeDropdowns();

        //Speed up debugging
        gameVersionDD.value = 0;
        dataPathIF.text = settings.binaryBasePath;
    }

    private void InitializeDropdowns()
    {
        if (gameVersionDD.options.Count > 0) { gameVersionDD.ClearOptions(); }

        List<string> supportedVersions = new List<string>() { Rayman1MSDOS.msdos };
        gameVersionDD.AddOptions(supportedVersions);
    }

    public void SetDataPathViaBrowse()
    {
        if (!string.IsNullOrWhiteSpace(dataPathIF.text) && (dataPathIF.text[^1] == '/' || dataPathIF.text[^1] == '\\'))
        {
            SetDataPath();
        }
    }

    public bool SetDataPath()
    {
        string binaryPath = dataPathIF.text;

        if (!string.IsNullOrWhiteSpace(binaryPath) && Directory.Exists(binaryPath))
        {
            settings.binaryBasePath = binaryPath;
            if (Rayman1BinaryAnimation.BinaryFilesExist(binaryPath))
            {
                Rayman1BinaryAnimation.LoadBinaryFiles(binaryPath);
                settings.SaveSettings();
                SetObjectsForVersion();
                Debug.Log("Succesfully loaded binary file data.");
                return true;
            }
        }

        objectDD.interactable = false;
        animationDD.interactable = false;
        return false;
    }

    private void SetObjectsForVersion()
    {
        if (gameVersionDD.value >= 0 && !string.IsNullOrWhiteSpace(settings.binaryBasePath))
        {
            Type designObjects;

            switch (gameVersionDD.captionText.text)
            {
                case Rayman1MSDOS.msdos:
                    designObjects = typeof(Rayman1MSDOS.DesignObjects);
                    break;
                default:
                    designObjects = null;
                    break;
            }

            List<string> objs = new List<string>();

            foreach (object obj in Enum.GetValues(designObjects))
            {
                objs.Add(obj.ToString());
            }

            objectDD.AddOptions(objs);
            objectDD.interactable = true;
            objectDD.value = 0;
        }
    }

    private void SetAnimationsForObject()
    {
        if (objectDD.value < 0)
        {
            return;
        }

        List<string> anims = new List<string>();

        switch (gameVersionDD.captionText.text)
        {
            case Rayman1MSDOS.msdos:
                selectedSpriteset = objectDD.captionText.text;
                Rayman1MSDOS.DesignObjects currObject = (Rayman1MSDOS.DesignObjects)Enum.Parse(typeof(Rayman1MSDOS.DesignObjects), objectDD.captionText.text);
                anims = Rayman1MSDOS.SetAnimationsForObject((int)currObject);
                break;
            default:
                break;
        }

        if (animationDD.options.Count > 0)
        {
            animationDD.ClearOptions();
        }
        animationDD.AddOptions(anims);
        animationDD.interactable = true;
    }

    private void LoadBinaryAnimation()
    {
        Rayman1MSDOS.DesignObjects currObject = (Rayman1MSDOS.DesignObjects)Enum.Parse(typeof(Rayman1MSDOS.DesignObjects), objectDD.captionText.text);
        Mapping map = null;
        if (gameManager.mappings.Count > 0)
        {
            map = gameManager.mappings.FirstOrDefault(x => x.Enabled && x.MapFromSet == settings.lastSpriteset && x.MapToSet == selectedSpriteset);
            if (map != null)
            {
                gameManager.spritesetImages = map.GenerateMappingSpriteset(settings.lastSpriteset, selectedSpriteset, settings.spritesetsPath, uiUtility);
            }
            else
            {
                gameManager.spritesetImages = rayBinary.LoadSpritesetFromBinary(currObject);
            }
        }

        gameManager.currentAnimation = rayBinary.LoadRaymAnimationFromBinary(animationDD.captionText.text, currObject, animationDD.value, selectedSpriteset, map);
    }

    private void CreatePreviewAnimation()
    {
        if (objectDD.interactable && animationDD.interactable)
        {
            LoadBinaryAnimation();
            miniPlayback.StartMiniPlayback();
        }
    }

    public void LoadEditorWithAnimation()
    {
        if (objectDD.interactable && animationDD.interactable)
        {
            LoadBinaryAnimation();
            SceneManager.LoadScene(1);
        }
    }
}
