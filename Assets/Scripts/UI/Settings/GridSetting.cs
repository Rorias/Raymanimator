using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GridSetting : Settings
{
    private GameManager gameManager;

    public Transform grid;
    public Material gridMaterial;

    public Toggle gridToggle;
    public TMP_InputField gridLODIF;
    public TMP_InputField gridXsizeIF;
    public TMP_InputField gridYsizeIF;

    private float gridDetail = 2.0f;

    protected override void Awake()
    {
        base.Awake();

        gridLODIF.onEndEdit.AddListener(delegate { SetGridLOD(); });
        gridXsizeIF.onEndEdit.AddListener(delegate { SetGridXsize(); });
        gridYsizeIF.onEndEdit.AddListener(delegate { SetGridYsize(); });
        gridToggle.onValueChanged.AddListener(delegate { GridState(); });
    }

    protected override void Start()
    {
        base.Start();

        gameManager = GameManager.Instance;

        SetGrid();
        InitializeInputFieldValues();
    }

    private void InitializeInputFieldValues()
    {
        gridXsizeIF.text = gameManager.currentAnimation.gridSizeX.ToString();
        gridYsizeIF.text = gameManager.currentAnimation.gridSizeY.ToString();

        if (gridLODIF != null)
        {
            gridLODIF.text = gameManager.ParseToString(gridDetail);
        }
    }

    public void SetGrid()
    {
        if (grid != null)
        {
            grid.localScale = new Vector2(gameManager.currentAnimation.gridSizeX / 16f, gameManager.currentAnimation.gridSizeY / 16f);
            gridMaterial.mainTextureScale = new Vector2(gameManager.currentAnimation.gridSizeX / gridDetail, gameManager.currentAnimation.gridSizeY / gridDetail);
        }
    }

    public void SetGridXsize()
    {
        int.TryParse(gridXsizeIF.text, out int conv);
        gameManager.currentAnimation.gridSizeX = Mathf.Min(Mathf.Max(conv, 1), 4095);
        gridXsizeIF.text = gameManager.currentAnimation.gridSizeX.ToString();
        SetGrid();
    }

    public void SetGridYsize()
    {
        int.TryParse(gridYsizeIF.text, out int conv);
        gameManager.currentAnimation.gridSizeY = Mathf.Min(Mathf.Max(conv, 1), 4095);
        gridYsizeIF.text = gameManager.currentAnimation.gridSizeY.ToString();
        SetGrid();
    }

    public void SetGridLOD() // Level of Detail
    {
        float gridLOD = gameManager.ParseToSingle(gridLODIF.text);
        gridDetail = Mathf.Min(Mathf.Max(gridLOD, 1), 8);
        gridLODIF.text = gameManager.ParseToString(gridDetail);
        SetGrid();
    }

    public void GridState()
    {
        grid.gameObject.SetActive(!grid.gameObject.activeSelf);
    }
}
