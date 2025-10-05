using System.Collections.Generic;

using UnityEngine;

public sealed class InputManager
{
    #region Singleton
    private static InputManager instance = null;
    private static readonly object padlock = new object();

    private InputManager() { }

    public static InputManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }
    }
    #endregion

    public enum InputKey
    {
        None,
        SpritePrevious,
        SpriteNext,
        DeletePart,
        FramePrevious,
        FrameNext,
        MoveSpriteLeft,
        MoveSpriteUp,
        MoveSpriteRight,
        MoveSpriteDown,
        ZoomCamera,
        DragCamera,
        MoveCameraLeft,
        MoveCameraUp,
        MoveCameraRight,
        MoveCameraDown,
        LeftMenu,
        UpMenu,
        RightMenu,
        DownMenu,
        MultiSelect,
        Select,
        Confirm,
        Return,
    };

    public enum KeyType { None, Keyboard, Controller, Meta, }

    public enum InputAxis
    {
        None,
        XaxisLeft,
        XaxisRight,
        YaxisUp,
        YaxisDown,
    };

    public enum AxisState { None, Down, Held, Up, }

    public enum PossibleJoystick { Left, Right, }

    public class Key
    {
        public KeyCode code { get; set; }
        public KeyType type { get; set; }
    }

    public class Axis
    {
        public string name { get; set; }
        public bool positive { get; set; }
        public AxisState state { get; set; }
    }

    public Dictionary<InputKey, List<Key>> Inputs = new Dictionary<InputKey, List<Key>>()
    {
        { InputKey.None, new List<Key> { new Key() { code = KeyCode.None, type = KeyType.None } } },
        { InputKey.SpritePrevious, new List<Key> { new Key() { code = KeyCode.Comma, type = KeyType.Keyboard } } },
        { InputKey.SpriteNext, new List<Key> { new Key() { code = KeyCode.Period, type = KeyType.Keyboard } } },
        { InputKey.DeletePart, new List<Key>{ new Key() { code=KeyCode.Delete, type = KeyType.Keyboard } } },
        { InputKey.FramePrevious, new List<Key> { new Key() { code = KeyCode.Minus, type = KeyType.Keyboard } } },
        { InputKey.FrameNext, new List<Key> { new Key() { code = KeyCode.Plus, type = KeyType.Keyboard } } },
        { InputKey.MoveSpriteLeft, new List<Key> {
            new Key() { code = KeyCode.LeftArrow, type = KeyType.Keyboard },
            new Key() { code = KeyCode.JoystickButton0, type = KeyType.Controller } }
        },
        { InputKey.MoveSpriteUp, new List<Key> {
            new Key() { code = KeyCode.UpArrow, type = KeyType.Keyboard },
            new Key() { code = KeyCode.JoystickButton2, type = KeyType.Controller } }
        },
        { InputKey.MoveSpriteRight, new List<Key> {
            new Key() { code = KeyCode.RightArrow, type = KeyType.Keyboard },
            new Key() { code = KeyCode.JoystickButton1, type = KeyType.Controller } }
        },
        { InputKey.MoveSpriteDown, new List<Key> {
            new Key() { code = KeyCode.DownArrow, type = KeyType.Keyboard },
            new Key() { code = KeyCode.JoystickButton4, type = KeyType.Controller } }
        },
        { InputKey.LeftMenu, new List<Key> { new Key() { code = KeyCode.LeftArrow, type = KeyType.Meta } } },
        { InputKey.UpMenu, new List<Key> { new Key() { code = KeyCode.UpArrow, type = KeyType.Meta } } },
        { InputKey.RightMenu, new List<Key> { new Key() { code = KeyCode.RightArrow, type = KeyType.Meta } } },
        { InputKey.DownMenu, new List<Key> { new Key() { code = KeyCode.DownArrow, type = KeyType.Meta } } },
        { InputKey.DragCamera, new List<Key> { new Key() { code = KeyCode.Mouse2, type = KeyType.Keyboard } } },
        { InputKey.MoveCameraLeft, new List<Key> { new Key() { code = KeyCode.A, type = KeyType.Keyboard } } },
        { InputKey.MoveCameraUp, new List<Key> { new Key() { code = KeyCode.W, type = KeyType.Keyboard } } },
        { InputKey.MoveCameraRight, new List<Key> { new Key() { code = KeyCode.D, type = KeyType.Keyboard } } },
        { InputKey.MoveCameraDown, new List<Key> { new Key() { code = KeyCode.S, type = KeyType.Keyboard } } },
        { InputKey.MultiSelect, new List<Key> {
            new Key() { code = KeyCode.LeftShift, type = KeyType.Keyboard },
            new Key() { code = KeyCode.RightShift, type = KeyType.Keyboard } }
        },
        {InputKey.Select, new List<Key>{ new Key { code = KeyCode.Mouse0, type = KeyType.Keyboard } } },
        { InputKey.Confirm, new List<Key> {
            new Key() { code = KeyCode.Return, type = KeyType.Meta },
            new Key() { code = KeyCode.JoystickButton7, type = KeyType.Meta } }
        },
        { InputKey.Return, new List<Key> {
            new Key() { code = KeyCode.Escape, type = KeyType.Meta },
            new Key() { code = KeyCode.Backspace, type = KeyType.Meta },
            new Key() { code = KeyCode.JoystickButton6, type = KeyType.Meta } }
        },
    };

    public Dictionary<InputAxis, List<Axis>> Axiss = new Dictionary<InputAxis, List<Axis>>()
    {
        { InputAxis.XaxisLeft, new List<Axis> {
            new Axis() { state = AxisState.None, name = "JoystickLeftStickX", positive = false },
            new Axis() { state = AxisState.None, name = "JoystickRightStickX", positive = false },
            new Axis() { state = AxisState.None, name = "DpadX", positive = false }, }
        },
        { InputAxis.XaxisRight, new List<Axis> {
            new Axis() { state = AxisState.None, name = "JoystickLeftStickX", positive = true },
            new Axis() { state = AxisState.None, name = "JoystickRightStickX", positive = true },
            new Axis() { state = AxisState.None, name = "DpadX", positive = true }, }
        },
        { InputAxis.YaxisUp, new List<Axis> {
            new Axis() { state = AxisState.None, name = "JoystickLeftStickY", positive = true },
            new Axis() { state = AxisState.None, name = "JoystickRightStickY", positive = true },
            new Axis() { state = AxisState.None, name = "DpadY", positive = true }, }
        },
        { InputAxis.YaxisDown, new List<Axis> {
            new Axis() { state = AxisState.None, name = "JoystickLeftStickY", positive = false },
            new Axis() { state = AxisState.None, name = "JoystickRightStickY", positive = false },
            new Axis() { state = AxisState.None, name = "DpadY", positive = false }, }
        },
    };


    public readonly Dictionary<InputKey, KeyCode> DefaultKeys = new Dictionary<InputKey, KeyCode>()
    {
        { InputKey.SpritePrevious, KeyCode.Comma },
        { InputKey.SpriteNext, KeyCode.Period },
        { InputKey.DeletePart, KeyCode.Delete },
        { InputKey.FramePrevious, KeyCode.Minus },
        { InputKey.FrameNext, KeyCode.Plus },
        { InputKey.MoveSpriteLeft, KeyCode.LeftArrow },
        { InputKey.MoveSpriteRight, KeyCode.RightArrow },
        { InputKey.MoveSpriteUp, KeyCode.UpArrow },
        { InputKey.MoveSpriteDown, KeyCode.DownArrow },
        { InputKey.Select, KeyCode.Mouse0 },
        { InputKey.DragCamera, KeyCode.Mouse2 },
        { InputKey.MoveCameraLeft,  KeyCode.A },
        { InputKey.MoveCameraUp,  KeyCode.W},
        { InputKey.MoveCameraRight, KeyCode.D },
        { InputKey.MoveCameraDown,  KeyCode.S },
        { InputKey.MultiSelect, KeyCode.LeftShift }
    };

    public readonly Dictionary<InputKey, KeyCode> DefaultButtons = new Dictionary<InputKey, KeyCode>()
    {
        { InputKey.SpritePrevious , KeyCode.JoystickButton0 },
        { InputKey.SpriteNext, KeyCode.JoystickButton1 },
        { InputKey.FramePrevious, KeyCode.JoystickButton2 },
        { InputKey.FrameNext, KeyCode.JoystickButton4 },
    };

    public bool controllerConnected = false;
    public PossibleJoystick activeJoystick;
    public InputKey lastKey;

    public void UpdateAxis()
    {
        if (controllerConnected)
        {
            foreach (KeyValuePair<InputAxis, List<Axis>> entry in Axiss)
            {
                for (int i = 0; i < Axiss[entry.Key].Count; i++)
                {
                    if ((Input.GetAxis(Axiss[entry.Key][i].name) >= 0.5f && Axiss[entry.Key][i].positive) || (Input.GetAxis(Axiss[entry.Key][i].name) <= -0.5f && !Axiss[entry.Key][i].positive))
                    {
                        if (Axiss[entry.Key][i].state == AxisState.None)
                        {
                            Axiss[entry.Key][i].state = AxisState.Down;
                        }
                        else if (Axiss[entry.Key][i].state == AxisState.Down)
                        {
                            Axiss[entry.Key][i].state = AxisState.Held;
                        }
                    }

                    if ((Input.GetAxis(Axiss[entry.Key][i].name) < 0.5f && Axiss[entry.Key][i].positive) || (Input.GetAxis(Axiss[entry.Key][i].name) > -0.5f && !Axiss[entry.Key][i].positive))
                    {
                        if (Axiss[entry.Key][i].state == AxisState.Held)
                        {
                            Axiss[entry.Key][i].state = AxisState.Up;
                        }
                        else if (Axiss[entry.Key][i].state == AxisState.Up)
                        {
                            Axiss[entry.Key][i].state = AxisState.None;
                        }
                    }
                }
            }
        }
    }

    public bool GetAxisDown(InputAxis _axis)
    {
        if (controllerConnected)
        {
            for (int i = 0; i < Axiss[_axis].Count; i++)
            {
                if (Axiss[_axis][i].state == AxisState.Down)
                {
                    bool activeStick = activeJoystick == PossibleJoystick.Left ? !Axiss[_axis][i].name.Contains("JoystickRightStick") : !Axiss[_axis][i].name.Contains("JoystickLeftStick");

                    return activeStick;
                }
            }
        }

        return false;
    }

    public bool GetAxis(InputAxis _axis)
    {
        if (controllerConnected)
        {
            for (int i = 0; i < Axiss[_axis].Count; i++)
            {
                if (Axiss[_axis][i].state == AxisState.Held)
                {
                    bool activeStick = activeJoystick == PossibleJoystick.Left ? !Axiss[_axis][i].name.Contains("JoystickRightStick") : !Axiss[_axis][i].name.Contains("JoystickLeftStick");

                    return activeStick;
                }
            }
        }

        return false;
    }

    public bool GetAxisUp(InputAxis _axis)
    {
        if (controllerConnected)
        {
            for (int i = 0; i < Axiss[_axis].Count; i++)
            {
                if (Axiss[_axis][i].state == AxisState.Up)
                {
                    bool activeStick = activeJoystick == PossibleJoystick.Left ? !Axiss[_axis][i].name.Contains("JoystickRightStick") : !Axiss[_axis][i].name.Contains("JoystickLeftStick");

                    return activeStick;
                }
            }
        }

        return false;
    }

    public bool GetAxisReleased(InputAxis _axis)
    {
        if (controllerConnected)
        {
            for (int i = 0; i < Axiss[_axis].Count; i++)
            {
                if (Axiss[_axis][i].state == AxisState.None)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool GetKeyDown(InputKey _key)
    {
        for (int i = 0; i < Inputs[_key].Count; i++)
        {
            if (Input.GetKeyDown(Inputs[_key][i].code))
            {
                return true;
            }
        }

        return false;
    }

    public bool GetKey(InputKey _key)
    {
        for (int i = 0; i < Inputs[_key].Count; i++)
        {
            if (Input.GetKey(Inputs[_key][i].code))
            {
                return true;
            }
        }

        return false;
    }

    public bool GetKeyUp(InputKey _key)
    {
        for (int i = 0; i < Inputs[_key].Count; i++)
        {
            if (Input.GetKeyUp(Inputs[_key][i].code))
            {
                return true;
            }
        }

        return false;
    }

    public bool GetKeyTouched(InputKey _key)
    {
        for (int i = 0; i < Inputs[_key].Count; i++)
        {
            if (Input.GetKeyDown(Inputs[_key][i].code) || Input.GetKey(Inputs[_key][i].code) || Input.GetKeyUp(Inputs[_key][i].code))
            {
                return true;
            }
        }

        return false;
    }

    public void LoadInputs(GameSettings _settings)
    {
        Inputs[InputKey.SpritePrevious].Find(x => x.type == KeyType.Keyboard).code = _settings.spritePrevious;
        Inputs[InputKey.SpriteNext].Find(x => x.type == KeyType.Keyboard).code = _settings.spriteNext;
        Inputs[InputKey.DeletePart].Find(x => x.type == KeyType.Keyboard).code = _settings.deletePart;
        Inputs[InputKey.FramePrevious].Find(x => x.type == KeyType.Keyboard).code = _settings.framePrevious;
        Inputs[InputKey.FrameNext].Find(x => x.type == KeyType.Keyboard).code = _settings.frameNext;
        Inputs[InputKey.MoveSpriteLeft].Find(x => x.type == KeyType.Keyboard).code = _settings.moveSpriteLeft;
        Inputs[InputKey.MoveSpriteRight].Find(x => x.type == KeyType.Keyboard).code = _settings.moveSpriteRight;
        Inputs[InputKey.MoveSpriteUp].Find(x => x.type == KeyType.Keyboard).code = _settings.moveSpriteUp;
        Inputs[InputKey.MoveSpriteDown].Find(x => x.type == KeyType.Keyboard).code = _settings.moveSpriteDown;
        Inputs[InputKey.MultiSelect].Find(x => x.type == KeyType.Keyboard).code = _settings.multiSelect;

        //Inputs[InputKey.LeftMenu].Add(Inputs[InputKey.Left][0]);
        //Inputs[InputKey.UpMenu].Add(Inputs[InputKey.Up][0]);
        //Inputs[InputKey.RightMenu].Add(Inputs[InputKey.Right][0]);
        //Inputs[InputKey.DownMenu].Add(Inputs[InputKey.Down][0]);
        //Inputs[InputKey.Confirm].AddRange(Inputs[InputKey.Fist]);

        activeJoystick = _settings.activeJoystick;

        #region Test code to show that the key gets added as a reference and is thus updated
        /*Debug.Log(Inputs3[InputKey.LeftMenu][Inputs3[InputKey.LeftMenu].Count - 1].code + " REF");
          Debug.Log(Inputs3[InputKey.Left][0].code + " ORIGINAL");

          Key entry = Inputs3[InputKey.Left].First(x => x.type == KeyType.Keyboard);
          entry.code = KeyCode.LeftWindows;

          Debug.Log("CHANGED: ?");
          Debug.Log(Inputs3[InputKey.LeftMenu][Inputs3[InputKey.LeftMenu].Count - 1].code + " REF");
          Debug.Log(Inputs3[InputKey.Left][0].code + " ORIGINAL");
        */
        /*Debug.Log(Inputs3[InputKey.Confirm][Inputs3[InputKey.Confirm].Count - 1].code + " REF");
        Debug.Log(Inputs3[InputKey.Confirm][Inputs3[InputKey.Confirm].Count - 2].code + " REF");
        Debug.Log(Inputs3[InputKey.Fist][0].code + " ORIGINAL");
        Debug.Log(Inputs3[InputKey.Fist][1].code + " ORIGINAL");

        Key entry = Inputs3[InputKey.Fist].First(x => x.type == KeyType.Keyboard);
        entry.code = KeyCode.LeftWindows;

        Debug.Log("CHANGED: ?");
        Debug.Log(Inputs3[InputKey.Confirm][Inputs3[InputKey.Confirm].Count - 1].code + " REF");
        Debug.Log(Inputs3[InputKey.Confirm][Inputs3[InputKey.Confirm].Count - 2].code + " REF");
        Debug.Log(Inputs3[InputKey.Fist][0].code + " ORIGINAL");
        Debug.Log(Inputs3[InputKey.Fist][1].code + " ORIGINAL");
        */
        #endregion
    }

    public void SaveInputs(GameSettings _settings)
    {
        _settings.spritePrevious = Inputs[InputKey.SpritePrevious].Find(x => x.type == KeyType.Keyboard).code;
        _settings.spriteNext = Inputs[InputKey.SpriteNext].Find(x => x.type == KeyType.Keyboard).code;
        _settings.deletePart = Inputs[InputKey.DeletePart].Find(x => x.type == KeyType.Keyboard).code;
        _settings.framePrevious = Inputs[InputKey.FramePrevious].Find(x => x.type == KeyType.Keyboard).code;
        _settings.frameNext = Inputs[InputKey.FrameNext].Find(x => x.type == KeyType.Keyboard).code;
        _settings.moveSpriteLeft = Inputs[InputKey.MoveSpriteLeft].Find(x => x.type == KeyType.Keyboard).code;
        _settings.moveSpriteRight = Inputs[InputKey.MoveSpriteRight].Find(x => x.type == KeyType.Keyboard).code;
        _settings.moveSpriteUp = Inputs[InputKey.MoveSpriteUp].Find(x => x.type == KeyType.Keyboard).code;
        _settings.moveSpriteDown = Inputs[InputKey.MoveSpriteDown].Find(x => x.type == KeyType.Keyboard).code;
        _settings.multiSelect = Inputs[InputKey.MultiSelect].Find(x => x.type == KeyType.Keyboard).code;

        _settings.activeJoystick = activeJoystick;
    }
}
