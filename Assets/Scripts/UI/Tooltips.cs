using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltips : Settings
{
    public CanvasScaler uiScaler;
    [Space]
    public Toggle tooltipsToggle;
    public Toggle extendedToggle;

    private RectTransform rect;
    private Image bg;
    private TMP_Text tooltipText;

    private Vector2 tooltipSize;

    private bool tooltipsOn = true;
    private bool extendedOn = false;

    #region Tooltip text
    #region Editor settings
    private const string SaveButton = "Saves the animation to a .xml and .cs file.\n";
    private const string SaveButtonExtended = "The .xml file will be used to be able to edit the animation again later.\n" +
                                              "The .cs file can be used for any personal purposes.";

    private const string GifButton = "Saves the animation to a zip with images of all the frames.\n" +
                                     "This zip is located in the same path as the animation itself.\n";
    private const string GifButtonExtended = "The GIF images use the size of grid and the camera zoom to determine what images to create.\n" +
                                             "It is suggested to use only multiples of 2 for the camera zoom when exporting a gif.";

    private const string ReverseButton = "Saves the animation with the frame order reversed to a seperate file.\n";
    private const string DoubleButton = "Saves the animation with each frame duplicated in order, to a seperate file.\n";

    private const string CameraZoom = "Type the zoom level of the camera.\n<b>You can also increase and decrease the zoom level using the scroll wheel.</b>";
    private const string CameraZoomReset = "Resets the zoom level of the camera.";

    private const string CameraSpeed = "Type the speed you want the camera to move at.\n<b>You can move the camera using the wasd keys.</b>";
    private const string CameraSpeedReset = "Resets the speed you want the camera to move at.";

    private const string ExtendedTooltipsToggle = "Having this on will display useful/advanced extra information about many buttons, toggles and fields.\n";
    private const string ExtendedTooltipsExtended = "See? Like this. The more you know!";

    private const string GridLOD = "Sets the grid's Level Of Detail. The higher the value, the more pixels per grid square.";
    private const string GridX = "Sets the width of the grid. This won't be saved unless you save the animation.";
    private const string GridY = "Sets the height of the grid. This won't be saved unless you save the animation.";

    private const string ResetColorButton = "Resets the background color to the default color used on editor startup.";

    private const string Play = "Press this button to start playing your animation.\nCopy To Next will be automatically turned off when you press play.\n";
    private const string PlayExtended = "No functions can be used whilst the animation is playing to prevent accidental changes during the playback.";

    private const string PlaybackSpeed = "Type the speed you want your animation to play at.\nThe higher the value, the faster the animation will play.\n";
    private const string PlaybackSpeedExtended = "You cannot go lower than 1.";
    #endregion
    #region Frame settings
    private const string PreviousFrame = "Load the previous frame of the animation to edit.\n";
    private const string NextFrame = "Load the next frame of the animation to edit.\n";
    private const string NextPreviousFrameExtended = "Can be especially useful for animations with 50+ frames.";

    private const string ClearFrame = "Clears the current frame of all it's sprites.\n<b>You can press the delete key if you only want to delete the selected sprite.</b>\n";
    private const string ClearFrameExtended = "The positions of the sprites when they were set to none <b>don't change.</b>";

    private const string FrameSlider = "Select the frame of animation you want to edit.\n<b>You can also use '+' and '-' to change the current frame.</b>\n";
    private const string FrameSliderExtended = "Changing the frame will not save the entire animation, only the position of all the sprites when you switch.";

    private const string RemoveFrame = "Decrease the total frames of the animation by one each time you press it.\n";
    private const string RemoveFrameExtended = "It will always be the last frame that gets removed, not the selected frame.";

    private const string AddFrame = "Increase the total frames of the animation by one each time you press it.\n";
    private const string AddFrameExtended = "It will always be the last frame that gets added, not the one after the selected frame.";

    private const string CopyToNextToggle = "Having this on will save all the current sprites and their positions to the next frame.\n";
    private const string CopyToNextToggleExtended = "If there are already sprites set in the next frame, this will be ignored.\n" +
                                              "<b>Deleting a sprite but not selecting a new one with Copy To Next still on will set the deleted sprite again.</b>\n" +
                                              "This is automatically turned off when playing back the animation.";

    private const string PreviousGhostToggle = "Having this on will show you a transparent version of the sprites from the previous frame if applicable.\n";
    private const string NextGhostToggle = "Having this on will show you a transparent version of the sprites from the next frame if applicable.\n";
    private const string GhostingToggleExtended = "This is automatically turned off when playing back the animation.";
    #endregion
    #region Part settings
    private const string XPos = "Type the new X position of the currently selected sprite.\n";
    private const string YPos = "Type the new Y position of the currently selected sprite.\n";
    private const string XYPosExtended = "<b>You can also use the arrow keys to move a part one pixel at a time.</b>";

    private const string FlipX = "Having this on will mirror the image horizontally for the currently selected sprite.\n";
    private const string FlipY = "Having this on will mirror the image vertically for the currently selected sprite.\n";
    private const string FlipXYExtended = "This will not be saved through multiple frames unless Copy To Next is on.";

    private const string FixX = "Fixes the X position of the sprite if it is not correctly centered on the grid.\n";
    private const string FixY = "Fixes the Y position of the sprite if it is not correctly centered on the grid.\n";
    private const string FixXYExtended = "This is neccesary due to the fact that when selecting certain sprites they are automatically off centered.\n" +
                                         "Moving them with the mouse is also a way of fixing this problem.";

    private const string PartSlider = "Select the sprite in the frame that you want to edit.\n<b>You can also use '<' and '>' to change which sprite you are editing.</b>\n";
    private const string PartSliderExtended = "It is possible to use your mouse to move around sprites in the editor as well.";

    private const string AddPart = "Increases the total sprites of the animation by one each time you press it.\n";
    private const string AddPartExtended = "It will always be the last sprite that gets added.";

    private const string RemovePart = "Decreases the total sprites of the animation by one each time you press it.\n";
    private const string RemovePartExtended = "It will always be the last sprite that gets removed, not the selected or first sprite.";

    private const string Priority = "Set the priority of the currently selected sprite throughout the entire animation.\n";
    private const string PriorityExtended = "<b>Changing the priority in e.g. frame 5, will also change the priority for all the other frames.</b>\n" +
                                            "A higher priority number means it gets drawn over sprites with a lower number.";
    #endregion
    #endregion

    protected override void Awake()
    {
        base.Awake();

        rect = GetComponent<RectTransform>();
        bg = GetComponent<Image>();
        tooltipText = GetComponentInChildren<TMP_Text>();

        tooltipsToggle.onValueChanged.AddListener(delegate { TooltipState(); });
        extendedToggle.onValueChanged.AddListener(delegate { ExtendedState(); });

        tooltipSize = new Vector2(76, 76);
    }

    private void Update()
    {
        if (!tooltipsOn)
        {
            DisableTooltip();
            return;
        }

        UIUtility.GetRayResults();

        if (UIUtility.rayResults.Count <= 0)
        {
            DisableTooltip();
            return;
        }

        Color c = bg.color;
        c.a = 0.7f;
        bg.color = c;

        Debug.Log(UIUtility.rayResults[0].gameObject.name);

        tooltipText.text = UIUtility.rayResults[0].gameObject.name switch
        {
            //File settings
            "SaveAnimationButtonText" => SaveButton + (extendedOn ? SaveButtonExtended : ""),
            "SaveAsGIFButtonText" => GifButton + (extendedOn ? GifButtonExtended : ""),
            "SaveInReverseButtonText" => ReverseButton,
            "SaveAsDoubleButtonText" => DoubleButton,
            //Camera settings
            "CameraZoomField" or "CameraZoomIF" => CameraZoom,
            "ResetZoomButtonText" => CameraZoomReset,
            "CameraSpeedField" or "CameraSpeedIF" => CameraSpeed,
            "ResetSpeedButtonText" => CameraSpeedReset,
            //Tooltip settings
            "ExtendedLabel" => ExtendedTooltipsToggle + (extendedOn ? ExtendedTooltipsExtended : ""),
            //Grid settings
            "GridLODField" or "GridLODIF" => GridLOD,
            "GridXField" or "GridXIF" => GridX,
            "GridYField" or "GridYIF" => GridY,
            //Background settings
            "ResetColorButtonText" => ResetColorButton,
            //Animation settings
            "PlayText" => Play + (extendedOn ? PlayExtended : ""),
            "PlaybackSpeedIF" => PlaybackSpeed + (extendedOn ? PlaybackSpeedExtended : ""),
            //Frame settings
            "RemoveFrameButtonText" => RemoveFrame + (extendedOn ? RemoveFrameExtended : ""),
            "AddFrameButtonText" => AddFrame + (extendedOn ? AddFrameExtended : ""),
            "PreviousFrameButtonText" => PreviousFrame + (extendedOn ? NextPreviousFrameExtended : ""),
            "NextFrameButtonText" => NextFrame + (extendedOn ? NextPreviousFrameExtended : ""),
            "FrameSelectBackground" or "FrameFill" or "FrameHandle" => FrameSlider + (extendedOn ? FrameSliderExtended : ""),
            "CopyToNextLabel" => CopyToNextToggle + (extendedOn ? CopyToNextToggleExtended : ""),
            "PrevGhostLabel" => PreviousGhostToggle + (extendedOn ? GhostingToggleExtended : ""),
            "NextGhostLabel" => NextGhostToggle + (extendedOn ? GhostingToggleExtended : ""),
            "ClearFrameButtonText" => ClearFrame + (extendedOn ? ClearFrameExtended : ""),
            //Part settings
            "RemovePartButtonText" => RemovePart + (extendedOn ? RemovePartExtended : ""),
            "AddPartButtonText" => AddPart + (extendedOn ? AddPartExtended : ""),
            "PartSelectBackground" or "PartFill" or "PartHandle" => PartSlider + (extendedOn ? PartSliderExtended : ""),
            "XPosIF" => XPos + (extendedOn ? XYPosExtended : ""),
            "YPosIF" => YPos + (extendedOn ? XYPosExtended : ""),
            "FlipXLabel" => FlipX + (extendedOn ? FlipXYExtended : ""),
            "FlipYLabel" => FlipY + (extendedOn ? FlipXYExtended : ""),
            "FixX" => FixX + (extendedOn ? FixXYExtended : ""),
            "FixY" => FixY + (extendedOn ? FixXYExtended : ""),
            "PriorityField" or "PriorityIF" => Priority + (extendedOn ? PriorityExtended : ""),
            _ => "",
        };

        if (tooltipText.text == "")
        {
            DisableTooltip();
            return;
        }

        int textLength = tooltipText.text.Split('.').Max(x => x.Length);
        tooltipSize.x = textLength * 7f;
        rect.sizeDelta = tooltipSize;

        float tooltipsSizeX = (rect.sizeDelta.x / 2f) * (Screen.width / uiScaler.referenceResolution.x);
        float tooltipsSizeY = (rect.sizeDelta.y / 2f) * (Screen.height / uiScaler.referenceResolution.y);
        transform.position = new Vector3(Input.mousePosition.x - (Input.mousePosition.x > (Screen.width / 2f) ? tooltipsSizeX : -tooltipsSizeX),
                                         Input.mousePosition.y - (Input.mousePosition.y > (Screen.height * 0.93f) ? tooltipsSizeY : -tooltipsSizeY),
                                         Input.mousePosition.z);
    }

    private void DisableTooltip()
    {
        Color c = bg.color;
        c.a = 0f;
        bg.color = c;
        tooltipText.text = "";
    }

    public void TooltipState()
    {
        tooltipsOn = !tooltipsOn;
        extendedToggle.interactable = tooltipsOn;
    }

    public void ExtendedState()
    {
        extendedOn = !extendedOn;
    }
}
