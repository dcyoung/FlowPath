
using UnityEngine;
using System.Collections;

public class ItemPickup : ItemInteraction
{

    public Transform defaultParent;

    //Terminate the objects interaction. In this case...
    //  -restore the original parent hierarchy
    //  -disable the leap motion pinch utility for this object.. so it may NOT be pinch controlled
    //  -remove any visual cue 
    protected override void EndInteraction()
    {
        print("Ending interaction derived");
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
    //  -parent the object to the thumb
    //  -enable the leap motion pinch utility for this object.. so it may be pinch controlled
    //  -display a visual cue that the object is able to be pinch controlled
    protected override void BeginInteraction()
    {
        print("Beginning interaction derived");
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

}
