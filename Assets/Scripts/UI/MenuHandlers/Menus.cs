using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Menus : MonoBehaviour
{
    public MenuItem startMenu;

    private List<MenuItem> menus = new List<MenuItem>();
    [HideInInspector] public MenuItem currentMenu;

    private void Start()
    {
        menus = FindObjectsByType<MenuItem>(FindObjectsSortMode.None).ToList();
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

    public void ActivateNextMenu(MenuItem _nextMenu)
    {
        if (currentMenu == _nextMenu && !_nextMenu.gameObject.activeSelf && _nextMenu.previousItem == _nextMenu)
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

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
