using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class MapperItem : MonoBehaviour
{
    public TMP_Text targetText;
    public Image targetSprite;

    public TMP_Dropdown sourceSpritesDD;

    public void SetSourceSpritesDropdown(Dictionary<int, Sprite> _sourceSpriteset)
    {
        if (sourceSpritesDD.options.Count > 0)
        {
            sourceSpritesDD.options.Clear();
        }

        sourceSpritesDD.AddOptions(_sourceSpriteset.Select(x => x.Value).ToList());

        foreach (KeyValuePair<int, Sprite> mapping in _sourceSpriteset)
        {
            sourceSpritesDD.options[mapping.Key].text = _sourceSpriteset[mapping.Key].name;
            sourceSpritesDD.options[mapping.Key].image = _sourceSpriteset[mapping.Key];
        }
    }

    public void SetTarget(string _name, Sprite _sprite)
    {
        targetText.text = _name;
        targetSprite.sprite = _sprite;
    }
}
