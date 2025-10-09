using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GhostingSetting : Settings
{
    public Toggle ghostingPreviousToggle;
    public Toggle ghostingNextToggle;


    protected override void Awake()
    {
        base.Awake();

        //ghostingNextToggle.onValueChanged.AddListener(delegate { SetSpritesetPath(); });
        //ghostingPreviousToggle.onValueChanged.AddListener(delegate { SetSpritesetPath(); });
    }
}
