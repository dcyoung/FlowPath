using UnityEngine;
using System.Collections;

public class ControlSwitch : MonoBehaviour
{

    private Transform parent;
    private SphereCollider trigger;
    private MeshRenderer switchMeshRenderer;

    //Subscribe to Event: subscribes switch to the mode manager to receive notice when the interaction mode changes
    void OnEnable()
    {
        ModeManager.OnModeChange += EnableConnections;
    }

    //Unsubscribe from Event: unsubscribe the connection switch from the mode maanger
    void OnDisable()
    {
        ModeManager.OnModeChange -= EnableConnections;
    }


    // Use this for initialization
    void Start()
    {
        parent = transform.parent;
        trigger = GetComponent<SphereCollider>();
        switchMeshRenderer = GetComponent<MeshRenderer>();
        setActiveSwitch(false);
    }


    //Change the active state of the switch
    private void setActiveSwitch(bool bIsActive)
    {
        trigger.enabled = bIsActive;
        switchMeshRenderer.enabled = bIsActive;
    }


    //An event will report if the mode has been updated... change the presence of switch to match
    void EnableConnections(InteractionMode newMode)
    {
        //only present the switch in connection mode
        setActiveSwitch(newMode == InteractionMode.ConnectionMode);

    }

    //Handle when the user presses the switch with their index finger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "indexTag")
        {
            //toggle the source value
            ToggleSourceValue();
        }
    }

    void ToggleSourceValue()
    {
        //toggle the source value
        NodeComponent nodeComp = parent.GetComponent<NodeComponent>();
        bool currState = nodeComp.GetNode().isActive();
        nodeComp.GetNode().setActiveState(!currState);
        CircuitManager.UpdateCircuit();
    }
}
