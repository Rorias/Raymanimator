using TMPro;

using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float avgFrameRate;
    public TMP_Text display_Text;

    private void Update()
    {
        avgFrameRate = (int)((1f / Time.unscaledDeltaTime) + 0.25f);
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
}
