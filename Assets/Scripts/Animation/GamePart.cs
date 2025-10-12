using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
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

    public float xDifference { get; private set; }
    public float yDifference { get; private set; }

    private float doubleClickTimer = 0.0f;
    private float maxDoubleClickTime = 0.4f;

    private bool canDoubleClick = false;

    private void Awake()
    {
        mainCam = Camera.main;

        sr = GetComponent<SpriteRenderer>();
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
        Vector3 dragPos = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        animatorC.DragSelectedParts(dragPos);
        animatorC.UpdatePos();
    }

    public void SetOffset()
    {
        xDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        yDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }
}
