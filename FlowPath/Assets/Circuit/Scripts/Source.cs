using UnityEngine;
using System.Collections;

//A Source class. Extends the base "Node" class. Abstracts logic for a source type primitive.
public class Source : Node
{
    //Constructor
    public Source(bool srcValue) : base(srcValue)
    {

    }

    //the source type has no inputs... so its input processing is simply returning the activeState it was last set to.
    public override bool processInputs(Circuit circuit)
    {
        return this.isActive();
    }

    public override string toString()
    {
        return "Default Source.toString()";
    }
}