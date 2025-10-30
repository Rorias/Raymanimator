using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class MappingViewItem : Raymanimator
{
    public Toggle stateToggle;
    public ButtonPlus editButton;
    public ButtonPlus deleteButton;

    private ConfirmWindow deleteConfirmWindow;
    private MappingController mapControl;
    public Mapping map { get; private set; }

    private string fileName;

    public void Initialize(MappingController _mapControl, Mapping _map)
    {
        deleteConfirmWindow = FindObjectOfType<ConfirmWindow>(true);
        mapControl = _mapControl;
        map = _map;

        fileName = map.MapFromSet + "To" + map.MapToSet;
        stateToggle.onValueChanged.AddListener((_state) => { map.Enabled = _state; map.SaveMapping(fileName); });
        editButton.onClick.AddListener(delegate { mapControl.InitializeEditMapping(map); });
        deleteButton.onClick.AddListener(delegate { deleteConfirmWindow.OpenWindow("Are you sure you want to delete " + fileName + "?", DeleteMapping); });

        stateToggle.isOn = map.Enabled;
        string spaced = Regex.Replace(fileName, "(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", " $0", RegexOptions.IgnorePatternWhitespace).Trim().ToLower();
        stateToggle.GetComponentInChildren<TMP_Text>().text = spaced;
    }

    public void UpdateMap()
    {
        
    }

    public void DeleteMapping()
    {
        if (!File.Exists(Mapping.filePath + fileName + ".json"))
        {
            DebugHelper.Log("The mapping cannot be deleted because it no longer exists.", DebugHelper.Severity.warning);
            return;
        }

        File.Delete(Mapping.filePath + fileName + ".json");
        if (File.Exists(Mapping.filePath + fileName + ".json.meta"))
        {
            File.Delete(Mapping.filePath + fileName + ".json.meta");
        }

        GameManager.Instance.mappings.Remove(map);
        mapControl.ReloadViewMappings();
    }
}
