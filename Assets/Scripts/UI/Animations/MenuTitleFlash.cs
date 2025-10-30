using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class MenuTitleFlash : Raymanimator
{
    public TMP_FontAsset[] cycleFonts;

    public float cycleTime;
    private float cycleTimer;

    private TMP_Text text;
    private int currIndex;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        cycleTimer -= Time.fixedDeltaTime;

        if (cycleTimer < 0)
        {
            cycleTimer = cycleTime;
            text.font = cycleFonts[currIndex];
            currIndex++;

            if (currIndex >= cycleFonts.Length)
            {
                currIndex = 0;
            }
        }
    }
}

