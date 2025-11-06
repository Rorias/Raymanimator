using UnityEngine;

public class ConsoleGUIDebug : MonoBehaviour
{
    public int fontSize = 24;

    private static string myLog = "";
    private string output;
    private string stack;
    private string prevOutput;

    private bool active = false;

    private void Awake()
    {
        Application.logMessageReceivedThreaded += Log;
        Debug.Log("Generating log files now!");
    }

    private void OnDisable()
    {
        Application.logMessageReceivedThreaded -= Log;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            active = !active;
        }

        if (Input.touchCount > 4)
        {
            Touch touch = Input.GetTouch(4);

            if (touch.phase == TouchPhase.Began)
            {
                active = !active;
            }
        }
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        if (output != prevOutput)
        {
            stack = stackTrace;

            if (string.IsNullOrWhiteSpace(stack))
            {
                myLog = output + "\n" + myLog;
            }
            else
            {
                myLog = output + " STACKTRACE: " + stack + "\n" + myLog;
            }

            if (myLog.Length > 10000)
            {
                myLog = myLog.Substring(0, 9000);
            }

            prevOutput = output;
        }
    }

    private void OnGUI()
    {
        if (active) //Do not display in editor (use #if !UNITY_EDITOR to also disable the rest)
        {
            GUI.skin.textArea.fontSize = fontSize;
            myLog = GUI.TextArea(new Rect(Screen.width / 20f, Screen.height / 20f, Screen.width - (Screen.width / 20f), Screen.height - (Screen.height / 20f)), myLog);
        }
    }
}
