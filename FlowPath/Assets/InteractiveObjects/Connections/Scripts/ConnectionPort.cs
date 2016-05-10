using UnityEngine;
using System.Collections;

public class ConnectionPort : MonoBehaviour {

    public enum PortType
    {
        Input,
        Output
    }

    public PortType portType;
    private bool bIsConnected = false;

    private Transform parent;
    private SphereCollider trigger;
    private MeshRenderer portMeshRenderer;

    //Subscribe to Event: subscribes connection port to the mode manager to receive notice when the interaction mode changes
    void OnEnable()
    {
        ModeManager.OnModeChange += EnableConnections;
        ProspectiveConnectionManager.OnConnectingStateChange += LimitPortActivity;
    }
    //Unsubscribe from Event: when disabling this item, unsubscribe the connection port from the mode maanger
    void OnDisable()
    {
        ModeManager.OnModeChange -= EnableConnections;
        ProspectiveConnectionManager.OnConnectingStateChange -= LimitPortActivity;
    }


    // Use this for initialization
    void Start () {
        parent = transform.parent;
        trigger = GetComponent<SphereCollider>();
        portMeshRenderer = GetComponent<MeshRenderer>();
        setActivePort(false);
	}
	
    
    //Change the active state of the port
    private void setActivePort(bool bIsActive)
    {
        HidePortSelection();

        trigger.enabled = bIsActive;
        portMeshRenderer.enabled = bIsActive;
    }


    //An event will report if the mode has been updated... change the presence of connection ports to match
    void EnableConnections(InteractionMode newMode)
    {
        if(newMode == InteractionMode.ConnectionMode)
        {
            setActivePort(portType == PortType.Output);
        }
        else
        {
            setActivePort(false);
        }
    }

    //An event will report if the prospective connection has changed states... change the presence of input or output connection ports to match
    void LimitPortActivity(ProspectiveConnectionState newState)
    {
        switch (newState)
        {
            case ProspectiveConnectionState.Latent:
                //disable the port if its an input... because an output must first be specified for the prospective connection
                setActivePort(portType == PortType.Output);
                break;
            case ProspectiveConnectionState.Initiated:
                //disable the port if its an output... because an output (_from) has already been specified for the prospective connection
                setActivePort(portType == PortType.Input);
                
                //If this port happens to be the _from port that was already selected in this intermediate prospective connection, Display a visual cue that is has been selected.
                if (transform.gameObject == ProspectiveConnectionManager.GetSpecifiedOutputPort())
                {
                    DisplayPortSelection();
                }
                break;
            case ProspectiveConnectionState.Completed:
                //the prospective connection has been formed, with both a specified output and input port. No need to select any more ports.
                setActivePort(false);
                if (transform.gameObject == ProspectiveConnectionManager.GetSpecifiedOutputPort() || transform.gameObject == ProspectiveConnectionManager.GetSpecifiedInputPort())
                {
                    DisplayPortSelection();
                }
                break;
            default:
                break;
        }
    }

    
    //Handle when the user presses the port with their index finger
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "indexTag")
        {
            if(portType == PortType.Input)
            {
                ProspectiveConnectionManager.SpecifyInputPort(transform.gameObject);
            }
            else if(portType == PortType.Output)
            {
                ProspectiveConnectionManager.SpecifyOutputPort(transform.gameObject);
            }
        }
    }

    //Display a visual cue that the port was selected. This will likley be used for showing which output (from) port was already selected when a connection
    //is in the intermediate stage of being built, where the _from port has been selected but the _to port has not yet been selected.
    public void DisplayPortSelection()
    {
        portMeshRenderer.enabled = true;
        portMeshRenderer.material.color = Color.green;
    }

    //Hide the visual cue that the port was selected. 
    public void HidePortSelection()
    {
        portMeshRenderer.material.color = Color.white;
    }

}
