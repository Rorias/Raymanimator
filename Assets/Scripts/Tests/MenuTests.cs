using System.Collections;
using System.Linq;

using NUnit.Framework;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.Events;
using System;

public class MenuTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator MainMenuButtons()
    {
        yield return TestUtility.LoadSelectedGameScene(0);
        IsButtonButtonPlus();
        ButtonHasValidEvents();
        yield return null;
    }

    [UnityTest]
    public IEnumerator MainMenuDropdowns()
    {
        yield return TestUtility.LoadSelectedGameScene(0);
        DropdownHasValidEvents();
        yield return null;
    }

    [UnityTest]
    public IEnumerator MainMenuToggles()
    {
        yield return TestUtility.LoadSelectedGameScene(0);
        ToggleHasValidEvents();
        yield return null;
    }

    [UnityTest]
    public IEnumerator MainMenuInputFields()
    {
        yield return TestUtility.LoadSelectedGameScene(0);
        InputFieldHasValidEvents();
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditorButtons()
    {
        yield return TestUtility.LoadSelectedGameScene(1);
        IsButtonButtonPlus();
        ButtonHasValidEvents();
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditorDropdowns()
    {
        yield return TestUtility.LoadSelectedGameScene(1);
        DropdownHasValidEvents();
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditorToggles()
    {
        yield return TestUtility.LoadSelectedGameScene(1);
        ToggleHasValidEvents();
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditorInputFields()
    {
        yield return TestUtility.LoadSelectedGameScene(1);
        InputFieldHasValidEvents();
        yield return null;
    }

    private void IsButtonButtonPlus()
    {
        Button[] buttons = FindObjectsOfType<Button>(true);

        Button x = buttons.FirstOrDefault(x => x.GetComponent<ButtonPlus>() == null);
        Button[] b = buttons.Where(x => x.GetComponent<ButtonPlus>() == null).ToArray();
        Assert.Null(x, x != null ? x.name + " on " + x.transform.parent.name + (b != null && b.Length - 1 > 0 ? " and " + (b.Length - 1) + " others " : "") + " should be converted to ButtonPlus." : "");
    }

    private void ButtonHasValidEvents()
    {
        ButtonPlus[] buttons = FindObjectsOfType<ButtonPlus>(true);

        if (buttons.Length <= 0)
        {
            Assert.Inconclusive("Buttons aren't found in the scene.");
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int eventCount = buttons[i].onClick.GetPersistentEventCount();

            for (int pEvent = 0; pEvent < eventCount; pEvent++)
            {
                UnityEngine.Object target = buttons[i].onClick.GetPersistentTarget(pEvent);
                string methodName = buttons[i].onClick.GetPersistentMethodName(pEvent);

                Assert.NotNull(target, buttons[i].name + " in " + buttons[i].transform.parent.name + " has no script assigned to one of its events.");
                Assert.NotNull(target.GetType().GetMethod(methodName), buttons[i].name + " in " + buttons[i].transform.parent.name + " has a missing method in one of its events.");
            }
        }
    }

    private void DropdownHasValidEvents()
    {
        TMP_DropdownPlus[] dropdowns = FindObjectsOfType<TMP_DropdownPlus>(true);

        if (dropdowns.Length <= 0)
        {
            Assert.Inconclusive("Dropdowns aren't found in the scene.");
        }

        for (int i = 0; i < dropdowns.Length; i++)
        {
            int eventCount = dropdowns[i].onValueChanged.GetPersistentEventCount();

            for (int pEvent = 0; pEvent < eventCount; pEvent++)
            {
                UnityEngine.Object target = dropdowns[i].onValueChanged.GetPersistentTarget(pEvent);
                string methodName = dropdowns[i].onValueChanged.GetPersistentMethodName(pEvent);

                Assert.NotNull(target, dropdowns[i].name + " in " + dropdowns[i].transform.parent.name + " has no script assigned to one of its events.");
                Assert.NotNull(target.GetType().GetMethod(methodName), dropdowns[i].name + " in " + dropdowns[i].transform.parent.name + " has a missing method in one of its events.");
            }
        }
    }

    private void ToggleHasValidEvents()
    {
        Toggle[] toggles = FindObjectsOfType<Toggle>(true);

        if (toggles.Length <= 0)
        {
            Assert.Inconclusive("Toggles aren't found in the scene.");
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            int eventCount = toggles[i].onValueChanged.GetPersistentEventCount();

            for (int pEvent = 0; pEvent < eventCount; pEvent++)
            {
                UnityEngine.Object target = toggles[i].onValueChanged.GetPersistentTarget(pEvent);
                string methodName = toggles[i].onValueChanged.GetPersistentMethodName(pEvent);

                Assert.NotNull(target, toggles[i].name + " in " + toggles[i].transform.parent.name + " has no script assigned to one of its events.");
                Assert.NotNull(target.GetType().GetMethod(methodName), toggles[i].name + " in " + toggles[i].transform.parent.name + " has a missing method in one of its events.");
            }
        }
    }

    private void InputFieldHasValidEvents()
    {
        TMP_InputField[] inputFields = FindObjectsOfType<TMP_InputField>(true);

        if (inputFields.Length <= 0)
        {
            Assert.Inconclusive("InputFields aren't found in the scene.");
        }

        for (int i = 0; i < inputFields.Length; i++)
        {
            LoopEvents(inputFields[i], inputFields[i].onValueChanged);
            LoopEvents(inputFields[i], inputFields[i].onEndEdit);
            LoopEvents(inputFields[i], inputFields[i].onSelect);
            LoopEvents(inputFields[i], inputFields[i].onDeselect);
        }
    }

    private void LoopEvents(TMP_InputField _object, UnityEvent<string> _event)
    {
        int eventCount = _event.GetPersistentEventCount();

        for (int pEvent = 0; pEvent < eventCount; pEvent++)
        {
            UnityEngine.Object target = _event.GetPersistentTarget(pEvent);
            string methodName = _event.GetPersistentMethodName(pEvent);

            Assert.NotNull(target, _object.name + " in " + _object.transform.parent.name + " has no script assigned to one of its events.");
            Assert.NotNull(target.GetType().GetMethod(methodName), _object.name + " in " + _object.transform.parent.name + " has a missing method in one of its events.");
        }
    }

    [UnityTest]
    public IEnumerator MainMenuMenus()
    {
        yield return TestUtility.LoadSelectedGameScene(0);
        IsMenuItemSet();
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditorMenus()
    {
        yield return TestUtility.LoadSelectedGameScene(1);
        IsMenuItemSet();
        yield return null;
    }

    private void IsMenuItemSet()
    {
        Menus[] menus = FindObjectsOfType<Menus>(true);

        if (menus.Length <= 0)
        {
            Assert.Inconclusive("Menus aren't found in the scene.");
        }

        for (int i = 0; i < menus.Length; i++)
        {
            ButtonPlus[] menuButtons = menus[i].GetComponentsInChildren<ButtonPlus>();

            for (int button = 0; button < menuButtons.Length; button++)
            {
                int eventCount = menuButtons[button].onClick.GetPersistentEventCount();

                Assert.Greater(eventCount, 0, "MenuButton " + menuButtons[button].name + " doesn't have persistent events applied.");

                bool activatesMenu = false;
                bool targetsParent = false;

                for (int pEvent = 0; pEvent < eventCount; pEvent++)
                {
                    UnityEngine.Object target = menuButtons[button].onClick.GetPersistentTarget(pEvent);
                    string methodName = menuButtons[button].onClick.GetPersistentMethodName(pEvent);

                    if (target == menus[i])
                    {
                        targetsParent = true;
                    }

                    if (methodName.Contains("ActivateNextMenu"))
                    {
                        activatesMenu = true;

                        //Only invoke call for the next menu if it's the only thing the button does
                        if (eventCount == 1)
                        {
                            try
                            {
                                menuButtons[button].onClick.Invoke();
                                menus[i].Back();
                            }
                            catch
                            {
                                Assert.Fail(methodName + " on " + menuButtons[button].name + " doesn't have a MenuItem assigned to it.");
                            }
                        }
                    }
                }

                Assert.True(activatesMenu, "MenuButton " + menuButtons[button].name + " doesn't activate any menu.");
                Assert.True(targetsParent, "MenuButton " + menuButtons[button].name + " doesn't target parent Menu " + menus[i].name + ".");
            }
        }
    }
}
