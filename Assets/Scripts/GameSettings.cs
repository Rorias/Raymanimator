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
    public enum Languages { English, Nederlands };

    public bool firstLoad;

    public InputManager.PossibleJoystick activeJoystick;

    public KeyCode[] zoomCamera;
    public KeyCode[] dragCamera;
    public KeyCode[] moveCameraLeft;
    public KeyCode[] moveCameraUp;
    public KeyCode[] moveCameraRight;
    public KeyCode[] moveCameraDown;

    public KeyCode[] select;
    public KeyCode[] multiSelect;
    public KeyCode[] spritePrevious;
    public KeyCode[] spriteNext;
    public KeyCode[] deletePart;
    public KeyCode[] moveSpriteLeft;
    public KeyCode[] moveSpriteUp;
    public KeyCode[] moveSpriteRight;
    public KeyCode[] moveSpriteDown;

    public KeyCode[] framePrevious;
    public KeyCode[] frameNext;

    public KeyCode[] hideUI;

    public KeyCode confirm;
    public KeyCode @return;

    public bool fullScreen { get { return fs; } set { fs = value; } }
    [SerializeField] private bool fs = false;
    public bool tooltipsOn { get { return tt; } set { tt = value; } }
    [SerializeField] private bool tt = false;
    public bool previousGhostOn { get { return pg; } set { pg = value; } }
    [SerializeField] private bool pg = true;
    public bool nextGhostOn { get { return ng; } set { ng = value; } }
    [SerializeField] private bool ng = true;
    public Color32 previousGhostColor { get { return pgc; } set { pgc = value; } }
    [SerializeField] private Color32 pgc = new Color32(0, 255, 255, 64);
    public Color32 nextGhostColor { get { return ngc; } set { ngc = value; } }
    [SerializeField] private Color32 ngc = new Color32(255, 64, 64, 64);
    public bool normalFont { get { return nf; } set { nf = value; } }
    [SerializeField] private bool nf = false;
    public int resNumber { get { return rnr; } set { rnr = value; } }
    [SerializeField] private int rnr = -1;
    public string spritesetsPath { get { return ssPath; } set { ssPath = value; } }
    [SerializeField] private string ssPath = string.Empty;
    public string lastSpriteset { get { return lastSS; } set { lastSS = value; } }
    [SerializeField] private string lastSS = string.Empty;
    public string animationsPath { get { return anPath; } set { anPath = value; } }
    [SerializeField] private string anPath = string.Empty;
    public int lastPlaybackSpeed { get { return lastPS; } set { lastPS = value; } }
    [SerializeField] private int lastPS = 24;
    public Themes editorTheme { get { return tme; } set { tme = value; } }
    [SerializeField] private Themes tme = Themes.Dark;
    public Languages editorLanguage { get { return lne; } set { lne = value; } }
    [SerializeField] private Languages lne = Languages.English;
    public string backgroundPath { get { return bgp; } set { bgp = value; } }
    [SerializeField] private string bgp = string.Empty;
    public string lastBackground { get { return lbg; } set { lbg = value; } }
    [SerializeField] private string lbg = string.Empty;
    public Color32 bgColor { get { return bgc; } set { bgc = value; } }
    [SerializeField] private Color32 bgc = new Color32(50, 75, 150, 255);
    public Color32 gridOpacity { get { return new Color32(255, 255, 255, gOp); } set { gOp = value.a; } }
    [SerializeField] private byte gOp = 100;

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
    ""ssPath"":""" + Application.dataPath + @"/StreamingAssets/Spritesets/"",
    ""anPath"":""" + Application.dataPath + @"/StreamingAssets/Animations/"",
    ""firstLoad"": true,
    ""activeJoystick"": 0
}");

            return File.ReadAllText(_path);
        }

        return string.Empty;
    }
}
