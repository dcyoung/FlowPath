using UnityEngine;
using System.Collections;


public enum ProspectiveConnectionState
{
    Latent, //No nodes specified
    Initiated,   //Start node has been specified
    Completed    //Start AND End nodes HAVE been specified
}

/// <summary>
/// Manager for all things related to the building of a new connection. Keeps track of the building progress
/// and notifies subscribers of any changes.
/// </summary>
public static class ProspectiveConnectionManager{

    //The current connection to be formed. Will require a user to specify a start and end node.
    public static ProspectiveConnectionState currentConnectionState = ProspectiveConnectionState.Latent;

    public delegate void StateChangeAlert(ProspectiveConnectionState newState);
    public static event StateChangeAlert OnConnectingStateChange;

    private static GameObject outputPort_from = null;
    private static GameObject inputPort_to = null;

    
    //Callable method that will fire the event, alerting all subscribed listeners that the state has changed
    public static void ReportStateChange()
    {
        if (OnConnectingStateChange != null)
        {
            OnConnectingStateChange(currentConnectionState);
        }
    }


    public static void SpecifyOutputPort(GameObject specifiedPort)
    {

        if (currentConnectionState == ProspectiveConnectionState.Latent)
        {
            MonoBehaviour.print("Registered an output port");
            outputPort_from = specifiedPort;
            currentConnectionState = ProspectiveConnectionState.Initiated;
            UpdateConnectionProgress();
            ReportStateChange();
        }
        else
        {
            MonoBehaviour.print("You cannot specify an output port at this time.");
        }
    }
    public static void SpecifyInputPort(GameObject specifiedPort)
    {
        if (currentConnectionState == ProspectiveConnectionState.Initiated)
        {
            MonoBehaviour.print("Registered an input port");
            inputPort_to = specifiedPort;
            currentConnectionState = ProspectiveConnectionState.Completed;
            UpdateConnectionProgress();
            ReportStateChange();
        }
        else
        {
            MonoBehaviour.print("You cannot specify an input port at this time.");
        }
    }

    public static GameObject GetSpecifiedInputPort()
    {
        return inputPort_to;
    }

    public static GameObject GetSpecifiedOutputPort()
    {
        return outputPort_from;
    }

    private static void UpdateConnectionProgress()
    {
        switch (currentConnectionState)
        {
            case ProspectiveConnectionState.Latent:
                ResetConnectionProgress();
                break;
            case ProspectiveConnectionState.Initiated:
                //draw the visual representation of the selected port
                ShowPortSelectionVisual(outputPort_from);
                break;
            case ProspectiveConnectionState.Completed:
                //draw the visual representations of both selected ports
                CompleteConnectionProgress();
                break;
            default:
                break;
        }
    }

    private static void ResetConnectionProgress()
    {
        if (outputPort_from != null)
        {
            RemovePortSelectionVisual(outputPort_from);
            outputPort_from = null;
        }
        if (inputPort_to != null)
        {
            RemovePortSelectionVisual(inputPort_to);
            inputPort_to = null;
        }

        if(currentConnectionState != ProspectiveConnectionState.Latent)
        {
            currentConnectionState = ProspectiveConnectionState.Latent;
            ReportStateChange();
        }
    }

    private static void CompleteConnectionProgress()
    {
        MonoBehaviour.print("Connection Completed");
        ShowPortSelectionVisual(outputPort_from);
        ShowPortSelectionVisual(inputPort_to);

        Node n_from = outputPort_from.transform.parent.GetComponent<NodeComponent>().GetNode();
        Node n_to = inputPort_to.transform.parent.GetComponent<NodeComponent>().GetNode();
        CircuitManager.AddEdge(n_from, n_to);
        CircuitManager.PrintCircuitSummary();

        ResetConnectionProgress();
    }

    //DRAW SOME VISUAL REPRESENTATION OF THE PORT
    private static void ShowPortSelectionVisual(GameObject port)
    {
        ///FIXME:
        MonoBehaviour.print("Showing port selection for: " + port.transform.parent.GetComponent<NodeComponent>().nodeType);
    }

    //Remove any visual representation of the port
    private static void RemovePortSelectionVisual(GameObject port)
    {
        ///FIXME:
    }
}
