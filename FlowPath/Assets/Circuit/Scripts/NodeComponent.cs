using UnityEngine;
using System.Collections;

public enum NodeType
{
    SOURCE,
    AND,
    OR,
    NOT
}
public class NodeComponent : MonoBehaviour {

    public NodeType nodeType;
    private Node node;

    MeshRenderer nodeRenderer;

    //Subscribe to Event: subscribes to the mode manager to receive notice when the interaction mode changes
    void OnEnable()
    {
        ModeManager.OnModeChange += CheckForDisplay;
        CircuitManager.OnCircuitUpdate += DisplayActiveState;
    }
    //Unsubscribe from Event: when disabling this item, unsubscribe the connection port from the mode maanger
    void OnDisable()
    {
        ModeManager.OnModeChange -= CheckForDisplay;
        CircuitManager.OnCircuitUpdate -= DisplayActiveState;
    }


    void Start()
    {
        nodeRenderer = transform.GetComponent<MeshRenderer>();
        switch (nodeType)
        {
            case NodeType.SOURCE:
                node = new Source(true);
                break;
            case NodeType.AND:
                node = new AndGate();
                break;
            case NodeType.OR:
                node = new OrGate();
                break;
            case NodeType.NOT:
                node = new NotGate();
                break;
            default:
                break;
        }

        CircuitManager.AddNode(node);

    }

    public Node GetNode()
    {
        return node;
    }

    

    private void CheckForDisplay(InteractionMode newMode)
    {
        //If behavior needs to change based on the mode, put the branching behaviour in here:

        //for now, just update the active state display on any mode change
        DisplayActiveState();
    }


    //Display a visual cue indicating whether the node is active or not
    public void DisplayActiveState()
    {
        if(node == null)
        {
            return;
        }
        if (node.isActive())
        {
            nodeRenderer.material.color = Color.green;
        }
        else
        {
            nodeRenderer.material.color = Color.white;
        }
    }


    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        print(node.GetHashCode() + ": " + node.isActive());
    //    }
    //}

}
