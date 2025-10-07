#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public partial class AnimatorController : MonoBehaviour
{
    public List<GamePart> GetCurrentGameParts()
    {
        return currentGameParts;
    }

    public List<Part> GetCurrentParts()
    {
        return currentParts;
    }
}
#endif