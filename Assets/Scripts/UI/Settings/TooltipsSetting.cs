using UnityEngine.UI;

public class TooltipsSetting : Settings
{
    public Toggle tooltipsToggle;
    public Toggle extendedToggle;

    private Tooltips tooltips;

    protected override void Awake()
    {
        base.Awake();

        tooltipsToggle.onValueChanged.AddListener((_state)=> { TooltipState(_state); });
        extendedToggle.onValueChanged.AddListener(delegate { ExtendedState(); });

        tooltips = FindObjectOfType<Tooltips>();
    }

    private void Start()
    {
        tooltipsToggle.isOn = settings.tooltipsOn;
    }

    public void TooltipState(bool _state)
    {
        settings.tooltipsOn = _state;
        extendedToggle.interactable = settings.tooltipsOn;
        settings.SaveSettings();
    }

    public void ExtendedState()
    {
        tooltips.extendedOn = !tooltips.extendedOn;
    }
}
