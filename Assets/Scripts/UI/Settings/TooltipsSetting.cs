using UnityEngine.UI;

public class TooltipsSetting : Settings
{
    public Toggle tooltipsToggle;
    public Toggle extendedToggle;

    private Tooltips tooltips;

    protected override void Awake()
    {
        base.Awake();

        tooltipsToggle.onValueChanged.AddListener(delegate { TooltipState(); });
        extendedToggle.onValueChanged.AddListener(delegate { ExtendedState(); });

        tooltips = FindObjectOfType<Tooltips>();
    }

    public void TooltipState()
    {
        settings.tooltipsOn = !settings.tooltipsOn;
        extendedToggle.interactable = settings.tooltipsOn;
        settings.SaveSettings();
    }

    public void ExtendedState()
    {
        tooltips.extendedOn = !tooltips.extendedOn;
    }
}
