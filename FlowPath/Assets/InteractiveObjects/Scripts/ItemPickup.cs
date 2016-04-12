﻿
using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{

    public Color defaultColor = Color.white;
    public Color notificationColor = Color.red;

    public Transform defaultParent;

    private bool bIndexOverlapping = false;
    private bool bThumbOverlapping = false;

    private Transform overlappingThumbBone;


    // keep track of the index and thumb objects, and if both are overlapping the volume... then trigger an interaction
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "indexTag")
        {
            bIndexOverlapping = true;
            if (ShouldInteract())
            {
                BeginInteraction(other); // do something like attaching the object to the fingers
            }
        }
        else if (other.gameObject.tag == "thumbTag")
        {
            bThumbOverlapping = true;
            overlappingThumbBone = other.gameObject.transform;
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
            bIndexOverlapping = false;
            if (!ShouldContinueInteraction())
            {
                EndInteraction();
            }
        }
        else if (other.gameObject.tag == "thumbTag")
        {
            bThumbOverlapping = false;
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
        RemoveVisualCue();
        GetComponent<Leap.Unity.PinchUtility.LeapRTS>().enabled = false;

        ///at runtime, an RTS Anchor is added as the parent of every movable primitive... so it must be reparented to the defaultParent
        //SafetyCheck that the parent of the item is indeed an RTS Anchor
        if (transform.parent.name != "RTS Anchor")
        {
            //for each RTS Anchor that is a child of defaultParent
            foreach (Transform child in defaultParent)
            {
                if (child.childCount == 0)
                {
                    transform.SetParent(child);
                }
            }
        }
        //Set the parent of the RTS Anchor to be the defaultParent
        transform.parent.SetParent(defaultParent);
    }


    //Trigger the objects interaction. In this case...
    //  -parent the object to the collider's parent
    //  -enable the leap motion pinch utility for this object.. so it may be pinch controlled
    //  -display a visual cue that the object is able to be pinch controlled
    private void BeginInteraction(Collider other)
    {
        DisplayVisualCue();
        transform.parent.SetParent(overlappingThumbBone.transform.parent);
        GetComponent<Leap.Unity.PinchUtility.LeapRTS>().enabled = true;
    }

    //Trigger the objects interaction. In this case...
    //  -parent the object to the thumb bones parent
    //  -enable the leap motion pinch utility for this object.. so it may be pinch controlled
    public void BeginInteractionFromExternal(Transform thumbBone)
    {
        DisplayVisualCue();
        transform.parent.SetParent(thumbBone.transform.parent);
        GetComponent<Leap.Unity.PinchUtility.LeapRTS>().enabled = true;
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
    public void DisplayVisualCue()
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
