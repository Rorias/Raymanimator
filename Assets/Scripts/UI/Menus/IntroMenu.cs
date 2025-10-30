using UnityEngine;

public class IntroMenu : Raymanimator
{
    public Menus menus;
    public MenuItem introMenu;
    public MenuItem initMenu;
    public MenuItem startMenu;
    [Space]
    public Animator anim;

    private GameSettings settings;
    private InputManager input;

    private void Awake()
    {
        settings = GameSettings.Instance;
        input = InputManager.Instance;
    }

    private void Update()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("lol") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f) ||
            ((input.GetKeyDown(InputManager.InputKey.Confirm) || input.GetKeyDown(InputManager.InputKey.Select)) && menus.currentMenu == introMenu))
        {
            if (settings.lastSpriteset == "" || string.IsNullOrWhiteSpace(settings.animationsPath) || settings.firstLoad)
            {
                menus.ActivateNextMenu(initMenu);
            }
            else
            {
                menus.ActivateNextMenu(startMenu);
            }
        }
    }
}
