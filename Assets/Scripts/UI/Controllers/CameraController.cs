using TMPro;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraSetting camSettings;

    private InputManager input;
    private TMP_InputField[] allInputfields;
    private Camera mainCam;

    private Vector3 origin;

    private float maxZoom;
    private float speed;

    private bool isOnUI = false;

    private void Awake()
    {
        input = InputManager.Instance;
        allInputfields = FindObjectsOfType<TMP_InputField>();
        mainCam = Camera.main;
    }

    private void Start()
    {
        maxZoom = camSettings == null ? 4 : camSettings.maxCameraZoom;
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

        speed = camSettings == null ? 1 : camSettings.cameraSpeed;

        UIUtility.GetRayResults();

        isOnUI = UIUtility.rayResults.Count > 0;

        if (!isOnUI)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f)
            {
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + (Input.GetAxisRaw("Mouse ScrollWheel") * -2), 1, maxZoom);
                CameraSetting.CameraZoomed();
            }
        }

        if (input.GetKeyDown(InputManager.InputKey.DragCamera))
        {
            origin = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (input.GetKey(InputManager.InputKey.DragCamera))
        {
            Vector3 difference = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.position = origin - difference;
        }

        if (input.GetKey(InputManager.InputKey.MoveCameraUp) && Camera.main.transform.position.y < 24)
        {
            Camera.main.transform.position += (Vector3.up / (maxZoom / Camera.main.orthographicSize)) * speed;
        }
        else if (input.GetKey(InputManager.InputKey.MoveCameraDown) && Camera.main.transform.position.y > -24)
        {
            Camera.main.transform.position += (Vector3.down / (maxZoom / Camera.main.orthographicSize)) * speed;
        }

        if (input.GetKey(InputManager.InputKey.MoveCameraLeft) && Camera.main.transform.position.x > -24)
        {
            Camera.main.transform.position += (Vector3.left / (maxZoom / Camera.main.orthographicSize)) * speed;
        }
        else if (input.GetKey(InputManager.InputKey.MoveCameraRight) && Camera.main.transform.position.x < 24)
        {
            Camera.main.transform.position += (Vector3.right / (maxZoom / Camera.main.orthographicSize)) * speed;
        }
    }
}
