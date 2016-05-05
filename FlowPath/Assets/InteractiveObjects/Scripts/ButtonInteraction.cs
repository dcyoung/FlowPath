using UnityEngine;
using System.Collections;

public class ButtonInteraction : MonoBehaviour {

    public InteractionMode interactionMode;

    void onTriggerEnter(Collider other) {
        if (other.gameObject.tag == "palmTag") {
            interactionMode.toggleConnectionMode();
        }
    }
}
