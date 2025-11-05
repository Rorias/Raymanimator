using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindow : Raymanimator
{
    public TMP_Text text;
    public ButtonPlus yesButton;
    public ButtonPlus noButton;
    public Toggle repeatToggle;

    private void Start()
    {
        noButton.onClick.AddListener(() => { CloseWindow(); });
        CloseWindow();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void OpenWindow(string _windowText, Action<bool> _method)
    {
        gameObject.SetActive(true);
        repeatToggle.gameObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() => { _method(!repeatToggle.isOn); CloseWindow(); });
        text.text = _windowText;
    }

    public void OpenWindow(string _windowText, Action _method)
    {
        gameObject.SetActive(true);
        repeatToggle.gameObject.SetActive(false);
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() => { _method(); CloseWindow(); });
        text.text = _windowText;
    }
}
