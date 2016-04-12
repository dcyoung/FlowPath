using UnityEngine;
using System.Collections;

public class PrimitiveSpawner : MonoBehaviour {
    
    //The colors used for overlapping cues
    public Color defaultColor = Color.white;
    public Color notificationColor = new Color(0.0f, 0.0f, 1.0f, 0.4f);  //Color.red;

    //The type of primitive to spawn
    public Transform primitiveToSpawn;

    //is the index finger currently inside the bin
    private bool bIndexOverlapping = false;
    //is the thumb currently inside the bin
    private bool bThumbOverlapping = false;

    //The bone at the end of the thumb which should be where the primitive spawns
    private Transform thumbBoneToSpawnAt;

    // Use this for initialization
    void Start ()
    {
        defaultColor = GetComponent<Renderer>().material.color;
	}

    // keep track of the index and thumb objects, and if both are overlapping the volume... then trigger an interaction
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "indexTag")
        {
            bIndexOverlapping = true;
            if (ShouldInteract())
            {
                //display a visual cue that the hand in inside the bin
                DisplayVisualCue();
            }
        }
        else if (other.gameObject.tag == "thumbTag")
        {
            bThumbOverlapping = true;
            thumbBoneToSpawnAt = other.gameObject.transform;
            if (ShouldInteract())
            {
                //display a visual cue that the hand in inside the bin
                DisplayVisualCue();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // reset the booleans if the objects are no longer overlapping
        if (other.gameObject.tag == "indexTag")
        {
            bIndexOverlapping = false;
            if (!ShouldContinueInteraction())
            {
                //the hand has left the bin, remove the visual cue 
                RemoveVisualCue();
                //Spawn an object
                BeginInteraction();
            }
        }
        else if (other.gameObject.tag == "thumbTag")
        {
            bThumbOverlapping = false;
            if (!ShouldContinueInteraction())
            {
                //the hand has left the bin, remove the visual cue 
                RemoveVisualCue();
                //Spawn an object
                BeginInteraction();
            }
        }
    }

    //Terminate the objects interaction. In this case...
    //  -remove any visual cue 
    private void EndInteraction()
    {
        print("Ending any existing interaction");
    }


    //Trigger the objects interaction. In this case...
    //  -spawn a primitive and enable the Leap Motion to grab it
    private void BeginInteraction()
    {
        print("Beginning Interaction");
        //spawn the primitive
        Transform primitiveClone = (Transform)Instantiate(primitiveToSpawn, Vector3.zero, Quaternion.identity);

        //Kick off the interaction between the primitive and the hand
        primitiveClone.GetComponent<ItemPickup>().BeginInteractionFromExternal(thumbBoneToSpawnAt);
    }

    //Check that any necessary criterion for interaction have been met. 
    //Define this for whatever behavior you want.   
    private bool ShouldInteract()
    {
        //The top bone of both the index finger and thumb must both be overlapping
        //with the trigger volume.
        bool bCriteriaMet = (bIndexOverlapping && bThumbOverlapping);
        return bCriteriaMet;
    }

    //Check that any necessary criterion for continuing an interaction have been met.
    //Define this for whatever behavior you want.
    private bool ShouldContinueInteraction()
    {
        //only one of the two finger (index + thumb) must be overlapping
        bool bCriteriaMet = (bIndexOverlapping || bThumbOverlapping);
        return bCriteriaMet;
    }


    //Display some visual cue to the user to indicate the item can now be pinch controlled.
    private void DisplayVisualCue()
    {
        SetColor(notificationColor);
    }

    //Remove any visual cue that indicated the item could be pinch controlled.
    private void RemoveVisualCue()
    {
        SetColor(defaultColor);
    }

    //Set the material color for the object
    private void SetColor(Color col)
    {
        GetComponent<Renderer>().material.color = col;
    }

    /*
    private Leap.Unity.PinchUtility.LeapPinchDetector GetClosestPinchDetector()
    {
        float distToLeft = Vector3.Distance(transform.position, leftPinchDetector.transform.position);
        float distToRight = Vector3.Distance(transform.position, rightPinchDetector.transform.position);

        return (distToRight <= distToLeft) ? rightPinchDetector : leftPinchDetector;       
    }
    */
}
