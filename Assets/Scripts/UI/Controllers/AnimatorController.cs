using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class AnimatorController : MonoBehaviour
{
    private GameManager gameManager;
    private GameSettings settings;
    private UIUtility uiUtility;
    private InputManager input;

    //current frame and current parts
    private Frame currentFrame;
    private List<Part> currentParts = new List<Part>();

    public GameObject UI;
    public GameObject gamePartPrefab;
    public GameObject previousGhostPrefab;
    public GameObject nextGhostPrefab;
    [Space]
    public Toggle xFlipToggle;
    public Toggle yFlipToggle;
    public TMP_InputField xPosIF;
    public TMP_InputField yPosIF;
    public TMP_InputField priorityIF;
    public TMP_InputField playbackSpeedIF;
    public SpritesDropdown spritesDDController;
    public TMP_Dropdown ddSprites;
    public ButtonPlus playButton;
    [Space]
    public ConfirmWindow exitConfirmWindow;
    public ButtonPlus exitButton;
    [Space]
    public Slider frameSelectSlider;
    public Slider partSelectSlider;

    private List<GamePart> gameParts = new List<GamePart>();
    private List<SpriteRenderer> previousGhostParts = new List<SpriteRenderer>();
    private List<SpriteRenderer> nextGhostParts = new List<SpriteRenderer>();

    private List<GamePart> currentGameParts = new List<GamePart>();

    private TMP_InputField[] allInputfields;
    private ButtonPlus[] allButtons;
    private Toggle[] allToggles;

    private TMP_Text partSelectText;
    private TMP_Text frameSelectText;

    private Animation thisAnim;

    private Vector3 nextFramePosition;

    private const float standardDelayTime = 0.3f;
    private const float hotkeyDelayTime = 0.05f;

    private float leftArrowTimer;
    private float rightArrowTimer;
    private float upArrowTimer;
    private float downArrowTimer;
    private float partSwapTimer;
    private float frameSwapTimer;

    private float playbackSpeed = 0.02f;

    private bool playingAnimation = false;
    private bool copyToNextFrame = true;
    private bool copyToNextFrameWasOn = false;
    [NonSerialized] public bool ghostingPrevious = true;
    [NonSerialized] public bool ghostingNext = true;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
        uiUtility = FindObjectOfType<UIUtility>();
        input = InputManager.Instance;

        allInputfields = FindObjectsOfType<TMP_InputField>();
        allButtons = FindObjectsOfType<ButtonPlus>();
        allToggles = FindObjectsOfType<Toggle>();

        partSelectText = GameObject.Find("CurrentPart").GetComponent<TMP_Text>();
        partSelectSlider.onValueChanged.AddListener(delegate { ChangeSelectedPart(); });

        frameSelectText = GameObject.Find("CurrentFrame").GetComponent<TMP_Text>();
        frameSelectSlider.onValueChanged.AddListener((_value) => { ChangeSelectedFrame(_value); });

        exitButton.onClick.AddListener(delegate
        { //TODO: Add check for changes in animation to show popup, otherwise no need
            exitConfirmWindow.OpenWindow("Save animation?", SaveAndQuit);
            exitConfirmWindow.noButton.onClick.AddListener(delegate { Quit(); });
        });
        playButton.onClick.AddListener(delegate { PlayAnimation(); });

        xFlipToggle.onValueChanged.AddListener((_state) => { FlipX(_state); });
        yFlipToggle.onValueChanged.AddListener((_state) => { FlipY(_state); });

        xPosIF.onEndEdit.AddListener(delegate { SetXPos(); });
        yPosIF.onEndEdit.AddListener(delegate { SetYPos(); });
        priorityIF.onEndEdit.AddListener(delegate { SetPriority(); });

        thisAnim = gameManager.currentAnimation;
    }

    private void Start()
    {
        if (gameManager.mappings.Count > 0)
        {
            Mapping map = gameManager.mappings.FirstOrDefault(x => x.MapFromSet == settings.lastSpriteset && x.MapToSet == thisAnim.usedSpriteset);
            if (map != null)
            {
                gameManager.spritesetImages = map.GenerateMappingSpriteset(settings.lastSpriteset, thisAnim.usedSpriteset, settings.spritesetsPath, uiUtility);
            }
            else
            {
                if (settings.lastSpriteset != thisAnim.usedSpriteset)
                {
                    Debug.Log(thisAnim.usedSpriteset + " was used when making this animation. Using " + settings.lastSpriteset + " can cause the animation to look different than intended.");
                    DebugHelper.Log(thisAnim.usedSpriteset + " was used when making this animation. Using " + settings.lastSpriteset + " can cause the animation to look different than intended.\n" +
                        "No mapping was found for the current spriteset match.", DebugHelper.Severity.warning);
                }
                else
                {
                    DebugHelper.Log("No mapping was found for the current spriteset.");
                }
            }
        }

        spritesDDController.InitializeSpritesDropdown();
        ddSprites.onValueChanged.AddListener(delegate { spritesDDController.SetSpriteForSelectedParts(currentParts, currentGameParts); });

        UpdatePartSelectText();
        UpdateFrameSelectText();

        for (int frame = 0; frame < thisAnim.frames.Count; frame++)
        {
            for (int part = 0; part < thisAnim.frames[frame].frameParts.Count; part++)
            {
                Part thisPart = thisAnim.frames[frame].frameParts[part];

                //if partIndex is set and partIndex exists in current spriteset
                if (thisPart.partIndex != -1 && ddSprites.options.Count > thisPart.partIndex)
                {
                    thisPart.part = ddSprites.options[thisPart.partIndex].image;
                }
            }
        }

        for (int part = 0; part < thisAnim.maxPartCount; part++)
        {
            CreatePart(part);
            CreatePreviousGhostPart(part);
            CreateNextGhostPart(part);
        }

        SetValues();

        currentFrame = thisAnim.frames[0];
        currentParts.Add(currentFrame.frameParts[0]);
        currentGameParts.Add(gameParts[0]);

        playbackSpeedIF.text = gameManager.ParseToString(settings.lastPlaybackSpeed);
        SetPlaybackSpeed();
        UpdatePos();
        UpdatePriorityText();
        UpdateFlip();
    }

    private void Update()
    {
        if (input.GetKeyDown(InputManager.InputKey.DeletePart))
        {
            DeletePart();
        }

        if (input.GetKeyDown(InputManager.InputKey.HideUI))
        {
            UI.SetActive(!UI.activeSelf);
        }

        foreach (TMP_InputField IF in allInputfields)
        {
            if (IF.isFocused)
            {
                return;
            }
        }

        if (spritesDDController.dropdownActive || playingAnimation)
        {
            return;
        }

        if (input.GetKeyDown(InputManager.InputKey.FrameNext))
        {
            if (Convert.ToInt32(frameSelectSlider.value) < thisAnim.maxFrameCount)
            {
                frameSelectSlider.value++;
                frameSwapTimer = Time.time + standardDelayTime;
            }
        }
        else if (input.GetKeyDown(InputManager.InputKey.FramePrevious))
        {
            if (Convert.ToInt32(frameSelectSlider.value) > 0)
            {
                frameSelectSlider.value--;
                frameSwapTimer = Time.time + standardDelayTime;
            }
        }

        if (input.GetKey(InputManager.InputKey.FrameNext) && Time.time > frameSwapTimer)
        {
            if (Convert.ToInt32(frameSelectSlider.value) < thisAnim.maxFrameCount)
            {
                frameSelectSlider.value++;
                frameSwapTimer = Time.time + hotkeyDelayTime;
            }
        }
        else if (input.GetKey(InputManager.InputKey.FramePrevious) && Time.time > frameSwapTimer)
        {
            if (Convert.ToInt32(frameSelectSlider.value) > 0)
            {
                frameSelectSlider.value--;
                frameSwapTimer = Time.time + hotkeyDelayTime;
            }
        }

        if (input.GetKeyDown(InputManager.InputKey.MoveSpriteLeft))
        {
            MovePartLeft();
            leftArrowTimer = Time.time + standardDelayTime;
        }
        if (input.GetKey(InputManager.InputKey.MoveSpriteLeft) && Time.time > leftArrowTimer)
        {
            MovePartLeft();
            leftArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (input.GetKeyDown(InputManager.InputKey.MoveSpriteRight))
        {
            MovePartRight();
            rightArrowTimer = Time.time + standardDelayTime;
        }
        if (input.GetKey(InputManager.InputKey.MoveSpriteRight) && Time.time > rightArrowTimer)
        {
            MovePartRight();
            rightArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (input.GetKeyDown(InputManager.InputKey.MoveSpriteUp))
        {
            MovePartUp();
            upArrowTimer = Time.time + standardDelayTime;
        }
        if (input.GetKey(InputManager.InputKey.MoveSpriteUp) && Time.time > upArrowTimer)
        {
            MovePartUp();
            upArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (input.GetKeyDown(InputManager.InputKey.MoveSpriteDown))
        {
            MovePartDown();
            downArrowTimer = Time.time + standardDelayTime;
        }
        if (input.GetKey(InputManager.InputKey.MoveSpriteDown) && Time.time > downArrowTimer)
        {
            MovePartDown();
            downArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (input.GetKeyDown(InputManager.InputKey.SpriteNext))
        {
            if (Convert.ToInt32(partSelectSlider.value) < thisAnim.maxPartCount)
            {
                partSelectSlider.value++;
                ChangeSelectedPart();
                UpdatePos();
                partSwapTimer = Time.time + standardDelayTime;
            }
        }
        else if (input.GetKeyDown(InputManager.InputKey.SpritePrevious))
        {
            if (Convert.ToInt32(partSelectSlider.value) > 0)
            {
                partSelectSlider.value--;
                ChangeSelectedPart();
                UpdatePos();
                partSwapTimer = Time.time + standardDelayTime;
            }
        }

        if (input.GetKey(InputManager.InputKey.SpriteNext) && Time.time > partSwapTimer)
        {
            if (Convert.ToInt32(partSelectSlider.value) < thisAnim.maxPartCount)
            {
                partSelectSlider.value++;
                ChangeSelectedPart();
                partSwapTimer = Time.time + hotkeyDelayTime;
            }
        }
        else if (input.GetKey(InputManager.InputKey.SpritePrevious) && Time.time > partSwapTimer)
        {
            if (Convert.ToInt32(partSelectSlider.value) > 0)
            {
                partSelectSlider.value--;
                ChangeSelectedPart();
                partSwapTimer = Time.time + hotkeyDelayTime;
            }
        }
    }

    #region Hotkeys
    public void DeletePart()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].sr.sprite = null;
        }

        for (int i = 0; i < currentParts.Count; i++)
        {
            for (int j = 0; j < currentFrame.frameParts.Count; j++)
            {
                if (currentParts[i].partID == currentFrame.frameParts[j].partID)
                {
                    currentFrame.frameParts[j].part = null;
                    break;
                }
            }

            currentParts[i].part = null;
        }
    }
    #endregion

    #region Initialization
    private void UpdatePartSelectText()
    {
        partSelectText.text = "Part " + (Convert.ToInt32(partSelectSlider.value) + 1) + "/" + thisAnim.maxPartCount;
    }

    private void UpdateFrameSelectText()
    {
        frameSelectText.text = "Frame " + (Convert.ToInt32(frameSelectSlider.value) + 1) + "/" + thisAnim.maxFrameCount;
    }

    private void CreatePart(int _part)
    {
        GameObject gameObjPart = Instantiate(gamePartPrefab);
        gameObjPart.name = "AnimPart" + _part;
        GamePart gamePart = gameObjPart.GetComponent<GamePart>();
        gamePart.Initialize(this);

        gameParts.Add(gamePart);

        gamePart.sr.sortingOrder = _part + 1;
        //Set visuals to position of first frame of the animation
        gamePart.sr.transform.position = new Vector3(thisAnim.frames[0].frameParts[_part].xPos, thisAnim.frames[0].frameParts[_part].yPos, 0);
        gamePart.sr.flipX = thisAnim.frames[0].frameParts[_part].flipX;
        gamePart.sr.flipY = thisAnim.frames[0].frameParts[_part].flipY;
        if (thisAnim.frames[0].frameParts[_part].part != null)
        {
            gamePart.sr.sprite = CreateSpriteWithPivot(thisAnim.frames[0].frameParts[_part].part, new Vector2(Convert.ToInt32(gamePart.sr.flipX), Convert.ToInt32(!gamePart.sr.flipY)));
        }

        gamePart.RecalculateCollision();
    }

    private void CreatePreviousGhostPart(int _part)
    {
        GameObject prevGhostPart = Instantiate(previousGhostPrefab);
        prevGhostPart.name = "PreviousFrameGhostPart" + _part;
        SpriteRenderer prevGhostPartSR = prevGhostPart.GetComponent<SpriteRenderer>();
        prevGhostPartSR.color = settings.previousGhostColor;

        previousGhostParts.Add(prevGhostPartSR);
    }

    private void CreateNextGhostPart(int _part)
    {
        GameObject nextGhostPart = Instantiate(nextGhostPrefab);
        nextGhostPart.name = "NextFrameGhostPart" + _part;
        SpriteRenderer nextGhostPartSR = nextGhostPart.GetComponent<SpriteRenderer>();
        nextGhostPartSR.color = settings.nextGhostColor;

        nextGhostParts.Add(nextGhostPartSR);
    }

    private void SetValues()
    {
        partSelectSlider.maxValue = thisAnim.maxPartCount - 1;
        frameSelectSlider.maxValue = thisAnim.maxFrameCount - 1;
    }
    #endregion

    #region Animation Functions
    public void PlayAnimation()
    {
        if (!playingAnimation)
        {
            playingAnimation = true;

            for (int i = 0; i < currentGameParts.Count; i++)
            {
                currentGameParts[i].anim.SetBool("WasSelected", false);
            }

            if (copyToNextFrame) { copyToNextFrameWasOn = true; copyToNextFrame = false; }
            else { copyToNextFrameWasOn = false; }

            for (int i = 0; i < gameParts.Count; i++)
            {
                if (gameParts[i].polyColl != null)
                {
                    gameParts[i].polyColl.enabled = false;
                }

                gameParts[i].anim.enabled = false;
            }

            playButton.GetComponentInChildren<TMP_Text>().text = "Stop";
            SetInteractablesState(false);

            StartCoroutine(StartAnimation());
        }
        else
        {
            playingAnimation = false;

            if (copyToNextFrameWasOn) { copyToNextFrame = true; }

            for (int i = 0; i < gameParts.Count; i++)
            {
                if (gameParts[i].polyColl != null)
                {
                    gameParts[i].polyColl.enabled = true;
                }

                gameParts[i].anim.enabled = true;
            }

            playButton.GetComponentInChildren<TMP_Text>().text = "Play";
            SetInteractablesState(true);

            StopAllCoroutines();
        }
    }

    private void SetInteractablesState(bool _state)
    {
        foreach (TMP_InputField inputField in allInputfields)
        {
            inputField.interactable = _state;
        }

        foreach (ButtonPlus button in allButtons)
        {
            if (button.name != "Play")
            {
                button.interactable = _state;
            }
        }

        foreach (Toggle toggle in allToggles)
        {
            toggle.interactable = _state;
        }

        ddSprites.interactable = _state;
    }

    private IEnumerator StartAnimation()
    {
        while (playingAnimation)
        {
            float targetTime = Time.time + ((1f / (float)settings.lastPlaybackSpeed) * (float)thisAnim.maxFrameCount);
            //float animTime = Time.time;
            float offset = 0.0f;
            for (int animFrames = 0; animFrames < thisAnim.maxFrameCount; animFrames++)
            {
                float frameTime = Time.time;
                frameSelectSlider.value = animFrames;
                yield return new WaitForSecondsRealtime(((targetTime - Time.time) / (thisAnim.maxFrameCount - animFrames)) - offset);
                offset += (Time.time - frameTime) - (1f / (float)settings.lastPlaybackSpeed);
                //Debug.Log("Frame time:" + (Time.time - frameTime) + ", offset: " + ((Time.time - frameTime) - (1f / (float)settings.lastPlaybackSpeed)));
            }
            //Debug.Log("Total time:" + (Time.time - animTime) + ", offset: " + ((Time.time - animTime) - (((1f / (float)settings.lastPlaybackSpeed) * (float)thisAnim.maxFrameCount))));
        }
    }

    public void AnimToZipStart()
    {
        playingAnimation = true;
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].anim.SetBool("WasSelected", false);
        }
        playbackSpeed = 0.001f;
    }

    public void AnimToZipEnd()
    {
        playbackSpeed = settings.lastPlaybackSpeed;
        playingAnimation = false;
    }

    public void SetPlaybackSpeed()
    {
        playbackSpeed = gameManager.ParseToSingle(playbackSpeedIF.text);

        if (playbackSpeed < 1f)
        {
            Debug.Log("Playback speed cannot be lower than 1. Auto-set to 1.");
            playbackSpeed = 1f;
            playbackSpeedIF.text = gameManager.ParseToString(playbackSpeed);
        }

        if (playbackSpeed > 120f)
        {
            Debug.Log("Playback speed cannot be higher than 120. Auto-set to 120.");
            playbackSpeed = 120f;
            playbackSpeedIF.text = gameManager.ParseToString(playbackSpeed);
        }

        settings.lastPlaybackSpeed = (int)playbackSpeed;
        settings.SaveSettings();
    }
    #endregion

    #region Frame Functions
    public void AddFrame()//TODO: change to add from current pos
    {
        int newMax = thisAnim.maxFrameCount + 1;
        thisAnim.maxFrameCount = Mathf.Min(Mathf.Max(newMax, 1), 9999);

        thisAnim.frames.Add(new Frame() { frameID = thisAnim.maxFrameCount - 1 });

        for (int pID = 0; pID < thisAnim.maxPartCount; pID++)
        {
            thisAnim.frames[thisAnim.maxFrameCount - 1].frameParts.Add(new Part() { partID = pID });
        }

        UpdateFrameUI();
    }

    public void RemoveFrame()//TODO: change to remove from current pos
    {
        if (frameSelectSlider.value == thisAnim.maxFrameCount - 1)
        {
            frameSelectSlider.value--;
        }

        int newMax = thisAnim.maxFrameCount - 1;
        thisAnim.maxFrameCount = Mathf.Min(Mathf.Max(newMax, 1), 9999);

        thisAnim.frames.RemoveAt(thisAnim.maxFrameCount);

        UpdateFrameUI();
    }

    public void UpdateFrameUI()
    {
        UpdateFrameSelectText();
        frameSelectSlider.maxValue = thisAnim.maxFrameCount - 1;
    }

    public void NextFrame()
    {
        frameSelectSlider.value++;
    }

    public void PrevFrame()
    {
        frameSelectSlider.value--;
    }

    public void ChangeSelectedFrame(float _value)
    {
        int frameId = Convert.ToInt32(_value);

        for (int i = 0; i < thisAnim.maxPartCount; i++)
        {
            currentFrame.frameParts[i].xPos = gameParts[i].transform.position.x;
            currentFrame.frameParts[i].yPos = gameParts[i].transform.position.y;
            currentFrame.frameParts[i].flipX = gameParts[i].sr.flipX;
            currentFrame.frameParts[i].flipY = gameParts[i].sr.flipY;
            currentFrame.frameParts[i].part = gameParts[i].sr.sprite;

            if (ghostingPrevious && !playingAnimation && frameId > 0)//previous frame exists
            {
                previousGhostParts[i].transform.position = new Vector2(thisAnim.frames[frameId - 1].frameParts[i].xPos, thisAnim.frames[frameId - 1].frameParts[i].yPos);
                previousGhostParts[i].flipX = thisAnim.frames[frameId - 1].frameParts[i].flipX;
                previousGhostParts[i].flipY = thisAnim.frames[frameId - 1].frameParts[i].flipY;
                previousGhostParts[i].sprite = thisAnim.frames[frameId - 1].frameParts[i].part;
            }
            else
            {
                previousGhostParts[i].sprite = null;
            }

            if (ghostingNext && !playingAnimation && frameId < thisAnim.maxFrameCount - 1)//previous frame exists
            {
                nextGhostParts[i].transform.position = new Vector2(thisAnim.frames[frameId + 1].frameParts[i].xPos, thisAnim.frames[frameId + 1].frameParts[i].yPos);
                nextGhostParts[i].flipX = thisAnim.frames[frameId + 1].frameParts[i].flipX;
                nextGhostParts[i].flipY = thisAnim.frames[frameId + 1].frameParts[i].flipY;
                nextGhostParts[i].sprite = thisAnim.frames[frameId + 1].frameParts[i].part;
            }
            else
            {
                nextGhostParts[i].sprite = null;
            }

            if (copyToNextFrame && frameId > currentFrame.frameID &&
                thisAnim.frames[frameId].frameParts[i].part == null)
            {
                //Set actual current part data to the last frame part data if copyToNextFrame is true
                thisAnim.frames[frameId].frameParts[i].partIndex = currentFrame.frameParts[i].partIndex;
                thisAnim.frames[frameId].frameParts[i].xPos = currentFrame.frameParts[i].xPos;
                thisAnim.frames[frameId].frameParts[i].yPos = currentFrame.frameParts[i].yPos;
                thisAnim.frames[frameId].frameParts[i].flipX = currentFrame.frameParts[i].flipX;
                thisAnim.frames[frameId].frameParts[i].flipY = currentFrame.frameParts[i].flipY;
                thisAnim.frames[frameId].frameParts[i].part = currentFrame.frameParts[i].part;
            }

            nextFramePosition.x = thisAnim.frames[frameId].frameParts[i].xPos;
            nextFramePosition.y = thisAnim.frames[frameId].frameParts[i].yPos;
            gameParts[i].transform.position = nextFramePosition;

            gameParts[i].sr.flipX = thisAnim.frames[frameId].frameParts[i].flipX;
            gameParts[i].sr.flipY = thisAnim.frames[frameId].frameParts[i].flipY;
            if (thisAnim.frames[frameId].frameParts[i].part != null)
            {
                gameParts[i].sr.sprite = CreateSpriteWithPivot(thisAnim.frames[frameId].frameParts[i].part, new Vector2(Convert.ToInt32(gameParts[i].sr.flipX), Convert.ToInt32(!gameParts[i].sr.flipY)));
            }
            else
            {
                gameParts[i].sr.sprite = null;
            }

            if (!playingAnimation)
            {
                gameParts[i].RecalculateCollision();
            }
        }

        currentFrame = thisAnim.frames[frameId];//set current frame to selected frame from the frame selector
        UpdateFrameSelectText();
        UpdateSelectedParts();

        //if (!playingAnimation)
        //{
        //    SetSelectedPart();
        //    LoadPartData();
        //}
    }

    public void ClearFrame()
    {
        for (int i = 0; i < thisAnim.maxPartCount; i++)
        {
            currentFrame.frameParts[i].part = null;
            gameParts[i].sr.sprite = null;
        }
    }

    public void CopyToNext()
    {
        copyToNextFrame = !copyToNextFrame;
    }

    public void UpdatePrevGhostColor()
    {
        for (int i = 0; i < previousGhostParts.Count; i++)
        {
            previousGhostParts[i].color = settings.previousGhostColor;
        }
    }

    public void UpdateNextGhostColor()
    {
        for (int i = 0; i < nextGhostParts.Count; i++)
        {
            nextGhostParts[i].color = settings.nextGhostColor;
        }
    }

    //private void LoadPartData()
    //{
    //    currentPart.part = currentGamePartSR.sprite;
    //    currentPart.flipX = currentGamePartSR.flipX;
    //    currentPart.flipY = currentGamePartSR.flipY;

    //    UpdatePos();
    //}

    #endregion

    #region Part Functions
    public void AddPart()
    {
        int newMax = thisAnim.maxPartCount + 1;
        newMax = Mathf.Min(Mathf.Max(newMax, 1), 99);
        thisAnim.maxPartCount = newMax;

        UpdatePartSelectText();

        for (int allFrames = 0; allFrames < thisAnim.maxFrameCount; allFrames++)
        {
            thisAnim.frames[allFrames].frameParts.Add(new Part() { partID = thisAnim.maxPartCount - 1 });
        }

        CreatePart(thisAnim.maxPartCount - 1);
        CreatePreviousGhostPart(thisAnim.maxPartCount - 1);
        CreateNextGhostPart(thisAnim.maxPartCount - 1);

        partSelectSlider.maxValue = thisAnim.maxPartCount - 1;
    }

    public void RemovePart()
    {
        if (partSelectSlider.value == thisAnim.maxPartCount - 1)
        {
            partSelectSlider.value--;
        }

        int newMax = thisAnim.maxPartCount - 1;
        newMax = Mathf.Min(Mathf.Max(newMax, 1), 99);
        thisAnim.maxPartCount = newMax;

        UpdatePartSelectText();

        for (int allFrames = 0; allFrames < thisAnim.maxFrameCount; allFrames++)
        {
            thisAnim.frames[allFrames].frameParts.RemoveAt(thisAnim.maxPartCount);
        }

        gameParts.RemoveAt(thisAnim.maxPartCount);

        previousGhostParts.RemoveAt(thisAnim.maxPartCount);
        nextGhostParts.RemoveAt(thisAnim.maxPartCount);

        partSelectSlider.maxValue = thisAnim.maxPartCount - 1;

        Destroy(GameObject.Find("AnimPart" + thisAnim.maxPartCount));
        Destroy(GameObject.Find("PreviousFrameGhostPart" + thisAnim.maxPartCount));
        Destroy(GameObject.Find("NextFrameGhostPart" + thisAnim.maxPartCount));
    }

    public void AddPartToMultiselect(int _partID)
    {
        //If ?? or part is already selected
        if (_partID >= thisAnim.maxPartCount || currentGameParts.Contains(gameParts[_partID]))
        {
            return;
        }

        currentGameParts.Add(gameParts[_partID]);

        if (!playingAnimation)
        {
            for (int i = 0; i < currentGameParts.Count; i++)
            {
                currentGameParts[i].anim.SetBool("WasSelected", true);
            }
        }

        currentParts.Add(currentFrame.frameParts[_partID]);
    }

    public void SelectAllParts()
    {
        currentParts.Clear();
        currentGameParts.Clear();

        for (int i = 0; i < thisAnim.maxPartCount; i++)
        {
            currentGameParts.Add(gameParts[i]);

            if (!playingAnimation)
            {
                currentGameParts[i].anim.SetBool("WasSelected", true);
            }

            currentParts.Add(currentFrame.frameParts[i]);

            if (currentGameParts[i].polyColl == null)
            {
                continue;
            }

            if (currentGameParts[i].sr.sprite == null)
            {
                currentGameParts[i].polyColl.enabled = false;
            }
            else
            {
                currentGameParts[i].polyColl.enabled = true;
            }
        }
    }

    public void ChangeSelectedPart()
    {
        int partID = Convert.ToInt32(partSelectSlider.value);

        if (partID >= thisAnim.maxPartCount)
        {
            return;
        }

        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].anim.SetBool("WasSelected", false);
        }

        currentParts.Clear();
        currentGameParts.Clear();

        currentGameParts.Add(gameParts[partID]);

        if (!playingAnimation)
        {
            currentGameParts[0].anim.SetBool("WasSelected", true);
        }

        currentParts.Add(currentFrame.frameParts[partID]);
        UpdatePartSelectText();
        UpdatePriorityText();
        UpdateFlip();

        if (currentGameParts[0].sr.sprite == null)
        {
            currentGameParts[0].polyColl.enabled = false;
        }
        else
        {
            currentGameParts[0].polyColl.enabled = true;
        }
    }

    public void UpdateSelectedParts()
    {
        //Update all currently selected part ids to be replaced with their counter part from the newly opened frame
        for (int i = 0; i < currentParts.Count; i++)
        {
            for (int j = 0; j < currentFrame.frameParts.Count; j++)
            {
                if(currentParts[i].partID == currentFrame.frameParts[j].partID)
                {
                    currentParts[i] = currentFrame.frameParts[j];
                    break;
                }
            }
        }
    }

    public void FlipX(bool _value)
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].sr.flipX = _value;
            currentGameParts[i].sr.sprite = currentGameParts[i].sr.sprite != null ? CreateSpriteWithPivot(currentGameParts[i].sr.sprite, new Vector2(Convert.ToInt32(_value), Convert.ToInt32(!currentGameParts[i].sr.flipY))) : null;
            currentParts[i].flipX = currentGameParts[i].sr.flipX;
        }
    }

    public void FlipY(bool _value)
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].sr.flipY = _value;
            currentGameParts[i].sr.sprite = currentGameParts[i].sr.sprite != null ? CreateSpriteWithPivot(currentGameParts[i].sr.sprite, new Vector2(Convert.ToInt32(currentGameParts[i].sr.flipX), Convert.ToInt32(!_value))) : null;
            currentParts[i].flipY = currentGameParts[i].sr.flipY;
        }
    }

    private Sprite CreateSpriteWithPivot(Sprite existingSprite, Vector2 pivot)
    {
        return Sprite.Create(existingSprite.texture, existingSprite.rect, pivot, existingSprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), true);
    }

    public void SetPriority()
    {
        int priority = Convert.ToInt32(priorityIF.text);

        if (priority < -127)
        {
            Debug.Log("Priority cannot be lower than -127. Auto-set to -127.");
            priority = -127;
        }

        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].sr.sortingOrder = priority;
            currentGameParts[i].transform.position = new Vector3(currentGameParts[i].transform.position.x, currentGameParts[i].transform.position.y, 0);
        }

        UpdatePriorityText();
    }

    public void UpdatePriorityText()
    {
        priorityIF.text = currentGameParts[0].sr.sortingOrder.ToString();
    }

    public void SetXPos()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].transform.position = new Vector3((Convert.ToSingle(xPosIF.text) + (thisAnim.gridSizeX / 2)) / 16f, currentGameParts[i].transform.position.y, currentGameParts[i].transform.position.z);
        }
        UpdatePos();
    }

    public void SetYPos()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].transform.position = new Vector3(currentGameParts[i].transform.position.x, (Convert.ToSingle(yPosIF.text) - (thisAnim.gridSizeY / 2)) / 16f, currentGameParts[i].transform.position.z);
        }
        UpdatePos();
    }

    private void MovePart(Vector3 _direction)
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            if (currentGameParts[i].sr.sprite == null)
            {
                DebugHelper.Log("Select a sprite for the part first.", DebugHelper.Severity.warning);
                continue;
            }

            currentGameParts[i].transform.position += _direction;
        }

        UpdatePos();
    }

    public void MovePartUp()
    {
        MovePart(new Vector3(0, 1.0f / 16.0f, 0));
    }

    public void MovePartDown()
    {
        MovePart(new Vector3(0, -(1.0f / 16.0f), 0));
    }

    public void MovePartRight()
    {
        MovePart(new Vector3(1.0f / 16.0f, 0, 0));
    }

    public void MovePartLeft()
    {
        MovePart(new Vector3(-(1.0f / 16.0f), 0, 0));
    }

    public void SetOffsetForSelectedParts()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].SetOffset();
        }
    }

    public void DragSelectedParts(Vector3 _drag)
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].transform.position = new Vector3(Mathf.Round((_drag.x - currentGameParts[i].xDifference) * 32.0f) / 32.0f, Mathf.Round((_drag.y - currentGameParts[i].yDifference) * 32.0f) / 32.0f, 0);
        }
    }

    public void FixPartX()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].transform.position += new Vector3(1.0f / 32.0f, 0, 0);
        }
        UpdatePos();
    }

    public void FixPartY()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].transform.position += new Vector3(0, 1.0f / 32.0f, 0);
        }
        UpdatePos();
    }

    public void UpdatePos()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentParts[i].xPos = currentGameParts[i].transform.position.x;
            currentParts[i].yPos = currentGameParts[i].transform.position.y;

            yPosIF.text = gameManager.ParseToString((currentGameParts[i].transform.position.y * 16f) + (thisAnim.gridSizeY / 2));
            xPosIF.text = gameManager.ParseToString((currentGameParts[i].transform.position.x * 16f) - (thisAnim.gridSizeX / 2));
        }
    }

    public void UpdateFlip()
    {
        xFlipToggle.isOn = currentParts[0].flipX;
        yFlipToggle.isOn = currentParts[0].flipY;
    }
    #endregion

    public void SaveAndQuit()
    {
        AnimationManager animManager = AnimationManager.Instance;
        animManager.SaveFile(gameManager.currentAnimation);
        exitConfirmWindow.noButton.onClick.RemoveAllListeners();
        Quit();
    }

    public void Quit()
    {
        settings.SaveSettings();
        SceneManager.LoadScene(0);
    }
}
