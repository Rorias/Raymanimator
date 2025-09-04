using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class TestUtility
{
    public static IEnumerator LoadSelectedGameScene(int _scene)
    {
        if (SceneManager.GetActiveScene().buildIndex != _scene || !SceneManager.GetActiveScene().isLoaded)
        {
            SceneManager.LoadScene(_scene);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == _scene && SceneManager.GetActiveScene().isLoaded);
        }
    }
}
