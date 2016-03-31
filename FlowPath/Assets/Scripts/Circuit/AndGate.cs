using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//An AndGate class. Extends the base "LogicGate" class and holds logic for an AND gate.
public class AndGate : LogicGate
{
    //Constructor
    public AndGate() : base()
    {

    }

    public override bool processInputs(Circuit circuit)
    {
        List<Node> parents = circuit.getParentsOfNode(this);
        Node parent1 = parents[0];
        Node parent2 = parents[1];

        return parent1.isActive() && parent2.isActive();
    }

    public override string toString()
    {
        return "Default AndGate.toString()";
    }
}
