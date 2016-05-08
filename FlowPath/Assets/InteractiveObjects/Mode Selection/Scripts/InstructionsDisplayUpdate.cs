using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionsDisplayUpdate : MonoBehaviour {

    //The actual text component to be updated. Make sure the parent of this script has a text component.
    private Text displayText;

    //Subscribe to Event: at its creation, subscribe the editable text to receive updates from the mode maanger and the prospectiveconnection manager
    void OnEnable()
    {
        ModeManager.OnModeChange += DisplayModeInstructions;
        ProspectiveConnectionManager.OnConnectingStateChange += DisplayConnectionInstructions;
    }
    //Unsubscribe from Event: when disabling this item, unsubscribe the editable text from mode maanger and the prospectiveconnection manager
    void OnDisable()
    {
        ModeManager.OnModeChange -= DisplayModeInstructions;
        ProspectiveConnectionManager.OnConnectingStateChange -= DisplayConnectionInstructions;
    }

    //At the start of the game, look for a text child component
    void Start()
    {
        if (gameObject.GetComponent<Text>() == null)
        {
            Debug.LogError("TextUpdate script must be a be parented to an object with a text component.");
            return;
        }
        displayText = gameObject.GetComponent<Text>();
    }

    //An event will report if the mode has been updated... change the instructions accordingly
    void DisplayModeInstructions(InteractionMode newMode)
    {
        switch (newMode)
        {
            case InteractionMode.SpectatorMode:
                displayText.text = "You are currently spectating.\nTo change modes, press the button on your right.";
                break;
            case InteractionMode.MoveObjectsMode:
                displayText.text = "You've entered build mode.\nPinch grab primtives from the bins, and move them around.";
                break;
            case InteractionMode.ConnectionMode:
                displayText.text = "You've entered connection mode.\nTo form a new connection, start by selecting an output port with your index finger.";
                break;
            default:
                displayText.text = "Default...\n check InstructionsDisplayUpdate.cs";
                break;
        }
    }

    void DisplayConnectionInstructions(ProspectiveConnectionState newState)
    {
        switch (newState)
        {
            case ProspectiveConnectionState.Latent:
                displayText.text = "To form a new connection, start by selecting an output port with your index finger.";
                break;
            case ProspectiveConnectionState.Initiated:
                displayText.text = "An output port has been selected. To complete the connection, use your index finger to select an input port.";
                break;
            case ProspectiveConnectionState.Completed:
                displayText.text = "You have successfully formed a connection.";
                break;
            default:
                displayText.text = "Default...\n check InstructionsDisplayUpdate.cs";
                break;
        }
    }

}
