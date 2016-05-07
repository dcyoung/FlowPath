using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Attach this to an object with a text component to update that text component with text for the current interaction mode.
/// </summary>
public class TextUpdate : MonoBehaviour {

    //The actual text component to be updated. Make sure the parent of this script has a text component.
    private Text displayText;

    //Subscribe to Event: at its creation, subscribe the editable text to receive updates from the mode maanger
    void OnEnable()
    {
        ModeManager.OnModeChange += UpdateDisplayText;
    }
    //Unsubscribe from Event: when disabling this item, unsubscribe the editable text to receive updates from the mode maanger
    void OnDisable()
    {
        ModeManager.OnModeChange -= UpdateDisplayText;
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

    //An event will report if the mode has been updated... change the display text to reflect this
    void UpdateDisplayText(InteractionMode newMode)
    {
        displayText.text = ModeManager.getUpdatedDisplayText();
    }
}
