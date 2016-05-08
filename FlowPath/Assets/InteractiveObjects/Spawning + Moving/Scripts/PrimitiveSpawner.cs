using UnityEngine;
using System.Collections;

public class PrimitiveSpawner : ItemInteraction {
    
    //The type of primitive to spawn
    public Transform primitiveToSpawn;
    public Quaternion spawnOrientation = Quaternion.identity;

    //A flag, in case the hand entered the volume but interaction was not allowed to begin (for example, if the InteractionMode was not conducive)
    private bool bInteractionBegun = false;

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
        //Make sure that an interaction actually did begin... its possible that the hand moved into vol, 
        //but the current InteractionMode doesn't permit spawning 
        if (bInteractionBegun)
        {
            base.EndInteraction();

            RemoveVisualCue();

            //spawn the primitive
            Transform primitiveClone = (Transform)Instantiate(primitiveToSpawn, Vector3.zero, spawnOrientation);

            //Kick off the interaction between the primitive and the hand
            primitiveClone.GetComponent<ItemPickup>().BeginInteractionFromExternal(overlappingThumbBone);

            bInteractionBegun = false;
        }
        
    }


    //Trigger the objects interaction. In this case the only interaction is to show that the hand is inside the bin...
    protected override void BeginInteraction()
    {
        base.BeginInteraction();
        bInteractionBegun = true;
        DisplayVisualCue();
    }

}
