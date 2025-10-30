using System;

using TMPro;

using UnityEngine;

public class ConfirmWindow : Raymanimator
{
    public TMP_Text text;
    public ButtonPlus yesButton;
    public ButtonPlus noButton;

    private void Start()
    {
        noButton.onClick.AddListener(() => { CloseWindow(); });
        CloseWindow();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void OpenWindow(string _windowText, Action _method)
    {
        gameObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() => { _method(); CloseWindow(); });
        text.text = _windowText;
    }
}
