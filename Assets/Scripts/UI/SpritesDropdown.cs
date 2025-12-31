using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class SpritesDropdown : MonoBehaviour
{
    public TMP_DropdownPlus ddSprites;
    public bool dropdownActive { get; private set; } = false;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        dropdownActive = transform.Find("Dropdown List") is not null;
    }

    public void InitializeSpritesDropdown()
    {
        if (gameManager.spritesetImages.Count <= 0)
        {
            DebugHelper.Log("Spriteset loading failed.", DebugHelper.Severity.critical);
            Debug.Log("Spriteset loading failed.");
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
            _currentGameParts[i].sr.sprite = _currentParts[i].part != null ? CreateSpriteWithPivot(_currentParts[i].part, new Vector2(Convert.ToInt32(_currentParts[i].flipX), Convert.ToInt32(!_currentParts[i].flipY))) : null;
            _currentGameParts[i].anim.SetBool("WasSelected", true);
            _currentGameParts[i].polyColl.enabled = true;
            _currentGameParts[i].RecalculateCollision();
        }
    }

    private Sprite CreateSpriteWithPivot(Sprite existingSprite, Vector2 pivot)
    {
        return Sprite.Create(existingSprite.texture, existingSprite.rect, pivot, existingSprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, new Vector4(0, 0, 0, 0), true);
    }
}
