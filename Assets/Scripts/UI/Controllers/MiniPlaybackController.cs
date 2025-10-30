using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MiniPlaybackController : Raymanimator
{
    public GameObject gamePartPrefab;
    public SpritesDropdown spritesDDController;
    public TMP_DropdownPlus ddSprites;

    private GameManager gameManager;
    private GameSettings settings;

    private Frame currentFrame;

    private List<GamePart> gameParts = new List<GamePart>();

    private Animation thisAnim;

    private Vector3 nextFramePosition;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        settings = GameSettings.Instance;
    }

    public void StartMiniPlayback()
    {
        thisAnim = gameManager.currentAnimation;
        DestroyPreviousPlayback();

        spritesDDController.InitializeSpritesDropdown();

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
        }

        currentFrame = thisAnim.frames[0];

        StartCoroutine(StartAnimation());
    }

    public void DestroyPreviousPlayback()
    {
        if (gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
        }
        ddSprites.ClearOptions();
        for (int i = 0; i < gameParts.Count; i++)
        {
            Destroy(gameParts[i].gameObject);
        }
        gameParts.Clear();
    }

    private void CreatePart(int _part)
    {
        GameObject gameObjPart = Instantiate(gamePartPrefab);
        gameObjPart.name = "AnimPart" + _part;
        GamePart gamePart = gameObjPart.GetComponent<GamePart>();

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
        gamePart.polyColl.enabled = false;
        gamePart.anim.enabled = false;
    }

    private IEnumerator StartAnimation()
    {
        while (true)
        {
            float targetTime = Time.time + ((1f / (float)settings.lastPlaybackSpeed) * (float)thisAnim.maxFrameCount);
            float offset = 0.0f;
            for (int animFrames = 0; animFrames < thisAnim.maxFrameCount; animFrames++)
            {
                float frameTime = Time.time;
                ChangeSelectedFrame(animFrames);
                yield return new WaitForSecondsRealtime(((targetTime - Time.time) / (thisAnim.maxFrameCount - animFrames)) - offset);
                offset += (Time.time - frameTime) - (1f / (float)settings.lastPlaybackSpeed);
            }
        }
    }

    private void ChangeSelectedFrame(float _value)
    {
        int frameId = Convert.ToInt32(_value);

        for (int i = 0; i < thisAnim.maxPartCount; i++)
        {
            currentFrame.frameParts[i].xPos = gameParts[i].transform.position.x;
            currentFrame.frameParts[i].yPos = gameParts[i].transform.position.y;
            currentFrame.frameParts[i].flipX = gameParts[i].sr.flipX;
            currentFrame.frameParts[i].flipY = gameParts[i].sr.flipY;
            currentFrame.frameParts[i].part = gameParts[i].sr.sprite;

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
        }

        currentFrame = thisAnim.frames[frameId];//set current frame to selected frame from the frame selector
    }

    private Sprite CreateSpriteWithPivot(Sprite existingSprite, Vector2 pivot)
    {
        return Sprite.Create(existingSprite.texture, existingSprite.rect, pivot, existingSprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), true);
    }
}
