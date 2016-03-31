using UnityEngine;
using System.Collections;

public class Node
{
    private bool bIsActive;
    
    //Constructor
    public Node()
    {
        this.bIsActive = false;
    }
    //used just for "source" types (ie: inputs or user switches)
    public Node(bool startingState)
    {
        this.bIsActive = startingState;
    }

    //processes the inputs coming to the node according to the logic of the node and return 
    //a value indicating whether the node should be active or not given the inputs
    public virtual bool processInputs(Circuit circuit)
    {
        return false;
    }

    //whether or not the node is currently active 
    public bool isActive()
    {
        return this.bIsActive;
    }

    public void setActiveState(bool activeState)
    {
        this.bIsActive = activeState;
    }

    public virtual string toString()
    {
        return "Default Node.toString()";
    }

}
