using UnityEngine;
using System.Collections;

//A logicGate class. Extends the base "Node" class and abstracts logic for all basic logic gates (AND, OR, NAND etc).
public class LogicGate : Node
{
    //Constructor
    public LogicGate() : base()
    {

    }

    //The children of LogicGate will define useful processInput methods
    public override bool processInputs(Circuit circuit)
    {
        return false;
    }

    public override string toString()
    {
        return "Default LogicGate.toString()";
    }
}
