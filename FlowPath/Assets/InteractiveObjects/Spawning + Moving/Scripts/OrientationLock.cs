using UnityEngine;
using System.Collections;


/// <summary>
/// Simple component that locks the orientation of the parent to its starting orientation. 
/// </summary>
public class OrientationLock : MonoBehaviour {

    private Quaternion initOrientation;

	// Use this for initialization
	void Start () {
        initOrientation = transform.rotation;
	}
	

    void LateUpdate()
    {
        transform.rotation = initOrientation;
    }
}
