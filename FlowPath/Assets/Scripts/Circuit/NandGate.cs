using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//A NandGate class. Extends the base "LogicGate" class and holds logic for a NAND gate.
public class NandGate : LogicGate
{
    //Constructor
    public NandGate() : base()
    {

    }

    public override bool processInputs(Circuit circuit)
    {
        List<Node> parents = circuit.getParentsOfNode(this);
        Node parent1 = parents[0];
        Node parent2 = parents[1];

        return !(parent1.isActive() && parent2.isActive());
    }

    public override string toString()
    {
        return "Default NandGate.toString()";
    }
}