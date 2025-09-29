using System;
using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateMenu : MonoBehaviour
{
    public TMP_InputField nameIF;
    public TMP_InputField framesIF;
    public TMP_InputField partsIF;
    public TMP_InputField xSizeIF;
    public TMP_InputField ySizeIF;

    private GameManager gameManager;
    private GameSettings settings;

    private void Awake()
    {
        nameIF.onEndEdit.AddListener(delegate { SetAnimationName(); });
        framesIF.onEndEdit.AddListener(delegate { SetMaxFrameCount(); });
        partsIF.onEndEdit.AddListener(delegate { SetMaxPartCount(); });
        xSizeIF.onEndEdit.AddListener(delegate { SetGridSizeX(); });
        ySizeIF.onEndEdit.AddListener(delegate { SetGridSizeY(); });
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
    }

    public void Initialize()
    {
        gameManager.currentAnimation = new Animation();

        if (string.IsNullOrWhiteSpace(framesIF.text))
        {
            framesIF.text = "24";
        }

        if (string.IsNullOrWhiteSpace(partsIF.text))
        {
            partsIF.text = "7";
        }

        if (string.IsNullOrWhiteSpace(xSizeIF.text))
        {
            xSizeIF.text = "64";
        }

        if (string.IsNullOrWhiteSpace(ySizeIF.text))
        {
            ySizeIF.text = "64";
        }
    }

    public void SetAnimationName()
    {
        if (CheckAnimationName())
        {
            gameManager.currentAnimation.animationName = nameIF.text;
        }
    }

    public bool CheckAnimationName()
    {
        if (string.IsNullOrWhiteSpace(nameIF.text))
        {
            Debug.Log("Please give your animation a name.");
            DebugHelper.Log("Please give your animation a name.");
            return false;
        }

        return true;
    }

    public void SetMaxFrameCount()
    {
        int.TryParse(framesIF.text, out int conv);
        gameManager.currentAnimation.maxFrameCount = Mathf.Min(Mathf.Max(conv, 1), 9999);
        framesIF.text = gameManager.currentAnimation.maxFrameCount.ToString();
    }

    public void SetMaxPartCount()
    {
        int.TryParse(partsIF.text, out int conv);
        gameManager.currentAnimation.maxPartCount = Mathf.Min(Mathf.Max(conv, 1), 99);
        partsIF.text = gameManager.currentAnimation.maxPartCount.ToString();
    }

    public void SetGridSizeX()
    {
        int.TryParse(xSizeIF.text, out int conv);
        gameManager.currentAnimation.gridSizeX = Mathf.Min(Mathf.Max(conv, 1), 4095);
        xSizeIF.text = gameManager.currentAnimation.gridSizeX.ToString();
    }

    public void SetGridSizeY()
    {
        int.TryParse(ySizeIF.text, out int conv);
        gameManager.currentAnimation.gridSizeY = Mathf.Min(Mathf.Max(conv, 1), 4095);
        ySizeIF.text = gameManager.currentAnimation.gridSizeY.ToString();
    }

    public void CreateNewAnimation()
    {
        if (File.Exists(settings.animationsPath + "\\" + gameManager.currentAnimation.animationName + ".xml"))
        {
            Debug.Log("There is already an animation with this name.");
            DebugHelper.Log("There is already an animation with this name.", DebugHelper.Severity.warning);
            return;
        }

        if (!CheckAnimationName()) { return; };

        SetAnimationName();
        SetMaxFrameCount();
        SetMaxPartCount();
        SetGridSizeX();
        SetGridSizeY();

        InitializeNewAnimation();

        SceneManager.LoadScene(1);
    }

    private void InitializeNewAnimation()
    {
        gameManager.currentAnimation.usedSpriteset = settings.lastSpriteset;

        for (int i = 0; i < gameManager.currentAnimation.maxFrameCount; i++)
        {
            gameManager.currentAnimation.frames.Add(new Frame() { frameID = i });
        }

        for (int i = 0; i < gameManager.currentAnimation.maxFrameCount; i++)
        {
            for (int part = 0; part < gameManager.currentAnimation.maxPartCount; part++)
            {
                gameManager.currentAnimation.frames[i].frameParts.Add(new Part() { partID = part, partIndex = -1 });
            }
        }
    }
}
