using UnityEngine;

public class Settings : MonoBehaviour
{
    protected GameSettings settings;
    protected UIUtility uiUtility;

    protected virtual void Start()
    {
        settings = GameSettings.Instance;
        uiUtility = FindObjectOfType<UIUtility>();
    }
}
