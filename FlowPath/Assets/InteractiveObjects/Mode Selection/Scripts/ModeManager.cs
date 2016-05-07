using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum InteractionMode
{
    SpectatorMode,
    MoveObjectsMode,
    ConnectionMode
}


public static class ModeManager{

    public static InteractionMode currentMode = InteractionMode.SpectatorMode;

    public delegate void ModeChangeAlert(InteractionMode newMode);
    public static event ModeChangeAlert OnModeChange; 

    public static void ReportModeChange()
    {
        if (OnModeChange != null)
        {
            OnModeChange(currentMode);
        }
    }


    public static string getUpdatedDisplayText()
    {
        string newDisplayText;
        switch (currentMode)
        {
            case InteractionMode.ConnectionMode:
                newDisplayText = "Mode:\n\nConnection";
                break;
            case InteractionMode.MoveObjectsMode:
                newDisplayText = "Mode:\n\nMove Objects";
                break;
            case InteractionMode.SpectatorMode:
                newDisplayText = "Mode:\n\nSpectate";
                break;
            default:
                newDisplayText = "Default";
                break;
        }
        return newDisplayText;
    }
    
    public static void cycleMode() {
        switch (currentMode)
        {
            case InteractionMode.ConnectionMode:
                currentMode = InteractionMode.SpectatorMode;
                break;
            case InteractionMode.MoveObjectsMode:
                currentMode = InteractionMode.ConnectionMode;
                break;
            case InteractionMode.SpectatorMode:
                currentMode = InteractionMode.MoveObjectsMode;
                break;
            default:
                currentMode = InteractionMode.SpectatorMode;
                break;
        }

        ReportModeChange();
    }

}
