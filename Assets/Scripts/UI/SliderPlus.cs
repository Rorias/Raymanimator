#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderPlus : Slider
{
    public UnityEvent<float> onMouseUp = new UnityEvent<float>();

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onMouseUp.Invoke(value);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SliderPlus)), CanEditMultipleObjects]
public class SliderPlusEditor : SliderEditor
{
    //Show all events on inspector GUI
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SerializedProperty onMouseUp = serializedObject.FindProperty("onMouseUp");
        EditorGUILayout.PropertyField(onMouseUp, new GUIContent("On Mouse Up"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
