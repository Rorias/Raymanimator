using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPlus : Button
{
    //Should this button ignore validation?
    public bool ignoreValidation = false;
    public UnityEvent onMouseEnter = new UnityEvent();
    public UnityEvent onMouseExit = new UnityEvent();
    public UnityEvent onButtonDeselect = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();
        onClick.AddListener(() =>
        {
            //Plays the button sound attached to an AudioSource on object "ButtonSound".
            GameObject.Find("ButtonSound")?.GetComponent<AudioSource>()?.Play();
            //Feel free to add more features here...
        });
    }

    public void SetBaseState()
    {
        base.DoStateTransition(SelectionState.Normal, false);
    }

    public void SetHighlightedState()
    {
        base.DoStateTransition(SelectionState.Highlighted, false);
    }

    public void SetSelectedState()
    {
        base.DoStateTransition(SelectionState.Selected, false);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onMouseEnter.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onMouseExit.Invoke();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        onButtonDeselect.Invoke();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        // Updates the subcomponent of the button to have the buttons name with "Text" appended
        if (transform.GetChild(0) != null && transform.GetChild(0).GetComponent<TMPro.TMP_Text>())
        {
            transform.GetChild(0).gameObject.name = gameObject.name + "Text";
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonPlus)), CanEditMultipleObjects]
public class ButtonPlusEditor : ButtonEditor
{
    //Show ignore validation toggle and all events on inspector GUI
    public override void OnInspectorGUI()
    {
        SerializedProperty ignoreValidation = serializedObject.FindProperty("ignoreValidation");
        EditorGUILayout.PropertyField(ignoreValidation, new GUIContent("Ignore Validation"), true);
        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();

        SerializedProperty onMouseEnter = serializedObject.FindProperty("onMouseEnter");
        EditorGUILayout.PropertyField(onMouseEnter, new GUIContent("On Mouse Enter"), true);
        serializedObject.ApplyModifiedProperties();

        SerializedProperty onMouseExit = serializedObject.FindProperty("onMouseExit");
        EditorGUILayout.PropertyField(onMouseExit, new GUIContent("On Mouse Exit"), true);
        serializedObject.ApplyModifiedProperties();

        SerializedProperty onButtonDeselect = serializedObject.FindProperty("onButtonDeselect");
        EditorGUILayout.PropertyField(onButtonDeselect, new GUIContent("On Button Deselect"), true);
        serializedObject.ApplyModifiedProperties();
    }
}

public class ReplaceDefaultButtons : EditorWindow
{
    #region GUI Creation
    [UnityEditor.MenuItem("Window/Replace Buttons")]
    private static void Init()
    {
        //Debug.Log("Run editor window Init");
        ReplaceDefaultButtons window = (ReplaceDefaultButtons)GetWindow(typeof(ReplaceDefaultButtons));
        window.Show();
    }

    private void OnGUI()
    {
        //Debug.Log("Run editor window OnGUI");
        bool replace = GUILayout.Button("Replace Buttons", "button");
        bool validate = GUILayout.Button("Validate Buttons", "button");

        if (replace)
        {
            ReplaceButtons();
        }

        if (validate)
        {
            ValidateButtons();
        }
    }
    #endregion

    //Replace all non-ButtonPlus buttons with their ButtonPlus counterpart. Does not work on Prefabs.
    private void ReplaceButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToArray();
        buttons = buttons.Where(x => x.GetComponent<ButtonPlus>() == null).ToArray();

        for (int i = 0; i < buttons.Length; i++)
        {
            var tmpGO = new GameObject("temp");
            var inst = tmpGO.AddComponent<ButtonPlus>();
            MonoScript btnPlus = MonoScript.FromMonoBehaviour(inst);
            DestroyImmediate(tmpGO);

            SerializedObject so = new SerializedObject(buttons[i]);
            SerializedProperty scriptProperty = so.FindProperty("m_Script");
            so.Update();
            scriptProperty.objectReferenceValue = btnPlus;
            so.ApplyModifiedProperties();
            so.UpdateIfRequiredOrScript();
        }

        Debug.Log("Replaced " + buttons.Length + " buttons with ButtonPlus.");
    }

    //Validates all ButtonPlus buttons by checking if they're interactable, raycastable, raytargetted and not blocked by higher layer canvasses.
    private void ValidateButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToArray();
        buttons = buttons.Where(x => x.GetComponent<ButtonPlus>() != null && !x.GetComponent<ButtonPlus>().ignoreValidation).ToArray();
        Canvas[] canvasses = FindObjectsByType<Canvas>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToArray();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].interactable)
            {
                Debug.LogError(buttons[i].name + " has 'interactable' disabled.\n" +
                    "If this is intended, you can ignore this message. Otherwise, enable it to make the button clickable.");
            }

            Graphic[] raycastTargets = buttons[i].GetComponentsInChildren<Graphic>();
            bool raycastable = false;

            for (int j = 0; j < raycastTargets.Length; j++)
            {
                if (raycastTargets[j].raycastTarget)
                {
                    raycastable = true;
                    break;
                }
            }

            if (!raycastable)
            {
                Debug.LogError(buttons[i].name + " has 'raycastTarget' disabled on all of its and its childrens graphic components.\n" +
                    "If this is intended, you can ignore this message. Otherwise, enable atleast one of them to make the button clickable.");
            }

            GraphicRaycaster grc = buttons[i].GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();

            if (grc == null || !grc.enabled)
            {
                Debug.LogError(buttons[i].name + "'s parent Canvas is missing the 'Graphic Raycaster' component or has it disabled.\n" +
                    "If this is intended, you can ignore this message. Otherwise, enable or add it to the canvas to make the button clickable.");
            }

            for (int j = 0; j < canvasses.Length; j++)
            {
                if (canvasses[j].sortingOrder > buttons[i].GetComponentInParent<Canvas>().sortingOrder)
                {
                    GraphicRaycaster Cgrc = canvasses[j].GetComponent<GraphicRaycaster>();
                    if (Cgrc != null && Cgrc.enabled)
                    {
                        Debug.LogError(buttons[i].name + " is obscured by canvas " + canvasses[j].name + " with a higher priority and a 'Graphic Raycaster' component.\n" +
                            "If this is intended, you can ignore this message. Otherwise, remove the 'Graphic Raycaster' component from the other canvas, or lower its priority to make the button clickable.");
                    }
                }
            }
        }
    }
}
#endif