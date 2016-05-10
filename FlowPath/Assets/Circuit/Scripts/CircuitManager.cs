using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CircuitManager{

    public delegate void UpdatedCircuitAlert();
    public static event UpdatedCircuitAlert OnCircuitUpdate;


    //The circuit to manage, initialized as empty circuit
    private static Circuit circuit = new Circuit();

    
    public static void ReportUpdatedCircuit()
    {
        if (OnCircuitUpdate != null)
        {
            OnCircuitUpdate();
        }
    }

    public static void UpdateCircuit()
    {
        circuit.solve();
        ReportUpdatedCircuit();
    }

    public static void AddNode(Node newNode)
    {
        circuit.addNode(newNode);
        UpdateCircuit();
    }

    public static void RemoveNode(Node toRemove)
    {
        circuit.removeNode(toRemove);
        UpdateCircuit();
    }

    public static void AddEdge(Node parent, Node child)
    {
        circuit.addEdge(parent, child);
        UpdateCircuit();
    }

    public static void PrintCircuitSummary()
    {
        circuit.printCircuitSummary();
    }

}
