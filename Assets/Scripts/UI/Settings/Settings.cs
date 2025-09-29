using UnityEngine;

public class Settings : MonoBehaviour
{
    protected GameSettings settings;
    protected UIUtility uiUtility;

    protected virtual void Awake()
    {
        settings = GameSettings.Instance;
    }

    protected virtual void Start()
    {
        uiUtility = FindObjectOfType<UIUtility>();
    }
}
