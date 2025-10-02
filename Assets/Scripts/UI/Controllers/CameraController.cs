using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public CameraSetting camSettings;

    private InputManager inputManager;
    private TMP_InputField[] allInputfields;

    private List<RaycastResult> rayResults = new List<RaycastResult>();

    private bool isOnUI = false;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        allInputfields = FindObjectsOfType<TMP_InputField>();
    }

    private void Update()
    {
        foreach (TMP_InputField IF in allInputfields)
        {
            if (IF.isFocused)
            {
                return;
            }
        }

        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current)
        { position = Input.mousePosition, pointerId = -1 }, rayResults);

        isOnUI = rayResults.Count > 0;

        if (!isOnUI)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f)
            {
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + (Input.GetAxisRaw("Mouse ScrollWheel") * -2), 1, camSettings.maxCameraZoom);
                CameraSetting.CameraZoomed();
            }
        }

        if (inputManager.GetKey(InputManager.InputKey.DragCamera))
        {
            //TODO: implement camera drag code by mouse position 
        }

        if (inputManager.GetKey(InputManager.InputKey.MoveCameraUp) && Camera.main.transform.position.y < 24)
        {
            Camera.main.transform.position += (Vector3.up / (camSettings.maxCameraZoom / Camera.main.orthographicSize)) * camSettings.cameraSpeed;
        }
        else if (inputManager.GetKey(InputManager.InputKey.MoveCameraDown) && Camera.main.transform.position.y > -24)
        {
            Camera.main.transform.position += (Vector3.down / (camSettings.maxCameraZoom / Camera.main.orthographicSize)) * camSettings.cameraSpeed;
        }

        if (inputManager.GetKey(InputManager.InputKey.MoveCameraLeft) && Camera.main.transform.position.x > -24)
        {
            Camera.main.transform.position += (Vector3.left / (camSettings.maxCameraZoom / Camera.main.orthographicSize)) * camSettings.cameraSpeed;
        }
        else if (inputManager.GetKey(InputManager.InputKey.MoveCameraRight) && Camera.main.transform.position.x < 24)
        {
            Camera.main.transform.position += (Vector3.right / (camSettings.maxCameraZoom / Camera.main.orthographicSize)) * camSettings.cameraSpeed;
        }
    }
}
