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

    private GameManager gameManager = GameManager.Instance;

    private Dictionary<int, Sprite> possibleSprites = new Dictionary<int, Sprite>();

    private float lastOpenedPositionDD = 0f;

    private void Update()
    {
        if (!dropdownActive && null != GameObject.Find("Dropdown List"))
        {
            dropdownActive = true;
            GameObject spriteContent = GameObject.Find("SpritesContent");
            RectTransform contentRect = spriteContent.GetComponent<RectTransform>();
            contentRect.position = new Vector3(contentRect.position.x, lastOpenedPositionDD, contentRect.position.z);

            for (int i = 1; i < spriteContent.transform.childCount; i++)//TODO: look into optimizing this somehow
            {
                Image currentImage = spriteContent.transform.GetChild(i).Find("Item Background").GetComponent<Image>();
                currentImage.sprite = ddSprites.options[i - 1].image;
            }
        }
        else if (dropdownActive && null == GameObject.Find("Dropdown List"))
        {
            dropdownActive = false;
        }
    }

    public void InitializeSpritesDropdown()
    {
        if (gameManager.spritesetImages.Count > 0)
        {
            foreach (KeyValuePair<int, Sprite> mapping in gameManager.spritesetImages)
            {
                possibleSprites.Add(mapping.Key, mapping.Value);
            }
        }
        else
        {
            Debug.LogError("Spriteset loading failed.");
            SceneManager.LoadScene(0);
            return;
        }

        if (possibleSprites.Count > 0)
        {
            ddSprites.AddOptions(possibleSprites.Select(x => x.Value).ToList());
        }
        else
        {
            Debug.Log("Possible sprites list is empty.");
        }

        foreach (KeyValuePair<int, Sprite> mapping in possibleSprites)
        {
            ddSprites.options[mapping.Key].text = possibleSprites[mapping.Key].name;
            ddSprites.options[mapping.Key].image = possibleSprites[mapping.Key];
        }
    }

    public void SetSpriteForSelectedParts(List<Part> _currentParts, List<GamePart> _currentGameParts)
    {
        for (int i = 0; i < _currentParts.Count; i++)
        {
            _currentParts[i].part = ddSprites.options[ddSprites.value].image;
            _currentParts[i].partIndex = ddSprites.value;
            _currentGameParts[i].sr.sprite = _currentParts[i].part;

            //When setting a sprite for the gamePart, turn on the poly collider
            if (_currentGameParts[i].polyColl != null)
            {
                Destroy(_currentGameParts[i].polyColl);
            }
            _currentGameParts[i].gameObject.AddComponent<PolygonCollider2D>();
            _currentGameParts[i].polyColl = _currentGameParts[i].GetComponent<PolygonCollider2D>();
            _currentGameParts[i].anim.SetBool("WasSelected", true);
        }

        lastOpenedPositionDD = GameObject.Find("SpritesContent").GetComponent<RectTransform>().position.y;
    }
}
