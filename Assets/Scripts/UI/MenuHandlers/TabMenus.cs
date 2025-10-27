using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class TabMenus : MonoBehaviour
{
    public TabMenuItem startMenu;

    public List<TabMenuItem> menus = new List<TabMenuItem>();
    [HideInInspector] public TabMenuItem currentMenu;

    private void Start()
    {
        if (menus.Count < 1)
        {
            menus = FindObjectsByType<TabMenuItem>(FindObjectsSortMode.None).ToList();
        }
        ResetMenu();
    }

    private void ResetMenu()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].gameObject.SetActive(false);
        }

        if (startMenu == null) { startMenu = menus[0]; }

        currentMenu = startMenu;
        startMenu.gameObject.SetActive(true);
    }

    public void ActivateNextMenu(TabMenuItem _nextMenu)
    {
        if (currentMenu == _nextMenu)
        {
            currentMenu.gameObject.SetActive(true);
            return;
        }

        if (currentMenu != null)
        {
            _nextMenu.previousItem = currentMenu;
            currentMenu.gameObject.SetActive(false);
        }

        if (currentMenu != _nextMenu)
        {
            currentMenu = _nextMenu;
            currentMenu.gameObject.SetActive(true);
        }
    }

    public void Back()
    {
        currentMenu.gameObject.SetActive(false);
        currentMenu = currentMenu.previousItem;
        currentMenu.gameObject.SetActive(true);
    }
}
