using System.Collections.Generic;

using UnityEngine;

public class SubMenus : MonoBehaviour
{
    public SubMenuItem defaultSubMenu;
    public List<SubMenuItem> subMenus = new List<SubMenuItem>();

    private SubMenuItem currentSubMenu;

    private void Start()
    {
        ResetMenu();
    }

    private void ResetMenu()
    {
        for (int i = 0; i < subMenus.Count; i++)
        {
            subMenus[i].gameObject.SetActive(false);
        }

        if (defaultSubMenu == null) { defaultSubMenu = subMenus[0]; }

        currentSubMenu = defaultSubMenu;
        defaultSubMenu.gameObject.SetActive(true);
    }

    public void ActivateNextMenu(SubMenuItem _nextMenu)
    {
        _nextMenu.previousItem = currentSubMenu;
        currentSubMenu.gameObject.SetActive(false);
        currentSubMenu = _nextMenu;
        currentSubMenu.gameObject.SetActive(true);
    }

    public void Back()
    {
        currentSubMenu.gameObject.SetActive(false);
        currentSubMenu = currentSubMenu.previousItem;
        currentSubMenu.gameObject.SetActive(true);
    }
}