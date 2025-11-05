using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class MappingController : Settings
{
    public ButtonPlus openButton;
    public GameObject animSettingsPanel;
    public GameObject mappingsPanel;
    [Space]
    #region View mappings
    public GameObject viewMappingsPanel;
    public GameObject prefabMappingViewItem;
    public Transform mappingsContent;
    public ButtonPlus addNewMappingBtn;
    #endregion
    [Space]
    #region Create/Edit mapping
    public GameObject createEditMappingPanel;
    public GameObject prefabMapperItem;
    public Transform mapperContent;
    public Toggle binaryToggle;
    public TMP_DropdownPlus sourceSpritesetDD;
    public TMP_DropdownPlus targetSpritesetDD;
    public ButtonPlus saveMappingBtn;
    public ButtonPlus cancelMappingBtn;
    #endregion

    private GameManager gameManager;
    private ThemeController themeController;

    private Dictionary<int, Sprite> sourceSpriteset = new Dictionary<int, Sprite>();
    private Dictionary<int, Sprite> targetSpriteset = new Dictionary<int, Sprite>();

    private List<MapperItem> mapperItems = new List<MapperItem>();
    private List<MappingViewItem> mappingViewItems = new List<MappingViewItem>();

    private bool binary = false;
    private Mapping thisMap;

    protected override void Awake()
    {
        base.Awake();
        gameManager = GameManager.Instance;
        themeController = FindObjectOfType<ThemeController>();

        binaryToggle.onValueChanged.AddListener((_state) => { SetBinaryState(_state); });

        sourceSpritesetDD.onValueChanged.AddListener((_value) => { LoadSourceAndTargetSprites(); });
        targetSpritesetDD.onValueChanged.AddListener((_value) => { LoadSourceAndTargetSprites(); });

        saveMappingBtn.onClick.AddListener(delegate { SaveMapping(); });
        cancelMappingBtn.onClick.AddListener(delegate { EndMapping(); });
        addNewMappingBtn.onClick.AddListener(delegate { InitializeCreateMapping(); });
    }

    private void Start()
    {
        viewMappingsPanel.SetActive(true);
        createEditMappingPanel.SetActive(false);
        InitializeViewMappings();
        CloseMappingWindow();
    }

    public void OpenMappingWindow()
    {
        animSettingsPanel.SetActive(false);
        mappingsPanel.SetActive(true);
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(() => { CloseMappingWindow(); });
    }

    public void CloseMappingWindow()
    {
        mappingsPanel.SetActive(false);
        animSettingsPanel.SetActive(true);
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(() => { OpenMappingWindow(); });
    }

    public void InitializeViewMappings()
    {
        for (int i = 0; i < gameManager.mappings.Count; i++)
        {
            CreateNewViewMapping(i);
        }
        addNewMappingBtn.transform.SetAsLastSibling();
        themeController.ReloadSceneData();
    }

    private void ClearViewMappings()
    {
        for (int i = 0; i < gameManager.mappings.Count + 1; i++)
        {
            Destroy(mappingViewItems[i].gameObject);
        }
        mappingViewItems.Clear();
    }

    private void ClearCreateEditMapping()
    {
        for (int i = targetSpriteset.Count - 1; i >= 0; i--)
        {
            if (mapperItems.Count > 0)
            {
                Destroy(mapperItems[i].gameObject);
                mapperItems.RemoveAt(i);
            }
        }

        sourceSpritesetDD.ClearOptions();
        targetSpritesetDD.ClearOptions();
    }

    public void ReloadViewMappings()
    {
        ClearViewMappings();
        InitializeViewMappings();
    }

    private void CreateNewViewMapping(int _index)
    {
        GameObject mappingViewItemObj = Instantiate(prefabMappingViewItem, mappingsContent);
        MappingViewItem mappingViewItem = mappingViewItemObj.GetComponent<MappingViewItem>();
        mappingViewItem.Initialize(this, gameManager.mappings[_index]);
        mappingViewItems.Add(mappingViewItem);
    }

    public void InitializeCreateMapping()
    {
        thisMap = new Mapping();
        viewMappingsPanel.SetActive(false);
        createEditMappingPanel.SetActive(true);

        sourceSpritesetDD.interactable = true;
        targetSpritesetDD.interactable = true;
        binaryToggle.interactable = true;

        uiUtility.ReloadDropdownSpriteOptions(settings.spritesetsPath, sourceSpritesetDD);
        if (binary)
        {
            targetSpritesetDD.options.Clear();
            List<string> objectSpritesets = Enum.GetNames(typeof(Rayman1MSDOS.DesignObjects)).ToList();
            targetSpritesetDD.AddOptions(objectSpritesets);
        }
        else
        {
            uiUtility.ReloadDropdownSpriteOptions(settings.spritesetsPath, targetSpritesetDD);
        }
    }

    public void InitializeEditMapping(Mapping _map)
    {
        thisMap = _map;
        viewMappingsPanel.SetActive(false);
        createEditMappingPanel.SetActive(true);

        sourceSpritesetDD.interactable = false;
        targetSpritesetDD.interactable = false;
        binaryToggle.interactable = false;

        uiUtility.ReloadDropdownSpriteOptions(settings.spritesetsPath, sourceSpritesetDD);
        uiUtility.ReloadDropdownSpriteOptions(settings.spritesetsPath, targetSpritesetDD);

        int targetIndex = targetSpritesetDD.options.FindIndex(x => x.text == thisMap.MapToSet);
        binaryToggle.isOn = targetIndex == -1;
        if (targetIndex == -1)
        {
            targetSpritesetDD.options.Clear();
            List<string> objectSpritesets = Enum.GetNames(typeof(Rayman1MSDOS.DesignObjects)).ToList();
            targetSpritesetDD.AddOptions(objectSpritesets);
            targetIndex = targetSpritesetDD.options.FindIndex(x => x.text == thisMap.MapToSet);
        }
        //If target index is still not found, the mapping no longer exists
        if (targetIndex == -1)
        {
            DebugHelper.Log("The mapping " + thisMap.MapToSet + " does not exist.", DebugHelper.Severity.warning);
            EndMapping();
        }

        sourceSpritesetDD.value = sourceSpritesetDD.options.FindIndex(x => x.text == thisMap.MapFromSet);
        targetSpritesetDD.value = targetIndex;

        for (int i = 0; i < thisMap.MappingValues.Length; i++)
        {
            mapperItems[i].sourceSpritesDD.value = thisMap.MappingValues[i].s;
        }
    }

    private void LoadSourceAndTargetSprites()
    {
        if (sourceSpritesetDD.value < 0 || targetSpritesetDD.value < 0)
        {
            return;
        }

        sourceSpriteset = uiUtility.LoadSpriteset(sourceSpritesetDD.captionText.text);
        if (binary)
        {
            Rayman1MSDOS.DesignObjects currObject = (Rayman1MSDOS.DesignObjects)Enum.Parse(typeof(Rayman1MSDOS.DesignObjects), targetSpritesetDD.captionText.text);
            targetSpriteset = Rayman1BinaryAnimation.Instance.LoadSpritesetFromBinary(currObject);
        }
        else
        {
            targetSpriteset = uiUtility.LoadSpriteset(targetSpritesetDD.captionText.text);
        }

        for (int i = 0; i < targetSpriteset.Count; i++)
        {
            MapperItem mapperItem;

            if (i >= mapperItems.Count)
            {
                GameObject mapperItemObj = Instantiate(prefabMapperItem, mapperContent);
                mapperItem = mapperItemObj.GetComponent<MapperItem>();
                mapperItem.SetTarget(targetSpriteset[i].name, targetSpriteset[i]);
                mapperItem.SetSourceSpritesDropdown(sourceSpriteset);
                mapperItems.Add(mapperItem);
            }
            else
            {
                mapperItem = mapperItems[i];
                mapperItem.SetTarget(targetSpriteset[i].name, targetSpriteset[i]);
                mapperItem.SetSourceSpritesDropdown(sourceSpriteset);
            }
        }

        for (int i = mapperItems.Count - 1; i >= targetSpriteset.Count; i--)
        {
            Destroy(mapperItems[i].gameObject);
            mapperItems.RemoveAt(i);
        }

        themeController.ReloadSceneData();
    }

    private void SetBinaryState(bool _state)
    {
        binary = _state;
        if (binaryToggle.interactable)
        {
            InitializeCreateMapping();
        }
    }

    private void SaveMapping()
    {
        if (sourceSpritesetDD.value < 0 || targetSpritesetDD.value < 0)
        {
            DebugHelper.Log("Please select a source and target spriteset first.");
            return;
        }

        if (thisMap.MappingValues == null)
        {
            thisMap.MapFromSet = sourceSpritesetDD.captionText.text;
            thisMap.MapToSet = targetSpritesetDD.captionText.text;
            thisMap.MappingValues = new Mapping.Mapper[mapperItems.Count];
            for (int i = 0; i < mapperItems.Count; i++)
            {
                thisMap.MappingValues[i] = new Mapping.Mapper()
                {
                    s = mapperItems[i].sourceSpritesDD.value,
                    t = i,
                };
            }
            string fileName = thisMap.MapFromSet + "To" + thisMap.MapToSet;
            thisMap.SaveMapping(fileName);
            gameManager.AddNewMapping(fileName);
            CreateNewViewMapping(gameManager.mappings.Count - 1);
            addNewMappingBtn.transform.SetAsLastSibling();
        }
        else
        {
            for (int i = 0; i < mapperItems.Count; i++)
            {
                thisMap.MappingValues[i] = new Mapping.Mapper()
                {
                    s = mapperItems[i].sourceSpritesDD.value,
                    t = i,
                };
            }
            string fileName = thisMap.MapFromSet + "To" + thisMap.MapToSet;
            thisMap.SaveMapping(fileName);
        }

        EndMapping();
        themeController.ReloadSceneData();
    }

    private void EndMapping()
    {
        ClearCreateEditMapping();
        createEditMappingPanel.SetActive(false);
        viewMappingsPanel.SetActive(true);
    }
}
