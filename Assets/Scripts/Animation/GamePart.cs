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

    private InputManager inputManager = InputManager.Instance;
    private AnimatorController animatorC;
    private Camera mainCam;

    private float xDifference;
    private float yDifference;

    private float doubleClickTimer = 0.0f;
    private float maxDoubleClickTime = 0.4f;

    private bool canDoubleClick = false;

    public void Initialize(AnimatorController _animatorC)
    {
        multipleSelected = false;
        animatorC = _animatorC;
        mainCam = Camera.main;

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
        xDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        yDifference = mainCam.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        if (inputManager.GetKey(InputManager.InputKey.MultiSelect))
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

            doubleClickTimer = maxDoubleClickTime;
            canDoubleClick = true;
        }
    }

    private void OnMouseDrag()
    {
        //TODO: update to animatorcontroller call that moves all parts based on the dragged one
        transform.position = new Vector3(Mathf.Round((mainCam.ScreenToWorldPoint(Input.mousePosition).x - xDifference) * 32.0f) / 32.0f, Mathf.Round((mainCam.ScreenToWorldPoint(Input.mousePosition).y - yDifference) * 32.0f) / 32.0f, 0);
        animatorC.UpdatePos();
    }
}
