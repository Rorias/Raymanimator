using System.Diagnostics;

using UnityEngine;
using UnityEngine.Events;

public class OptionsMenu : MonoBehaviour
{
    public UnityEvent initializeSettings;

    public void Initialize()
    {
        initializeSettings.Invoke();
    }

    public void OpenSettingsFile()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            Arguments = Application.persistentDataPath + "/RaymanimatorSettings.json",
            FileName = "notepad.exe",
        };

        Process.Start(startInfo);
    }
}
