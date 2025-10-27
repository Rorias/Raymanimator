using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpritesDropdown : MonoBehaviour
{
    public TMP_Dropdown ddSprites;

    public bool dropdownActive { get; private set; } = false;

    private GameManager gameManager;

    private float lastOpenedPositionDD = 0f;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (!dropdownActive)
        {
            if (transform.Find("Dropdown List") is null)
            {
                return;
            }

            dropdownActive = true;
            GameObject spriteContent = GameObject.Find("SpritesContent");
            RectTransform contentRect = spriteContent.GetComponent<RectTransform>();
            contentRect.position = new Vector3(contentRect.position.x, lastOpenedPositionDD, contentRect.position.z);

            for (int i = 1; i < spriteContent.transform.childCount; i++)
            {
                Image currentImage = spriteContent.transform.GetChild(i).Find("Item Background").GetComponent<Image>();
                currentImage.sprite = ddSprites.options[i - 1].image;
            }
        }
        else if (transform.Find("Dropdown List") is null)
        {
            dropdownActive = false;
        }
    }

    public void InitializeSpritesDropdown()
    {
        if (gameManager.spritesetImages.Count <= 0)
        {
            Debug.LogError("Spriteset loading failed.");
            SceneManager.LoadScene(0);
            return;
        }

        ddSprites.AddOptions(gameManager.spritesetImages.Select(x => x.Value).ToList());

        foreach (KeyValuePair<int, Sprite> mapping in gameManager.spritesetImages)
        {
            ddSprites.options[mapping.Key].text = gameManager.spritesetImages[mapping.Key].name;
            ddSprites.options[mapping.Key].image = gameManager.spritesetImages[mapping.Key];
        }
    }

    public void SetSpriteForSelectedParts(List<Part> _currentParts, List<GamePart> _currentGameParts)
    {
        for (int i = 0; i < _currentParts.Count; i++)
        {
            _currentParts[i].part = ddSprites.options[ddSprites.value].image;
            _currentParts[i].partIndex = ddSprites.value;
            _currentGameParts[i].sr.sprite = _currentParts[i].part;
            _currentGameParts[i].anim.SetBool("WasSelected", true);
        }

        lastOpenedPositionDD = GameObject.Find("SpritesContent").GetComponent<RectTransform>().position.y;
    }
}
