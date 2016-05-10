using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//An OrGate class. Extends the base "LogicGate" class and holds logic for an OR gate.
public class OrGate : LogicGate
{
    //Constructor
    public OrGate() : base()
    {

    }

    public override bool processInputs(Circuit circuit)
    {
        List<Node> parents = circuit.getParentsOfNode(this);
        if (parents == null || parents.Count != 2)
        {
            if (parents.Count == 1)
            {
                //This is an OR node, so even if its missing a parent, it should be on if the one parent is on
                return parents[0].isActive();
            }
            return false;
        }

        Node parent1 = parents[0];
        Node parent2 = parents[1];

        return parent1.isActive() || parent2.isActive();
    }

    public override string toString()
    {
        return "Default OrGate.toString()";
    }
}
