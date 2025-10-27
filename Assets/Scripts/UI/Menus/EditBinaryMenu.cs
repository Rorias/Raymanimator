using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditBinaryMenu : MonoBehaviour
{
    public ButtonPlus loadBinaryBtn;
    public TMP_Dropdown gameVersionDD;
    public TMP_InputField dataPathIF;
    public TMP_Dropdown objectDD;
    public TMP_Dropdown animationDD;

    private GameManager gameManager;
    private GameSettings settings;
    private InputManager input;
    private Rayman1BinaryAnimation rayBinary;
    private string selectedSpriteset;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
        input = InputManager.Instance;
        rayBinary = Rayman1BinaryAnimation.Instance;
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
        dataPathIF.text = settings.binaryBasePath;

        objectDD.onValueChanged.AddListener(delegate { SetAnimationsForObject(); });

        objectDD.interactable = false;
        animationDD.interactable = false;

        InitializeDropdowns();

        //Speed up debugging
        gameVersionDD.value = 0;
        objectDD.value = 0;
    }

    private void InitializeDropdowns()
    {
        if (gameVersionDD.options.Count > 0) { gameVersionDD.ClearOptions(); }

        List<string> supportedVersions = new List<string>() { Rayman1MSDOS.msdos };
        gameVersionDD.AddOptions(supportedVersions);
    }

    public void SetDataPathViaBrowse()
    {
        SetDataPath();
    }

    public bool SetDataPath()
    {
        string binaryPath = dataPathIF.text;

        if (!string.IsNullOrWhiteSpace(binaryPath) && Directory.Exists(binaryPath))
        {
            settings.binaryBasePath = binaryPath;
            Rayman1BinaryAnimation.LoadBinaryAnimations(settings);
            settings.SaveSettings();
            Debug.Log("Succesfully loaded binary file data.");
            SetObjectsForVersion();
            return true;
        }

        Debug.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
        DebugHelper.Log("Path cannot be found. Check if you spelled it correctly or use the browse button instead.");
        return false;
    }

    private void SetObjectsForVersion()
    {
        if (gameVersionDD.value >= 0 && !string.IsNullOrWhiteSpace(dataPathIF.text))
        {
            List<string> objects = new List<string>();

            switch (gameVersionDD.captionText.text)
            {
                case Rayman1MSDOS.msdos:
                    foreach (Rayman1MSDOS.DesignObjects obj in Enum.GetValues(typeof(Rayman1MSDOS.DesignObjects)))
                    {
                        objects.Add(obj.ToString());
                    }
                    break;
                default:
                    break;
            }

            objectDD.AddOptions(objects);
            objectDD.interactable = true;
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
                anims = Rayman1MSDOS.SetAnimationsForObject(objectDD.value, out selectedSpriteset);
                break;
            default:
                break;
        }

        animationDD.ClearOptions();
        animationDD.AddOptions(anims);
        animationDD.interactable = true;
    }

    public void LoadBinaryAnimation()
    {
        Rayman1MSDOS.DesignObjects currObject = (Rayman1MSDOS.DesignObjects)Enum.Parse(typeof(Rayman1MSDOS.DesignObjects), objectDD.captionText.text);
        gameManager.spritesetImages = rayBinary.LoadSpritesetFromBinary((int)currObject);
        gameManager.currentAnimation = rayBinary.LoadRaymAnimationFromBinary(animationDD.captionText.text, (int)currObject, animationDD.value, selectedSpriteset);
        SceneManager.LoadScene(1);
    }
}
