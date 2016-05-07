using UnityEngine;
using System.Collections;

public class ButtonInteraction : MonoBehaviour {

    public ModeManager manager;

    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "palmTag") {
            manager.cycleConnectionMode();
        }
    }
}
