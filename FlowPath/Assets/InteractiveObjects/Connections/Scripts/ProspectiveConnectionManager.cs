using UnityEngine;
using System.Collections;


public enum ProspectiveConnectionState
{
    Latent, //No nodes specified
    Initiated,   //Start node has been specified
    Completed    //Start AND End nodes HAVE been specified
}

public static class ProspectiveConnectionManager{

    //The current connection to be formed. Will require a user to specify a start and end node.
    public static ProspectiveConnectionState currentConnectionState = ProspectiveConnectionState.Latent;

    public delegate void StateChangeAlert(ProspectiveConnectionState newState);
    public static event StateChangeAlert OnConnectingStateChange;

    private static GameObject inputPort = null;
    private static GameObject outputPort = null;

    //Callable method that will fire the event, alerting all subscribed listeners that the state has changed
    public static void ReportStateChange()
    {
        if (OnConnectingStateChange != null)
        {
            OnConnectingStateChange(currentConnectionState);
        }
    }

    public static void SpecifyInputPort(GameObject specifiedPort)
    {
        if (currentConnectionState == ProspectiveConnectionState.Initiated)
        {
            MonoBehaviour.print("Registered an input port");
            inputPort = specifiedPort;
            currentConnectionState = ProspectiveConnectionState.Completed;
            ReportStateChange();
        }
        else
        {
            MonoBehaviour.print("You cannot specify an input port at this time.");
        }
    }

    public static void SpecifyOutputPort(GameObject specifiedPort)
    {

        if (currentConnectionState == ProspectiveConnectionState.Latent)
        {
            MonoBehaviour.print("Registered an output port");
            outputPort = specifiedPort;
            currentConnectionState = ProspectiveConnectionState.Initiated;
            ReportStateChange();
        }
        else
        {
            MonoBehaviour.print("You cannot specify an output port at this time.");
        }
    }

    public static GameObject GetSpecifiedInputPort()
    {
        return inputPort;
    }

    public static GameObject GetSpecifiedOutputPort()
    {
        return outputPort;
    }
}
