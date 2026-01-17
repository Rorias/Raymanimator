using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EditBinaryMenu : Raymanimator
{
    public ButtonPlus loadBinaryBtn;
    public TMP_DropdownPlus gameVersionDD;
    public TMP_InputField dataPathIF;
    public TMP_DropdownPlus fileDD;
    public TMP_DropdownPlus objectDD;
    public TMP_DropdownPlus animationDD;
    public TMP_DropdownPlus paletteDD;

    private GameManager gameManager;
    private GameSettings settings;
    private InputManager input;
    private BinaryAnimation rayBinary;
    private UIUtility uiUtility;
    private MiniPlaybackController miniPlayback;

    private string selectedSpriteset;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
        input = InputManager.Instance;
        rayBinary = BinaryAnimation.Instance;
        uiUtility = FindObjectOfType<UIUtility>();
        miniPlayback = FindObjectOfType<MiniPlaybackController>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (!loadBinaryBtn.gameObject.activeInHierarchy || dataPathIF.isFocused)
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
        gameVersionDD.onValueChanged.AddListener(delegate { SetFilesForVersion(); });
        dataPathIF.onValueChanged.AddListener(delegate { SetDataPathViaBrowse(); });
        dataPathIF.onEndEdit.AddListener(delegate { SetDataPath(); });

        fileDD.onValueChanged.AddListener(delegate { SetObjectsForFile(); });
        objectDD.onValueChanged.AddListener(delegate { SetAnimationsForObject(); });
        animationDD.onValueChanged.AddListener(delegate { CreatePreviewAnimation(); });
        paletteDD.onValueChanged.AddListener(delegate { CreatePreviewAnimation(); });

        fileDD.interactable = false;
        objectDD.interactable = false;
        animationDD.interactable = false;
        paletteDD.interactable = false;

        InitializeGameVersionDropdown();

        gameVersionDD.value = 0;
        dataPathIF.text = settings.binaryBasePath;
    }

    private void InitializeGameVersionDropdown()
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

    public void SetDataPath()
    {
        string binaryPath = dataPathIF.text;

        if (string.IsNullOrWhiteSpace(binaryPath) || !Directory.Exists(binaryPath))
        {
            Debug.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
            DebugHelper.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.", DebugHelper.Severity.warning);
            objectDD.interactable = false;
            animationDD.interactable = false;
            return;
        }

        if (!BinaryAnimation.BinaryFilesExist(binaryPath, gameVersionDD.captionText.text))
        {
            fileDD.interactable = false;
            objectDD.interactable = false;
            animationDD.interactable = false;
            paletteDD.interactable = false;
            return;
        }

        settings.binaryBasePath = binaryPath;
        settings.SaveSettings();
        BinaryAnimation.LoadBinaryFiles(binaryPath);
        SetFilesForVersion();
        Debug.Log("Succesfully loaded binary file data.");
    }

    private void SetFilesForVersion()
    {
        if (gameVersionDD.value >= 0 && !string.IsNullOrWhiteSpace(settings.binaryBasePath))
        {
            if (fileDD.options.Count > 0)
            {
                fileDD.ClearOptions();
            }

            switch (gameVersionDD.captionText.text)
            {
                case Rayman1MSDOS.msdos:
                    fileDD.AddOptions(Rayman1MSDOS.FileOptions);
                    break;
                default:
                    break;
            }

            fileDD.interactable = true;
            if (fileDD.value != -1)
            {
                fileDD.value = -1;
            }
        }
    }

    private void SetObjectsForFile()
    {
        if (fileDD.value < 0)
        {
            return;
        }

        if (objectDD.options.Count > 0)
        {
            objectDD.ClearOptions();
        }

        switch (gameVersionDD.captionText.text)
        {
            case Rayman1MSDOS.msdos:
                objectDD.AddOptions(Rayman1MSDOS.GetObjectsForFileIndex(fileDD.value));
                break;
            default:
                break;
        }

        objectDD.interactable = true;
        if (objectDD.value != -1)
        {
            objectDD.value = -1;
        }
    }

    private void SetAnimationsForObject()
    {
        if (objectDD.value < 0)
        {
            return;
        }

        if (animationDD.options.Count > 0)
        {
            animationDD.ClearOptions();
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

        animationDD.AddOptions(anims);
        animationDD.interactable = true;
        paletteDD.interactable = true;
        if (animationDD.value != -1)
        {
            animationDD.value = -1;
        }
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
                gameManager.spritesetImages = rayBinary.LoadSpritesetFromBinary(currObject, paletteDD.value);
            }
        }

        gameManager.currentAnimation = rayBinary.LoadRaymAnimationFromBinary(animationDD.captionText.text, currObject, animationDD.value, selectedSpriteset, map);
        gameManager.currentAnimation.binaryPaletteIndex = paletteDD.value;
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
