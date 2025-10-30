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
    public ColorSlider gridOpacity;

    private Animation thisAnim;

    private float gridDetail = 2.0f;

    protected override void Awake()
    {
        base.Awake();
        gameManager = GameManager.Instance;

        gridLODIF.onEndEdit.AddListener(delegate { SetGridLOD(); });
        gridXsizeIF.onEndEdit.AddListener(delegate { SetGridXsize(); });
        gridYsizeIF.onEndEdit.AddListener(delegate { SetGridYsize(); });
        gridToggle.onValueChanged.AddListener(delegate { GridState(); });
        gridOpacity.UpdateAlpha += SetOpacity;
        gridOpacity.SetAlpha += SaveColor;
        gridOpacity.Reset += ResetColor;

        thisAnim = gameManager.currentAnimation;
    }

    protected void Start()
    {
        if(thisAnim == null)
        {
            DebugHelper.Log("No proper animation was loaded during initiatization.", DebugHelper.Severity.critical);
            Debug.Log("No proper animation was loaded during initiatization.");
            return;
        }

        SetGrid();
        InitializeInputFieldValues();
        gridOpacity.SetColorSliders(settings.gridOpacity);
    }

    private void InitializeInputFieldValues()
    {
        gridXsizeIF.text = thisAnim.gridSizeX.ToString();
        gridYsizeIF.text = thisAnim.gridSizeY.ToString();

        if (gridLODIF != null)
        {
            gridLODIF.text = gameManager.ParseToString(gridDetail);
        }
    }

    public void SetGrid()
    {
        if (grid != null)
        {
            grid.localScale = new Vector2(thisAnim.gridSizeX / 16f, thisAnim.gridSizeY / 16f);
            gridMaterial.mainTextureScale = new Vector2(thisAnim.gridSizeX / gridDetail, thisAnim.gridSizeY / gridDetail);
        }
    }

    public void SetGridXsize()
    {
        int.TryParse(gridXsizeIF.text, out int conv);
        thisAnim.gridSizeX = Mathf.Min(Mathf.Max(conv, 1), 4095);
        gridXsizeIF.text = thisAnim.gridSizeX.ToString();
        SetGrid();
    }

    public void SetGridYsize()
    {
        int.TryParse(gridYsizeIF.text, out int conv);
        thisAnim.gridSizeY = Mathf.Min(Mathf.Max(conv, 1), 4095);
        gridYsizeIF.text = thisAnim.gridSizeY.ToString();
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

    public void SetOpacity()
    {
        gridMaterial.color = gridOpacity.GetColorBarAlpha(gridMaterial.color);
    }

    public void SaveColor()
    {
        settings.gridOpacity = gridMaterial.color;
        settings.SaveSettings();
    }

    public void ResetColor()
    {
        gridMaterial.color = new Color32(255, 255, 255, 100);
        gridOpacity.SetColorSliders(gridMaterial.color);
        SaveColor();
    }
}
