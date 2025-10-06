using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimatorController : MonoBehaviour
{
    private GameManager gameManager = GameManager.Instance;
    private GameSettings settings = GameSettings.Instance;
    private InputManager inputManager = InputManager.Instance;

    //current frame and current parts
    private Frame currentFrame;
    private List<Part> currentParts = new List<Part>();

    public GameObject gamePartPrefab;
    public GameObject previousGhostPrefab;
    public GameObject nextGhostPrefab;

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

    private List<GamePart> GameParts = new List<GamePart>();
    private List<SpriteRenderer> PreviousGhostParts = new List<SpriteRenderer>();
    private List<SpriteRenderer> NextGhostParts = new List<SpriteRenderer>();

    private List<GamePart> currentGameParts = new List<GamePart>();

    private TMP_InputField[] allInputfields;
    private ButtonPlus[] allButtons;
    private Toggle[] allToggles;

    private TMP_Text partSelectText;
    private TMP_Text frameSelectText;

    private WaitForSeconds playbackSpeedWFS = new WaitForSeconds(0.02f);
    private Animation thisAnim;

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
    private bool ghostingPrevious = true;
    private bool ghostingNext = false;
    private bool ghostingWasOn = false;

    private void Awake()
    {
        allInputfields = FindObjectsOfType<TMP_InputField>();
        allButtons = FindObjectsOfType<ButtonPlus>();
        allToggles = FindObjectsOfType<Toggle>();

        partSelectText = GameObject.Find("CurrentPart").GetComponent<TMP_Text>();
        partSelectSlider.onValueChanged.AddListener(delegate { ChangeSelectedPart(); });

        frameSelectText = GameObject.Find("CurrentFrame").GetComponent<TMP_Text>();
        frameSelectSlider.onValueChanged.AddListener(delegate { ChangeSelectedFrame(); });

        exitButton.onClick.AddListener(delegate { exitConfirmWindow.OpenWindow("Save animation?", SaveAndQuit); exitConfirmWindow.noButton.onClick.AddListener(delegate { Quit(); }); });
        playButton.onClick.AddListener(delegate { PlayAnimation(); });

        thisAnim = gameManager.currentAnimation;
    }

    private void Start()
    {
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
        currentGameParts.Add(GameParts[0]);

        if (settings.lastSpriteset != thisAnim.usedSpriteset)
        {
            Debug.Log("A different spriteset was used when making this animation. Using this one can cause the animation to look different than intended.");
        }

        playbackSpeedIF.text = gameManager.ParseToString(settings.lastPlaybackSpeed);
        SetPlaybackSpeed();
    }

    private void Update()
    {
        if (inputManager.GetKeyDown(InputManager.InputKey.DeletePart))
        {
            for (int i = 0; i < currentGameParts.Count; i++)
            {
                currentGameParts[i].sr.sprite = null;
            }

            for (int i = 0; i < currentParts.Count; i++)
            {
                currentParts[i].part = null;
            }
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

        if (inputManager.GetKeyDown(InputManager.InputKey.FrameNext))
        {
            if (Convert.ToInt32(frameSelectSlider.value) < thisAnim.maxFrameCount)
            {
                frameSelectSlider.value++;
                frameSwapTimer = Time.time + standardDelayTime;
            }
        }
        else if (inputManager.GetKeyDown(InputManager.InputKey.FramePrevious))
        {
            if (Convert.ToInt32(frameSelectSlider.value) > 0)
            {
                frameSelectSlider.value--;
                frameSwapTimer = Time.time + standardDelayTime;
            }
        }

        if (inputManager.GetKeyDown(InputManager.InputKey.FrameNext) && Time.time > frameSwapTimer)
        {
            if (Convert.ToInt32(frameSelectSlider.value) < thisAnim.maxFrameCount)
            {
                frameSelectSlider.value++;
                frameSwapTimer = Time.time + hotkeyDelayTime;
            }
        }
        if (inputManager.GetKeyDown(InputManager.InputKey.FramePrevious) && Time.time > frameSwapTimer)
        {
            if (Convert.ToInt32(frameSelectSlider.value) > 0)
            {
                frameSelectSlider.value--;
                frameSwapTimer = Time.time + hotkeyDelayTime;
            }
        }

        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteLeft))
        {
            MovePartLeft();
            leftArrowTimer = Time.time + standardDelayTime;
        }
        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteLeft) && Time.time > leftArrowTimer)
        {
            MovePartLeft();
            leftArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteRight))
        {
            MovePartRight();
            rightArrowTimer = Time.time + standardDelayTime;
        }
        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteRight) && Time.time > rightArrowTimer)
        {
            MovePartRight();
            rightArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteUp))
        {
            MovePartUp();
            upArrowTimer = Time.time + standardDelayTime;
        }
        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteUp) && Time.time > upArrowTimer)
        {
            MovePartUp();
            upArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteDown))
        {
            MovePartDown();
            downArrowTimer = Time.time + standardDelayTime;
        }
        if (inputManager.GetKeyDown(InputManager.InputKey.MoveSpriteDown) && Time.time > downArrowTimer)
        {
            MovePartDown();
            downArrowTimer = Time.time + hotkeyDelayTime;
        }

        if (inputManager.GetKeyDown(InputManager.InputKey.SpriteNext))
        {
            if (Convert.ToInt32(partSelectSlider.value) < thisAnim.maxPartCount)
            {
                partSelectSlider.value++;
                ChangeSelectedPart();
                UpdatePos();
                partSwapTimer = Time.time + standardDelayTime;
            }
        }
        else if (inputManager.GetKeyDown(InputManager.InputKey.SpritePrevious))
        {
            if (Convert.ToInt32(partSelectSlider.value) > 0)
            {
                partSelectSlider.value--;
                ChangeSelectedPart();
                UpdatePos();
                partSwapTimer = Time.time + standardDelayTime;
            }
        }

        if (inputManager.GetKeyDown(InputManager.InputKey.SpriteNext) && Time.time > partSwapTimer)
        {
            if (Convert.ToInt32(partSelectSlider.value) < thisAnim.maxPartCount)
            {
                partSelectSlider.value++;
                ChangeSelectedPart();
                partSwapTimer = Time.time + hotkeyDelayTime;
            }
        }
        if (inputManager.GetKeyDown(InputManager.InputKey.SpritePrevious) && Time.time > partSwapTimer)
        {
            if (Convert.ToInt32(partSelectSlider.value) > 0)
            {
                partSelectSlider.value--;
                ChangeSelectedPart();
                partSwapTimer = Time.time + hotkeyDelayTime;
            }
        }
    }

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

        GameParts.Add(gamePart);

        gamePart.sr.sortingOrder = _part + 1;
        gamePart.sr.sprite = thisAnim.frames[0].frameParts[_part].part;
        gamePart.sr.transform.position = new Vector3(thisAnim.frames[0].frameParts[_part].xPos, thisAnim.frames[0].frameParts[_part].yPos, 0);
        gamePart.sr.flipX = thisAnim.frames[0].frameParts[_part].flipX;
        gamePart.sr.flipY = thisAnim.frames[0].frameParts[_part].flipY;

        if (gamePart.sr.sprite != null)
        {
            gameObjPart.AddComponent<PolygonCollider2D>();
            gamePart.polyColl = gameObjPart.GetComponent<PolygonCollider2D>();
        }
    }

    private void CreatePreviousGhostPart(int _part)
    {
        GameObject ghostPart = Instantiate(previousGhostPrefab);
        ghostPart.name = "PreviousFrameGhostPart" + _part;
        SpriteRenderer ghostPartSR = ghostPart.GetComponent<SpriteRenderer>();
        ghostPartSR.color = new Color(0, 1, 1, 0.25f);//TODO: change color based on user settings

        PreviousGhostParts.Add(ghostPartSR);
    }

    private void CreateNextGhostPart(int _part)
    {
        GameObject ghostPart = Instantiate(nextGhostPrefab);
        ghostPart.name = "NextFrameGhostPart" + _part;
        SpriteRenderer ghostPartSR = ghostPart.GetComponent<SpriteRenderer>();
        ghostPartSR.color = new Color(1, 0.25f, 0.25f, 0.25f);//TODO: change color based on user settings

        NextGhostParts.Add(ghostPartSR);
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

            if (ghostingNext || ghostingPrevious) { ghostingWasOn = true; ghostingNext = false; ghostingPrevious = false; }//TODO: split return to on function based on previous/next
            else { ghostingWasOn = false; }

            for (int i = 0; i < GameParts.Count; i++)
            {
                if (GameParts[i].polyColl != null)
                {
                    GameParts[i].polyColl.enabled = false;
                }
            }

            playButton.GetComponentInChildren<TMP_Text>().text = "Stop";
            SetInteractablesState(false);

            StartCoroutine(StartAnimation());
        }
        else
        {
            playingAnimation = false;

            if (copyToNextFrameWasOn) { copyToNextFrame = true; }
            if (ghostingWasOn) { ghostingNext = true; ghostingPrevious = true; }//TODO: split return to on function based on previous/next

            for (int i = 0; i < GameParts.Count; i++)
            {
                if (GameParts[i].polyColl != null)
                {
                    GameParts[i].polyColl.enabled = true;
                }
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
            for (int animFrames = 0; animFrames < thisAnim.maxFrameCount; animFrames++)
            {
                frameSelectSlider.value = animFrames;
                yield return playbackSpeedWFS;
            }
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

    public void SetPlaybackSpeed()//TODO: change from milliseconds to frames per second counter, capped at 120
    {
        playbackSpeed = gameManager.ParseToSingle(playbackSpeedIF.text);

        if (playbackSpeed < 0.001f)
        {
            Debug.Log("Playback speed cannot be lower than 0.001. Auto-set to 0.1.");
            playbackSpeed = 0.1f;
            playbackSpeedIF.text = gameManager.ParseToString(playbackSpeed);
        }

        playbackSpeedWFS = new WaitForSeconds(playbackSpeed);
        settings.lastPlaybackSpeed = playbackSpeed;
        settings.SaveSettings();
    }
    #endregion

    #region Frame Functions
    public void AddFrame()//TODO: add AddFrameToEnd function to differentiate between current and late adding
    {
        int newMax = thisAnim.maxFrameCount + 1;
        newMax = Mathf.Min(Mathf.Max(newMax, 1), 999);
        thisAnim.maxFrameCount = newMax;

        UpdateFrameSelectText();

        thisAnim.frames.Add(new Frame() { frameID = thisAnim.maxFrameCount - 1 });

        for (int allParts = 0; allParts < thisAnim.maxPartCount; allParts++)
        {
            thisAnim.frames[thisAnim.maxFrameCount - 1].frameParts.Add(new Part() { partID = allParts });
        }

        frameSelectSlider.maxValue = thisAnim.maxFrameCount - 1;
    }

    public void RemoveFrame()
    {
        if (frameSelectSlider.value == thisAnim.maxFrameCount - 1)
        {
            frameSelectSlider.value--;
        }

        int newMax = thisAnim.maxFrameCount - 1;
        newMax = Mathf.Min(Mathf.Max(newMax, 1), 999);
        thisAnim.maxFrameCount = newMax;

        UpdateFrameSelectText();

        thisAnim.frames.RemoveAt(thisAnim.maxFrameCount);

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

    public void ChangeSelectedFrame()
    {
        int frameId = Convert.ToInt32(frameSelectSlider.value);

        for (int i = 0; i < thisAnim.maxPartCount; i++)
        {
            currentFrame.frameParts[i].part = GameParts[i].sr.sprite;
            currentFrame.frameParts[i].xPos = GameParts[i].transform.position.x;
            currentFrame.frameParts[i].yPos = GameParts[i].transform.position.y;
            currentFrame.frameParts[i].flipX = GameParts[i].sr.flipX;
            currentFrame.frameParts[i].flipY = GameParts[i].sr.flipY;

            if (ghostingPrevious && !playingAnimation && frameSelectSlider.value > currentFrame.frameID)//TODO: apply ghosting next/previous logic
            {
                PreviousGhostParts[i].transform.position = new Vector2(currentFrame.frameParts[i].xPos, currentFrame.frameParts[i].yPos);
                PreviousGhostParts[i].sprite = currentFrame.frameParts[i].part;
                PreviousGhostParts[i].flipX = currentFrame.frameParts[i].flipX;
                PreviousGhostParts[i].flipY = currentFrame.frameParts[i].flipY;
            }
            else
            {
                PreviousGhostParts[i].sprite = null;
            }

            if (copyToNextFrame && frameSelectSlider.value > currentFrame.frameID &&
                thisAnim.frames[frameId].frameParts[i].part == null)
            {
                //Set actual current part data to the last frame part data if copyToNextFrame is true
                thisAnim.frames[frameId].frameParts[i].part = currentFrame.frameParts[i].part;
                thisAnim.frames[frameId].frameParts[i].partIndex = currentFrame.frameParts[i].partIndex;
                thisAnim.frames[frameId].frameParts[i].xPos = currentFrame.frameParts[i].xPos;
                thisAnim.frames[frameId].frameParts[i].yPos = currentFrame.frameParts[i].yPos;
                thisAnim.frames[frameId].frameParts[i].flipX = currentFrame.frameParts[i].flipX;
                thisAnim.frames[frameId].frameParts[i].flipY = currentFrame.frameParts[i].flipY;
            }

            GameParts[i].transform.position = new Vector3(thisAnim.frames[frameId].frameParts[i].xPos, thisAnim.frames[frameId].frameParts[i].yPos, 0);
            GameParts[i].sr.sprite = thisAnim.frames[frameId].frameParts[i].part;
            GameParts[i].sr.flipX = thisAnim.frames[frameId].frameParts[i].flipX;
            GameParts[i].sr.flipY = thisAnim.frames[frameId].frameParts[i].flipY;

            if (!playingAnimation)
            {
                if (GameParts[i].polyColl && GameParts[i].polyColl.enabled)
                {
                    Destroy(GameParts[i].polyColl);
                    GameParts[i].gameObject.AddComponent<PolygonCollider2D>();
                    GameParts[i].polyColl = GameParts[i].GetComponent<PolygonCollider2D>();
                }
            }
        }

        currentFrame = thisAnim.frames[frameId];
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
            GameParts[i].sr.sprite = null;
        }
    }

    public void CopyToNext()
    {
        copyToNextFrame = !copyToNextFrame;
    }

    public void Ghosting()//TODO: apply ghosting next/previous
    {
        ghostingPrevious = !ghostingPrevious;
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

        GameObject newObjPart = Instantiate(gamePartPrefab);
        newObjPart.name = "AnimPart" + (thisAnim.maxPartCount - 1);
        GamePart gamePart = newObjPart.GetComponent<GamePart>();
        gamePart.Initialize(this);
        GameParts.Add(gamePart);
        gamePart.sr.sortingOrder = thisAnim.maxPartCount;

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

        GameParts.RemoveAt(thisAnim.maxPartCount);

        PreviousGhostParts.RemoveAt(thisAnim.maxPartCount);
        NextGhostParts.RemoveAt(thisAnim.maxPartCount);

        partSelectSlider.maxValue = thisAnim.maxPartCount - 1;

        Destroy(GameObject.Find("AnimPart" + thisAnim.maxPartCount));
        Destroy(GameObject.Find("PreviousFrameGhostPart" + thisAnim.maxPartCount));
        Destroy(GameObject.Find("NextFrameGhostPart" + thisAnim.maxPartCount));
    }

    public void AddPartToMultiselect(int _partID)
    {
        //If ?? or part is already selected
        if (_partID >= thisAnim.maxPartCount || currentGameParts.Contains(GameParts[_partID]))
        {
            return;
        }

        currentGameParts.Add(GameParts[_partID]);

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
            currentGameParts.Add(GameParts[i]);

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

        currentGameParts.Add(GameParts[partID]);

        if (!playingAnimation)
        {
            currentGameParts[0].anim.SetBool("WasSelected", true);
        }

        currentParts.Add(currentFrame.frameParts[partID]);
        UpdatePartSelectText();
        UpdatePriorityText();

        if (currentGameParts[0].polyColl != null)
        {
            //if the part from the new frame has no sprite
            if (currentGameParts[0].sr.sprite == null)
            {
                //turn off the box collider
                currentGameParts[0].polyColl.enabled = false;
            }
            else//otherwise
            {
                //turn on the box collider
                currentGameParts[0].polyColl.enabled = true;
            }
        }
    }

    public void UpdateSelectedParts()
    {
        List<int> partIds = new List<int>();

        for (int i = 0; i < currentParts.Count; i++)
        {
            partIds.Add(currentParts[i].partID);
        }

        currentParts.Clear();

        for (int i = 0; i < thisAnim.maxPartCount; i++)
        {
            if (partIds.Contains(i))
            {
                currentParts.Add(currentFrame.frameParts[i]);
            }
        }
    }

    public void FlipX()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].sr.flipX = !currentGameParts[i].sr.flipX;
            currentParts[i].flipX = currentGameParts[i].sr.flipX;
        }
    }

    public void FlipY()
    {
        for (int i = 0; i < currentGameParts.Count; i++)
        {
            currentGameParts[i].sr.flipY = !currentGameParts[i].sr.flipY;
            currentParts[i].flipY = currentGameParts[i].sr.flipY;
        }
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
