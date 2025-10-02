using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraSetting : Settings
{
    public static event UnityAction OnCameraZoomed;
    public static void CameraZoomed() => OnCameraZoomed?.Invoke();

    [NonSerialized] public float maxCameraZoom = 32f;
    [NonSerialized] public float cameraSpeed = 0.2f;

    public TMP_InputField cameraZoomIF;
    public ButtonPlus cameraZoomReset;

    public TMP_InputField cameraSpeedIF;
    public ButtonPlus cameraSpeedReset;

    public ButtonPlus cameraPositionReset;

    private GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();

        cameraZoomIF.onEndEdit.AddListener(delegate { SetCameraZoom(); });
        cameraZoomReset.onClick.AddListener(delegate { ResetCameraZoom(); });
        cameraSpeedIF.onEndEdit.AddListener(delegate { SetCameraSpeed(); });
        cameraSpeedReset.onClick.AddListener(delegate { ResetCameraSpeed(); });
        cameraPositionReset.onClick.AddListener(delegate { ResetCameraPosition(); });

        OnCameraZoomed += () => InitializeInputFieldValues();
    }

    protected override void Start()
    {
        base.Start();

        gameManager = GameManager.Instance;
        InitializeInputFieldValues();
    }

    private void InitializeInputFieldValues()
    {
        cameraZoomIF.text = gameManager.ParseToString(Camera.main.orthographicSize);
        cameraSpeedIF.text = gameManager.ParseToString(cameraSpeed);
    }

    public void SetCameraZoom()
    {
        float zoomLevel = gameManager.ParseToSingle(cameraZoomIF.text);
        zoomLevel = Mathf.Min(Mathf.Max(zoomLevel, 1), maxCameraZoom);

        Camera.main.orthographicSize = zoomLevel;
        cameraZoomIF.text = gameManager.ParseToString(Camera.main.orthographicSize);
        Debug.Log("Succesfully changed camera zoom to " + Camera.main.orthographicSize.ToString() + ".");
    }

    public void SetCameraSpeed()
    {
        float camSpeed = gameManager.ParseToSingle(cameraSpeedIF.text);
        camSpeed = Mathf.Min(Mathf.Max(camSpeed, 0.025f), 10);

        cameraSpeed = camSpeed;
        cameraSpeedIF.text = gameManager.ParseToString(cameraSpeed);
        Debug.Log("Succesfully changed camera speed to " + cameraSpeed.ToString() + ".");
    }

    public void ResetCameraZoom()
    {
        Camera.main.orthographicSize = 4;
        cameraZoomIF.text = "4";
    }

    public void ResetCameraSpeed()
    {
        cameraSpeed = 0.2f;
        cameraSpeedIF.text = gameManager.ParseToString(cameraSpeed);
    }

    public void ResetCameraPosition()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }
}
