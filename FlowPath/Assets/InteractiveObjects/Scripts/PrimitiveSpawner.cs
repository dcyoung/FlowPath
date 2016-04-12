using UnityEngine;
using System.Collections;

public class PrimitiveSpawner : ItemInteraction {
    
    //The type of primitive to spawn
    public Transform primitiveToSpawn;

    // Use this for initialization
    void Start ()
    {
        defaultColor = GetComponent<Renderer>().material.color;
        notificationColor = new Color(0.0f, 0.0f, 1.0f, 0.4f);
	}


    //Terminate the objects interaction. In this case we want to spawn a new object when the hand pulls away from the bin. 
    //So...
    //  -remove any visual cue 
    //  -spawn a primitive and enable the Leap Motion to grab it
    protected override void EndInteraction()
    {
        RemoveVisualCue();

        //spawn the primitive
        Transform primitiveClone = (Transform)Instantiate(primitiveToSpawn, Vector3.zero, Quaternion.identity);

        //Kick off the interaction between the primitive and the hand
        primitiveClone.GetComponent<ItemPickup>().BeginInteractionFromExternal(overlappingThumbBone);
    }


    //Trigger the objects interaction. In this case the only interaction is to show that the hand is inside the bin...
    protected override void BeginInteraction()
    {
        DisplayVisualCue();
    }

}
