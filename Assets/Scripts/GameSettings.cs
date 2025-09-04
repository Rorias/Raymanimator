using System;
using System.IO;

using UnityEngine;

[Serializable]
public sealed class GameSettings
{
    #region Singleton
    private static GameSettings _instance = null;
    private static readonly object padlock = new object();

    private GameSettings() { }

    public static GameSettings Instance
    {
        get
        {
            lock (padlock)
            {
                if (null == _instance)
                {
                    LoadSettings();
                }
                return _instance;
            }
        }
    }
    #endregion

    public static string file;
    public const string fileName = "MegagroeiSettings";

    public bool firstLoad;

    public InputManager.PossibleJoystick activeJoystick;
    public KeyCode spritePrevious;
    public KeyCode spriteNext;
    public KeyCode framePrevious;
    public KeyCode frameNext;
    public KeyCode spritePosLeft;
    public KeyCode spritePosUp;
    public KeyCode spritePosRight;
    public KeyCode spritePosDown;
    public KeyCode zoomCamera;
    public KeyCode moveCamera;
    public KeyCode confirm;
    public KeyCode @return;

    public KeyCode jumpJoy;
    public KeyCode crouchJoy;
    public KeyCode rollJoy;
    public KeyCode runJoy;

    public void SaveSettings()
    {
        File.WriteAllText(file, JsonUtility.ToJson(Instance, true));

        string json = JsonUtility.ToJson(Instance);
        Debug.Log(json);
    }

    private static void LoadSettings()
    {
        if (File.Exists(file))
        {
            string json = File.ReadAllText(file);
            _instance = (GameSettings)JsonUtility.FromJson(json, typeof(GameSettings));
        }
        else
        {
            string json = CreateSettings(file);
            _instance = (GameSettings)JsonUtility.FromJson(json, typeof(GameSettings));
        }
    }

    private static string CreateSettings(string _path)
    {
        if (_path != null)
        {
            File.WriteAllText(_path,
@"{    
    ""firstLoad"": true,
    ""activeJoystick"": 0
}");

            return File.ReadAllText(_path);
        }

        return string.Empty;
    }
}
