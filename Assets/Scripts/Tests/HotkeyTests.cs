using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.Events;

public class HotkeyTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator TestEditorHotkeys()
    {
        yield return TestUtility.LoadSelectedGameScene(0);
        #region Load first available animation
        Menus menus = FindObjectOfType<Menus>();
        MenuItem[] menuItems = FindObjectsOfType<MenuItem>(true);
        MenuItem menu = null;
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (menuItems[i].gameObject.name == "StartMenu")
            {
                menu = menuItems[i];
                break;
            }
        }
        menus.ActivateNextMenu(menu);
        GameObject.Find("EditButton").GetComponent<ButtonPlus>().onClick.Invoke();
        GameObject.Find("AnimationDropdown").GetComponent<TMP_DropdownPlus>().value = 0;
        GameObject.Find("LoadButton").GetComponent<ButtonPlus>().onClick.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        #endregion

        AnimatorController ac = FindObjectOfType<AnimatorController>();
        List<GamePart> currentGameParts = ac.GetCurrentGameParts();
        List<Part> currentParts = ac.GetCurrentParts();
        bool gameset = currentGameParts[0].sr.sprite != null;
        bool set = currentParts[0].part != null;
        ac.DeletePart();

        Assert.True(set == (currentParts[0].part == null) && gameset == (currentGameParts[0].sr.sprite == null), "Part and visual were not properly deleted.");
        yield return null;
    }
}
