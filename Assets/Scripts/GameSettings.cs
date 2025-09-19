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
    public const string fileName = "RaymanimatorSettings";

    public enum Themes { Light, Dark, Colorcoded };

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

    public bool fullScreen { get { return fs; } set { fs = value; } }
    [SerializeField] private bool fs = false;
    public int resNumber { get { return rnr; } set { rnr = value; } }
    [SerializeField] private int rnr = 1;
    public string spritesetsPath { get { return ssPath; } set { ssPath = value; } }
    [SerializeField] private string ssPath = string.Empty;
    public string lastSpriteset { get { return lastSS; } set { lastSS = value; } }
    [SerializeField] private string lastSS = string.Empty;
    public string animationsPath { get { return anPath; } set { anPath = value; } }
    [SerializeField] private string anPath = string.Empty;
    public float lastPlaybackSpeed { get { return lastPS; } set { lastPS = value; } }
    [SerializeField] private float lastPS = 0.0f;
    public Themes editorTheme { get { return tme; } set { tme = value; } }
    [SerializeField] private Themes tme = Themes.Dark;
    public Color bgColor { get { return bgc; } set { bgc = value; } }
    [SerializeField] private Color bgc = new Color(0.196f, 0.294f, 0.627f);

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
    ""animationsPath"":""" + Application.dataPath + @"/StreamingAssets/"",
    ""firstLoad"": true,
    ""activeJoystick"": 0
}");

            return File.ReadAllText(_path);
        }

        return string.Empty;
    }
}
