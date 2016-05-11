using UnityEngine;
using System.Collections;

public class HologramRotator : MonoBehaviour {

    public float turnRate = 1.0f;
    //private Quaternion startingOrientation; 

	// Use this for initialization
	void Start () {
        //startingOrientation = transform.localRotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(0.0f, turnRate, 0.0f);
	}
}
