using System;

using TMPro;

using UnityEngine;

public class HotkeyGroup : MonoBehaviour
{
    [NonSerialized] public string hotkeyName;
    [NonSerialized] public TMP_Text hotkeyNameText;
    [NonSerialized] public ButtonPlus key1Btn;
    [NonSerialized] public TMP_Text key1NameText;
    [NonSerialized] public ButtonPlus key2Btn;
    [NonSerialized] public TMP_Text key2NameText;

    public void Initialize()
    {
        hotkeyNameText = transform.GetChild(0).GetComponent<TMP_Text>();

        key1Btn = transform.GetChild(1).GetComponent<ButtonPlus>();
        key1NameText = transform.GetChild(1).GetComponentInChildren<TMP_Text>();

        key2Btn = transform.GetChild(2).GetComponent<ButtonPlus>();
        key2NameText = transform.GetChild(2).GetComponentInChildren<TMP_Text>();
    }
}
