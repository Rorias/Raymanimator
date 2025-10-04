using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                Debug.LogError("Called too early");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        Application.targetFrameRate = 120;

        GameSettings.file = Application.persistentDataPath + "/" + GameSettings.fileName + ".json";
        //immediately loads the settings file into memory
        gameSettings = GameSettings.Instance;
        input = InputManager.Instance;

        LoadSettings();

        if (Input.GetJoystickNames().Length > 0 && !string.IsNullOrWhiteSpace(Input.GetJoystickNames()[0]))
        {
            input.controllerConnected = true;
        }

        SceneManager.sceneLoaded += FauxAwake;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private static readonly CultureInfo CultUS = new CultureInfo("en-US");

    private GameSettings gameSettings;
    private InputManager input;

    [NonSerialized] public Dictionary<int, Sprite> spritesetImages = new Dictionary<int, Sprite>();
    [NonSerialized] public Animation currentAnimation = null;

    private void FauxAwake(Scene _s, LoadSceneMode _lsm)
    {
        Camera.main.backgroundColor = gameSettings.bgColor;

        if (gameSettings.firstLoad)
        {
            //Do initialization
            foreach (KeyValuePair<InputManager.InputKey, KeyCode> Default in input.DefaultKeys)
            {
                InputManager.Key key = input.Inputs[Default.Key].First(x => x.type == InputManager.KeyType.Keyboard);
                key.code = input.DefaultKeys[Default.Key];
            }

            foreach (KeyValuePair<InputManager.InputKey, KeyCode> Default in input.DefaultButtons)
            {
                InputManager.Key key = input.Inputs[Default.Key].FirstOrDefault(x => x.type == InputManager.KeyType.Controller);
                if (key != null)
                {
                    key.code = input.DefaultButtons[Default.Key];
                }
            }

            gameSettings.firstLoad = false;
            SaveInputs();
        }
    }

    private void Update()
    {
        input.UpdateAxis();
    }

    public void LoadSettings()
    {
        LoadInputs();
    }

    private void LoadInputs()
    {
        input.LoadInputs(gameSettings);
    }

    public void SaveInputs()
    {
        input.SaveInputs(gameSettings);
        gameSettings.SaveSettings();
    }

    public float ParseToSingle(string parseValue)
    {
        if (float.TryParse(parseValue, NumberStyles.Float, CultUS, out float conv))
        {
            return conv;
        }
        else
        {
            DebugHelper.Log("Could not parse empty value. Using 1.0 instead.", DebugHelper.Severity.error);
            return 1.0f;
        }
    }

    public string ParseToString(float parseValue)
    {
        return parseValue.ToString(CultUS);
    }
}
