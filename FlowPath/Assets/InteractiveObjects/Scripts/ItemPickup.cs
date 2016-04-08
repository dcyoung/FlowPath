using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public Color defaultColor = Color.white;
    public Color notificationColor = Color.red;

    public Transform defaultParent;

    private bool _indexOverlapping = false;
    private bool _thumbOverlapping = false;

    void Start()
    {
        //note the initial parent of the object, to help restore the hierarchy after any changes
        defaultParent = transform.parent;
    }

    // keep track of the index and thumb objects, and if both are overlapping the volume... then trigger an interaction
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "indexTag")
        {
            _indexOverlapping = true;
            if (ShouldInteract())
            {
                BeginInteraction(other); // do something like attaching the object to the fingers
            }
        }
        else if (other.gameObject.tag == "thumbTag")
        {
            _thumbOverlapping = true;
            if (ShouldInteract())
            {
                BeginInteraction(other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // reset the booleans if the objects are no longer overlapping
        if (other.gameObject.tag == "indexTag")
        {
            _indexOverlapping = false;
            if (!ShouldContinueInteraction())
            {
                EndInteraction();
            }
        }
        else if (other.gameObject.tag == "thumbTag")
        {
            _thumbOverlapping = false;
            if (!ShouldContinueInteraction())
            {
                EndInteraction();
            }
        }
    }


    //Terminate the objects interaction. In this case...
    //  -restore the original parent hierarchy
    //  -disable the leap motion pinch utility for this object.. so it may NOT be pinch controlled
    //  -remove any visual cue 
    private void EndInteraction()
    {
        GetComponent<Leap.Unity.PinchUtility.LeapRTS>().enabled = false;
        transform.SetParent(defaultParent);
        RemoveVisualCue();
    }


    //Trigger the objects interaction. In this case...
    //  -parent the object to the collider's parent
    //  -enable the leap motion pinch utility for this object.. so it may be pinch controlled
    //  -display a visual cue that the object is able to be pinch controlled
    private void BeginInteraction(Collider other)
    {
        transform.SetParent(other.transform.parent);
        GetComponent<Leap.Unity.PinchUtility.LeapRTS>().enabled = true;
        SetColor(notificationColor);
    }

    //Check that any necessary criterion for interaction have been met. 
    //Define this for whatever behavior you want.   
    private bool ShouldInteract()
    {
        //The top bone of both the index finger and thumb must both be overlapping
        //with the trigger volume.
        bool bCriteriaMet = (_indexOverlapping && _thumbOverlapping);
        return bCriteriaMet;
    }

    //Check that any necessary criterion for continuing an interaction have been met.
    //Define this for whatever behavior you want.
    private bool ShouldContinueInteraction()
    {
        //only one of the two finger (index + thumb) must be overlapping
        bool bCriteriaMet = (_indexOverlapping || _thumbOverlapping);
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
}
