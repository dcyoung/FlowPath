using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CircuitManager{

    public delegate void UpdatedCircuitAlert(List<Node> updatedNodes);
    public static event UpdatedCircuitAlert OnCircuitUpdate;


    //The circuit to manage, initialized as empty circuit
    private static Circuit circuit = new Circuit();

    
    public static void ReportUpdatedCircuit()
    {
        if (OnCircuitUpdate != null)
        {
            OnCircuitUpdate(circuit.getNodes());
        }
    }

    private static void UpdateCircuit()
    {
        circuit.solve();
        ReportUpdatedCircuit();
    }

    public static void AddNode(Node newNode)
    {
        circuit.addNode(newNode);
    }

    public static void RemoveNode(Node toRemove)
    {
        circuit.removeNode(toRemove);
    }

    public static void AddEdge(Node parent, Node child)
    {
        circuit.addEdge(parent, child);
    }

    public static void PrintCircuitSummary()
    {
        circuit.printCircuitSummary();
    }

}
