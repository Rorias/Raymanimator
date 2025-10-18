using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class HotkeySetting : Settings
{
    public TMP_FontAsset redFont;
    public TMP_FontAsset yellowFont;
    public TMP_FontAsset greenFont;

    public GameObject prefabHotkeyGroup;

    public Transform keyHolder;

    private List<HotkeyGroup> hotkeys = new List<HotkeyGroup>();
    private KeyCode[] notAllowed = new KeyCode[] { KeyCode.Escape, KeyCode.Return, KeyCode.Backspace, KeyCode.LeftWindows, KeyCode.RightWindows, KeyCode.LeftApple, KeyCode.RightApple, KeyCode.Pause };

    private GameManager gameManager;
    private InputManager input;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameManager.Instance;
        input = InputManager.Instance;
        InitializeHotkeyUI();
    }

    private void InitializeHotkeyUI()
    {
        //For every default input key's set of keycodes
        var loopKBKeys = input.Inputs.Where(x => x.Value.All(x => x.type != InputManager.KeyType.Meta) && x.Value.Any(x => x.type == InputManager.KeyType.Keyboard));
        foreach (KeyValuePair<InputManager.InputKey, List<InputManager.Key>> hotkey in loopKBKeys)
        {
            //Create a new hotkey group
            GameObject hotkeyGroupObject = Instantiate(prefabHotkeyGroup, keyHolder);
            HotkeyGroup hotkeyGroup = hotkeyGroupObject.GetComponent<HotkeyGroup>();
            hotkeyGroup.Initialize();
            //set the hotkey name
            string spaced = Regex.Replace(hotkey.Key.ToString(), "[A-Z0-9]", " $0").Trim().ToLower();
            hotkeyGroup.hotkeyName = hotkey.Key.ToString();
            hotkeyGroup.hotkeyNameText.text = spaced;
            //set key name 1
            if (hotkey.Value.Count > 0)
            {
                spaced = Regex.Replace(hotkey.Value[0].code.ToString(), "[A-Z0-9]", " $0").Trim().ToLower();
                hotkeyGroup.key1NameText.text = spaced;
            }
            else
            {
                hotkeyGroup.key1NameText.font = yellowFont;
                hotkeyGroup.key1NameText.text = "none";
            }
            //set key name 2 or default
            if (hotkey.Value.Count > 1)
            {
                spaced = Regex.Replace(hotkey.Value[1].code.ToString(), "[A-Z0-9]", " $0").Trim().ToLower();
                hotkeyGroup.key2NameText.text = spaced;
            }
            else
            {
                hotkeyGroup.key2NameText.font = yellowFont;
                hotkeyGroup.key2NameText.text = "none";
            }

            hotkeyGroup.key1Btn.onClick.AddListener(() => SetNewKey(hotkey.Key));
            hotkeyGroup.key2Btn.onClick.AddListener(() => SetNewKey(hotkey.Key));

            hotkeys.Add(hotkeyGroup);
        }
    }

    private void SetNewKey(InputManager.InputKey _key)
    {
        int index = hotkeys.FindIndex(x => x.hotkeyName == _key.ToString());
        hotkeys.First(x => x.hotkeyName == _key.ToString()).key1NameText.font = yellowFont;
        StartCoroutine(SetKey(_key, index));
    }

    private IEnumerator SetKey(InputManager.InputKey _key, int _keyIndex)
    {
        //to ensure that the select key doesnt apply itself to itself
        yield return new WaitForSecondsRealtime(0.04f);
        InputManager.Key entry = input.Inputs[_key].First(x => x.type == InputManager.KeyType.Keyboard);
        bool keyNotSet = true;

        while (keyNotSet)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    if (notAllowed.Contains(kcode) || kcode.ToString().Contains("Joystick")) { continue; }

                    entry.code = kcode;
                    string spaced = Regex.Replace(kcode.ToString(), "[A-Z0-9]", " $0").Trim().ToLower();
                    hotkeys[_keyIndex].key1NameText.text = spaced;
                    CheckKeyConflicts();
                    gameManager.SaveInputs();
                    keyNotSet = false;
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private void CheckKeyConflicts()
    {
        //set all keys green
        for (int outer = 0; outer < hotkeys.Count; outer++)
        {
            hotkeys[outer].key1NameText.font = greenFont;
        }

        //set all conflicting keys red
        for (int outer = 0; outer < hotkeys.Count; outer++)
        {
            List<HotkeyGroup> conflictingKeys = hotkeys.FindAll(x => x.key1NameText.text == hotkeys[outer].key1NameText.text && x != hotkeys[outer]);

            for (int i = 0; i < conflictingKeys.Count; i++)
            {
                conflictingKeys[i].key1NameText.font = redFont;
            }
        }
    }
}

