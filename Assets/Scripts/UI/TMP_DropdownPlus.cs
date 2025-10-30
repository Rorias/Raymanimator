using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

using UnityEngine;
using UnityEngine.EventSystems;

public class TMP_DropdownPlus : TMP_Dropdown
{
    public bool openLastPosition;

    private int baseChildCount;
    private float lastOpenedPositionDD = 0f;

    protected override void Awake()
    {
        base.Awake();
        onValueChanged.AddListener(delegate
        {
            if (transform.Find("Dropdown List") != null)
            {
                lastOpenedPositionDD = transform.Find("Dropdown List").GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition.y;
            }
        });

        baseChildCount = transform.childCount;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        bool alreadyOpen = transform.childCount != baseChildCount;
        base.OnPointerClick(eventData);
        if (openLastPosition && !alreadyOpen && interactable)
        {
            RectTransform contentRect = transform.Find("Dropdown List").GetChild(0).GetChild(0).GetComponent<RectTransform>();
            contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, lastOpenedPositionDD);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TMP_DropdownPlus)), CanEditMultipleObjects]
public class TMP_DropdownPlusEditor : DropdownEditor
{
    //Show all events on inspector GUI
    public override void OnInspectorGUI()
    {
        SerializedProperty openLastPosition = serializedObject.FindProperty("openLastPosition");
        EditorGUILayout.PropertyField(openLastPosition, new GUIContent("Open Last Position"), true);
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
#endif