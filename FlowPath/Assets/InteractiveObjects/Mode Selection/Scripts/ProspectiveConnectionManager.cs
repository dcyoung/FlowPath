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


    public static void ReportStateChange()
    {
        if (OnConnectingStateChange != null)
        {
            OnConnectingStateChange(currentConnectionState);
        }
    }

}
