using UnityEngine.SceneManagement;

public class LanguageController : Raymanimator
{
    private void Awake()
    {
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
