using System;

using UnityEngine;

public class Background : MonoBehaviour
{
    [NonSerialized] public SpriteRenderer sr;
    [NonSerialized] public PolygonCollider2D polyColl;

    private Camera mainCam;

    private float xDifference;
    private float yDifference;

    private void Awake()
    {
        mainCam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        polyColl = GetComponent<PolygonCollider2D>();
    }

    private void OnMouseDown()
    {
        xDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        yDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        Vector3 dragPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        transform.position = new Vector3(Mathf.Round((dragPos.x - xDifference) * 32.0f) / 32.0f, Mathf.Round((dragPos.y - yDifference) * 32.0f) / 32.0f, 0);
    }
}
