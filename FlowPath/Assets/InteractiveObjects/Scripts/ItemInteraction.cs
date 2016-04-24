using UnityEngine;
using System.Collections;

public class ItemInteraction : MonoBehaviour {

    public Color defaultColor = Color.white;
    public Color notificationColor = Color.red;

    //is the index finger top bone currently inside the object
    private bool bIndexOverlapping = false;
    //is the thumb top bone currently inside the object
    private bool bThumbOverlapping = false;

    protected Transform overlappingThumbBone;

    //are the hands interacting with any other objects right now... limit interactions to 1 at a time
    private bool bIsFreeToInteract = true; 




    //Subscribe to Event: at its creation, subscribe the interactable item to the event system that notifies items of other interactions
    void OnEnable()
    {
        InteractionEventManager.OnInteraction += ChangeFreedom;
    }
    //Unsubscribe from Event: when disabling an item, unsubscribe it from the event system that notifies items of other interactions
    void OnDisable()
    {
        InteractionEventManager.OnInteraction += ChangeFreedom;
    }

    //An event will report whenever an item interaction begins or ends... change the freedom to interact with this item accordingly
    void ChangeFreedom(bool bOtherOngoingInteraction)
    {
        bIsFreeToInteract = !bOtherOngoingInteraction;
    }


    // keep track of the index and thumb objects, and if both are overlapping the volume... then trigger an interaction
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "indexTag")
        {
            bIndexOverlapping = true;
            if (ShouldInteract())
            {
                BeginInteraction(); // do something like attaching the object to the fingers
            }
        }
        else if (other.gameObject.tag == "thumbTag")
        {
            bThumbOverlapping = true;
            overlappingThumbBone = other.gameObject.transform;
            if (ShouldInteract())
            {
                BeginInteraction();
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


    //Terminate the objects interaction.
    protected virtual void EndInteraction()
    {
        //... Implement this in the derived class
        InteractionEventManager.ReportInteraction(false);
    }


    //Trigger the objects interaction. In this case...
    protected virtual void BeginInteraction()
    {
        //... Implement this in the derived class
        InteractionEventManager.ReportInteraction(true);
    }

    //Check that any necessary criterion for interaction have been met. 
    //Define this for whatever behavior you want.   
    private bool ShouldInteract()
    {
        //The top bone of both the index finger and thumb must both be overlapping
        //with the trigger volume.
        bool bCriteriaMet = (bIsFreeToInteract && bIndexOverlapping && bThumbOverlapping);
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
    protected void DisplayVisualCue()
    {
        SetColor(notificationColor);
    }

    //Remove any visual cue that indicated the item could be pinch controlled.
    protected void RemoveVisualCue()
    {
        SetColor(defaultColor);
    }

    //Set the material color for the object
    private void SetColor(Color col)
    {
        GetComponent<Renderer>().material.color = col;
    }
}
