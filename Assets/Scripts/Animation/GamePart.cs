using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePart : MonoBehaviour
{
    private static bool multipleSelected = false;

    [NonSerialized] public SpriteRenderer sr;
    [NonSerialized] public PolygonCollider2D polyColl;
    [NonSerialized] public Animator anim;

    private InputManager input = InputManager.Instance;
    private AnimatorController animatorC;
    private Camera mainCam;
    private List<RaycastResult> results = new List<RaycastResult>();

    public float xDifference { get; private set; }
    public float yDifference { get; private set; }

    private float doubleClickTimer = 0.0f;
    private float maxDoubleClickTime = 0.4f;

    private bool canDoubleClick = false;

    private void Awake()
    {
        mainCam = Camera.main;

        sr = GetComponent<SpriteRenderer>();
        polyColl = GetComponent<PolygonCollider2D>();
    }

    public void Initialize(AnimatorController _animatorC)
    {
        multipleSelected = false;
        animatorC = _animatorC;

        sr = GetComponent<SpriteRenderer>();
        polyColl = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        doubleClickTimer -= Time.fixedDeltaTime;

        if (doubleClickTimer <= 0)
        {
            canDoubleClick = false;
        }
    }

    private void OnMouseDown()
    {
        UIUtility.GetRayResults();
        results.Clear();
        for (int i = 0; i < UIUtility.rayResults.Count; i++)
        {
            results.Add(UIUtility.rayResults[i]);
        }
        if (results.Count > 0) { return; }

        if (input.GetKey(InputManager.InputKey.MultiSelect))
        {
            multipleSelected = true;

            if (canDoubleClick)
            {
                animatorC.SelectAllParts();
            }
            else
            {
                animatorC.AddPartToMultiselect(Convert.ToInt32(gameObject.name.Substring(8)));
            }
        }
        else
        {
            if (canDoubleClick || !multipleSelected)
            {
                animatorC.partSelectSlider.value = Convert.ToInt32(gameObject.name.Substring(8));
                animatorC.ChangeSelectedPart();
                multipleSelected = false;
            }
        }

        doubleClickTimer = maxDoubleClickTime;
        canDoubleClick = true;

        animatorC.SetOffsetForSelectedParts();
    }

    private void OnMouseDrag()
    {
        if (results.Count > 0) { return; }

        Vector3 dragPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        animatorC.DragSelectedParts(dragPos);
        animatorC.UpdatePos();
    }

    public void SetOffset()
    {
        xDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        yDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }

    public void RecalculateCollision()
    {
        if (polyColl.enabled && sr.sprite != null)
        {
            polyColl.pathCount = 0;
            polyColl.pathCount = 1;

            List<Vector2> path = new List<Vector2>();
            for (int p = 0; p < polyColl.pathCount; p++)
            {
                //Skip sprites that are too small from generating a physics shape as it throws errors?
                if(sr.sprite.texture.width < 2 || sr.sprite.texture.height < 2)
                {
                    continue;
                }

                sr.sprite.GetPhysicsShape(p, path);
                if (path.Count > 0)
                {
                    for (int n = 0; n < path.Count; n++)
                    {
                        float x = sr.flipX ? -path[n].x : path[n].x;
                        float y = sr.flipY ? -path[n].y : path[n].y;
                        path[n] = new Vector2(x, y);
                    }
                    polyColl.SetPath(p, path);
                }
            }
        }
    }
}
