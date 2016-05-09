using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//A NotGate class. Extends the base "LogicGate" class and holds logic for a NOT gate.
public class NotGate : LogicGate
{
    //Constructor
    public NotGate() : base()
    {

    }

    public override bool processInputs(Circuit circuit)
    {
        List<Node> parents = circuit.getParentsOfNode(this);
        Node parent = parents[0];
        return !parent.isActive();
    }

    public override string toString()
    {
        return "Default NotGate.toString()";
    }
}


