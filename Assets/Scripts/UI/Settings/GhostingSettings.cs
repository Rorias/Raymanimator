using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GhostingSettings : MonoBehaviour
{
    public Toggle ghostingNextToggle;
    public Toggle ghostingPreviousToggle;


    private void Awake()
    {
        //ghostingNextToggle.onValueChanged.AddListener(delegate { SetSpritesetPath(); });
        //ghostingPreviousToggle.onValueChanged.AddListener(delegate { SetSpritesetPath(); });
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}

