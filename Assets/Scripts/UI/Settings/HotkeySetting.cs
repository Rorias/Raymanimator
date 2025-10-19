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
    public ButtonPlus openButton;
    public GameObject hotkeySettingsPanel;
    public GameObject animSettingsPanel;
    [Space]
    public TMP_FontAsset redFont;
    public TMP_FontAsset yellowFont;
    public TMP_FontAsset greenFont;

    public GameObject prefabHotkeyGroup;

    public Transform keyHolder;

    private List<HotkeyGroup> hotkeys = new List<HotkeyGroup>();
    private KeyCode[] notAllowed = new KeyCode[] { KeyCode.Return, KeyCode.Backspace, KeyCode.LeftWindows, KeyCode.RightWindows, KeyCode.LeftApple, KeyCode.RightApple, KeyCode.Pause };

    private GameManager gameManager;
    private InputManager input;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameManager.Instance;
        input = InputManager.Instance;
        InitializeHotkeyUI();
        CloseHotkeyWindow();
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
            spaced = Regex.Replace(hotkey.Value[0].code.ToString(), "[A-Z0-9]", " $0").Trim().ToLower();
            hotkeyGroup.key1NameText.font = spaced == "none" ? yellowFont : greenFont;
            hotkeyGroup.key1NameText.text = spaced;
            //set key name 2 or default
            spaced = Regex.Replace(hotkey.Value[1].code.ToString(), "[A-Z0-9]", " $0").Trim().ToLower();
            hotkeyGroup.key2NameText.font = spaced == "none" ? yellowFont : greenFont;
            hotkeyGroup.key2NameText.text = spaced;

            hotkeyGroup.key1Btn.onClick.AddListener(() => SetNew1Key(hotkey.Key));
            hotkeyGroup.key2Btn.onClick.AddListener(() => SetNew2Key(hotkey.Key));
            hotkeyGroup.resetBtn.onClick.AddListener(() => ResetKeys(hotkey.Key));

            hotkeys.Add(hotkeyGroup);
        }

        CheckKeyConflicts();
    }

    private void SetNew1Key(InputManager.InputKey _key)
    {
        int index = hotkeys.FindIndex(x => x.hotkeyName == _key.ToString());
        hotkeys.First(x => x.hotkeyName == _key.ToString()).key1NameText.font = yellowFont;
        for (int i = 0; i < hotkeys.Count; i++)
        {
            if (hotkeys[i] != hotkeys[index])
            {
                hotkeys[i].key1Btn.interactable = false;
            }
            hotkeys[i].key2Btn.interactable = false;
        }
        StartCoroutine(SetKey(_key, 0, hotkeys[index].key1NameText));
    }

    private void SetNew2Key(InputManager.InputKey _key)
    {
        int index = hotkeys.FindIndex(x => x.hotkeyName == _key.ToString());
        hotkeys.First(x => x.hotkeyName == _key.ToString()).key2NameText.font = yellowFont;
        for (int i = 0; i < hotkeys.Count; i++)
        {
            hotkeys[i].key1Btn.interactable = false;
            if (hotkeys[i] != hotkeys[index])
            {
                hotkeys[i].key2Btn.interactable = false;
            }
        }
        StartCoroutine(SetKey(_key, 1, hotkeys[index].key2NameText));
    }

    private IEnumerator SetKey(InputManager.InputKey _key, int _inputIndex, TMP_Text _text)
    {
        //to ensure that the select key doesnt apply itself to itself
        yield return new WaitForSecondsRealtime(0.04f);
        InputManager.Key entry = input.Inputs[_key].Where(x => x.type == InputManager.KeyType.Keyboard).ToArray()[_inputIndex];
        bool keyNotSet = true;

        while (keyNotSet)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    if (notAllowed.Contains(kcode) || kcode.ToString().Contains("Joystick")) { continue; }

                    if (kcode == KeyCode.Escape)
                    {
                        entry.code = KeyCode.None;
                        _text.text = "none";
                        CheckKeyConflicts();
                        gameManager.SaveInputs();
                        keyNotSet = false;
                        break;
                    }

                    entry.code = kcode;
                    string spaced = Regex.Replace(kcode.ToString(), "[A-Z0-9]", " $0").Trim().ToLower();
                    _text.text = spaced;
                    CheckKeyConflicts();
                    gameManager.SaveInputs();
                    keyNotSet = false;
                    break;
                }
            }
            yield return null;
        }

        for (int i = 0; i < hotkeys.Count; i++)
        {
            hotkeys[i].key1Btn.interactable = true;
            hotkeys[i].key2Btn.interactable = true;
        }
    }

    private void CheckKeyConflicts()
    {
        //set all bound keys green
        for (int outer = 0; outer < hotkeys.Count; outer++)
        {
            hotkeys[outer].key1NameText.font = hotkeys[outer].key1NameText.text == "none" ? yellowFont : greenFont;
            hotkeys[outer].key2NameText.font = hotkeys[outer].key2NameText.text == "none" ? yellowFont : greenFont;
        }

        //set all conflicting keys red
        for (int outer = 0; outer < hotkeys.Count; outer++)
        {
            for (int conflict = outer; conflict < hotkeys.Count; conflict++)
            {
                //Dont compare the key to itself
                if (hotkeys[outer] == hotkeys[conflict])
                {
                    continue;
                }

                //If the key is bound
                if (hotkeys[outer].key1NameText.text != "none")
                {
                    if (hotkeys[outer].key1NameText.text == hotkeys[conflict].key1NameText.text)
                    {
                        hotkeys[outer].key1NameText.font = redFont;
                        hotkeys[conflict].key1NameText.font = redFont;
                    }
                }

                //If the key is bound
                if (hotkeys[outer].key2NameText.text != "none")
                {
                    if (hotkeys[outer].key2NameText.text == hotkeys[conflict].key2NameText.text)
                    {
                        hotkeys[outer].key2NameText.font = redFont;
                        hotkeys[conflict].key2NameText.font = redFont;
                    }
                }

                if (hotkeys[outer].key1NameText.text == hotkeys[conflict].key2NameText.text)
                {
                    hotkeys[outer].key1NameText.font = redFont;
                    hotkeys[conflict].key2NameText.font = redFont;
                }

                if (hotkeys[outer].key2NameText.text == hotkeys[conflict].key1NameText.text)
                {
                    hotkeys[outer].key2NameText.font = redFont;
                    hotkeys[conflict].key1NameText.font = redFont;
                }
            }
        }
    }

    private void ResetKeys(InputManager.InputKey _key)
    {
        InputManager.Key[] keys = input.Inputs[_key].Where(x => x.type == InputManager.KeyType.Keyboard).ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].code = input.DefaultKeys[_key][i];
        }

        hotkeys.First(x => x.hotkeyName == _key.ToString()).key1NameText.text = Regex.Replace(keys[0].code.ToString(), "[A-Z0-9]", " $0").Trim().ToLower(); ;
        hotkeys.First(x => x.hotkeyName == _key.ToString()).key2NameText.text = Regex.Replace(keys[1].code.ToString(), "[A-Z0-9]", " $0").Trim().ToLower(); ;

        CheckKeyConflicts();
        gameManager.SaveInputs();
    }

    public void OpenHotkeyWindow()
    {
        animSettingsPanel.SetActive(false);
        hotkeySettingsPanel.SetActive(true);
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(() => { CloseHotkeyWindow(); });
    }

    public void CloseHotkeyWindow()
    {
        animSettingsPanel.SetActive(true);
        hotkeySettingsPanel.SetActive(false);
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(() => { OpenHotkeyWindow(); });
    }
}
