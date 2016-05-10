using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to an object with a collision volume that is setup to be a "Trigger". When a leapmotion palm hits the trigger, the interaction mode is cycled.
/// </summary>
public class ButtonInteraction : MonoBehaviour {

   
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "palmTag") {
            ModeManager.cycleMode();
        }
    }

	void Update() {
		if (Input.GetKeyDown (KeyCode.M)) {
			ModeManager.cycleMode();

//			GameObject startCube = GameObject.Find ("Cube3");
//			GameObject endCube = GameObject.Find ("Cube4");
//
//			startCube.AddComponent<Rope_Tube> ();
//			startCube.GetComponent<Rope_Tube> ().target = endCube.transform;
//			endCube.transform.parent = startCube.transform;
		}
	}
}
