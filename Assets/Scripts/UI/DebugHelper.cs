using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour
{
    private static TMP_Text debugText;
    public enum Severity { none, warning, error, critical }

    private static readonly Color none = new Color(1, 1, 1);
    private static readonly Color warning = new Color(1, 0.8f, 0);
    private static readonly Color error = new Color(1, 0.4f, 0);
    private static readonly Color critical = new Color(1, 0, 0);

    private static float disappearTime = 5.0f;
    private static bool visible = false;

    private void Awake()
    {
        debugText = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        if (visible)
        {
            disappearTime -= Time.fixedDeltaTime;

            if (disappearTime <= 0 && debugText.color.a > 0)
            {
                Color c = debugText.color;
                c.a -= 0.1f;
                debugText.color = c;
            }

            if (debugText.color.a <= 0)
            {
                debugText.text = "";
                visible = false;
            }
        }
    }

    public static void Log(string _text, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        debugText.text = _text;
        debugText.color = none;
        disappearTime = 3;
        visible = true;

        string _location = "[" + sourceFilePath.Substring(sourceFilePath.LastIndexOf('\\') + 1) + " > " + memberName + "():" + sourceLineNumber + "]";
    }

    public static void Log(string _text, Severity _severity, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        debugText.text = _text;
        disappearTime = 5;
        visible = true;

        switch (_severity)
        {
            case Severity.none:
                debugText.color = none;
                break;
            case Severity.warning:
                debugText.color = warning;
                break;
            case Severity.error:
                debugText.color = error;
                break;
            case Severity.critical:
                debugText.color = critical;
                break;
        }

        string _location = "[" + sourceFilePath.Substring(sourceFilePath.LastIndexOf('\\') + 1) + " > " + memberName + "():" + sourceLineNumber + "]";
    }
}
