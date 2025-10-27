using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class MappingController : Settings
{
    public GameObject prefabMapperItem;

    public Toggle binaryToggle;
    public TMP_Dropdown sourceSpritesetDD;
    public TMP_Dropdown targetSpritesetDD;

    public Transform mappingContent;

    public ButtonPlus saveMappingBtn;

    private Dictionary<int, Sprite> sourceSpriteset = new Dictionary<int, Sprite>();
    private Dictionary<int, Sprite> targetSpriteset = new Dictionary<int, Sprite>();

    private List<MapperItem> mapperItems = new List<MapperItem>();

    private bool binary = false;

    protected override void Awake()
    {
        base.Awake();

        binaryToggle.onValueChanged.AddListener((_state) => { SetBinaryState(_state); });

        sourceSpritesetDD.onValueChanged.AddListener((_value) => { LoadSourceAndTargetSprites(); });
        targetSpritesetDD.onValueChanged.AddListener((_value) => { LoadSourceAndTargetSprites(); });

        saveMappingBtn.onClick.AddListener(delegate { SaveMapping(); });
    }

    private void OnEnable()
    {
        InitializeNewMapping();
    }

    private void InitializeNewMapping()
    {
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

    private void LoadSourceAndTargetSprites()
    {
        if (sourceSpritesetDD.value < 0 || targetSpritesetDD.value < 0)
        {
            return;
        }

        sourceSpriteset = uiUtility.LoadSpriteset(sourceSpritesetDD.captionText.text);
        if (binary)
        {
            targetSpriteset = Rayman1BinaryAnimation.Instance.LoadSpritesetFromBinary(targetSpritesetDD.value);
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
                GameObject mapperItemObj = Instantiate(prefabMapperItem, mappingContent);
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
    }

    private void SetBinaryState(bool _state)
    {
        binary = _state;
        InitializeNewMapping();
    }

    private void SaveMapping()
    {
        Mapping m = new Mapping();
        m.MapFromSet = sourceSpritesetDD.captionText.text;
        m.MapToSet = targetSpritesetDD.captionText.text;
        m.MappingValues = new Mapping.Mapper[mapperItems.Count];
        for (int i = 0; i < mapperItems.Count; i++)
        {
            m.MappingValues[i] = new Mapping.Mapper()
            {
                s = mapperItems[i].sourceSpritesDD.value,
                t = i,
            };
        }
        m.SaveMapping(m.MapFromSet + "To" + m.MapToSet);
        GameManager.Instance.LoadMappings();
    }
}
