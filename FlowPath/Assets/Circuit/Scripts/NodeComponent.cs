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

    
    void Start()
    {
        switch (nodeType)
        {
            case NodeType.SOURCE:
                node = new Source(false);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(node.GetHashCode() + ": " + node.isActive());
        }
    }

}
