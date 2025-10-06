using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class SubMenus : MonoBehaviour
{
    public SubMenuItem defaultSubMenu;
    public List<SubMenuItem> subMenus = new List<SubMenuItem>();

    private List<RaycastResult> rayResults = new List<RaycastResult>();
    private SubMenuItem currentSubMenu;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
        ResetMenu();
    }

    private void Update()
    {
        if (inputManager.GetKeyDown(InputManager.InputKey.Select))
        {
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current)
            { position = Input.mousePosition, pointerId = -1 }, rayResults);
            //If something was clicked, keep the menu open
            if (rayResults.Count > 0)
            {
                return;
            }

            currentSubMenu.gameObject.SetActive(false);
        }
    }

    private void ResetMenu()
    {
        for (int i = 0; i < subMenus.Count; i++)
        {
            subMenus[i].gameObject.SetActive(false);
        }

        if (defaultSubMenu == null) { defaultSubMenu = subMenus[0]; }

        currentSubMenu = defaultSubMenu;
        currentSubMenu.previousItem = currentSubMenu;
    }

    public void ActivateNextMenu(SubMenuItem _nextMenu)
    {
        if (currentSubMenu == _nextMenu && !_nextMenu.gameObject.activeSelf && _nextMenu.previousItem == _nextMenu)
        {
            currentSubMenu.gameObject.SetActive(true);
            return;
        }

        _nextMenu.previousItem = currentSubMenu;
        currentSubMenu.gameObject.SetActive(false);

        if (currentSubMenu != _nextMenu)
        {
            currentSubMenu = _nextMenu;
            currentSubMenu.gameObject.SetActive(true);
        }
    }

    public void ActivateOnHover(SubMenuItem _thisMenu)
    {
        if (currentSubMenu != _thisMenu && !_thisMenu.gameObject.activeInHierarchy && currentSubMenu.gameObject.activeInHierarchy)
        {
            currentSubMenu.gameObject.SetActive(false);
            currentSubMenu = _thisMenu;
            currentSubMenu.gameObject.SetActive(true);
            //Hardcoded just for Raymanimator
            currentSubMenu.caller?.Select();
        }
    }

    public void Back()
    {
        currentSubMenu.gameObject.SetActive(false);
        currentSubMenu = currentSubMenu.previousItem;
        currentSubMenu.gameObject.SetActive(true);
    }
}