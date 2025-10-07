using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    private GameSettings settings;

    private void Awake()
    {
        settings = GameSettings.Instance;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _lsm)
    {
        UpdateLanguage();
    }

    public void UpdateLanguage()
    {

    }
}
