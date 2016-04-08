using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Circuit
{
    private List<Node> nodes; //all nodes that make up the directed graph (circuit)

    //adjacency lists to represent the edges of the graph 
    private Dictionary<Node, List<Node>> dependencies;  // mapping dependencies (these aren't the direction of the directed edges)
    private Dictionary<Node, List<Node>> dependents;  // mapping edges for dependents


    private Stack<Node> reversePost;
    private Dictionary<Node, bool> marked;

    /**
     * Constructor
     */
    public Circuit()
    {
        nodes = new List<Node>();
        dependencies = new Dictionary<Node, List<Node>>();
        dependents = new Dictionary<Node, List<Node>>();
    }

    /**
     * Solves the digital logic circuit by performing a topological sort
     * on all the nodes and then determining the active state of each in 
     * that order based off their inputs (active states of their parents).
     */
    public void solve()
    {
        //topologically sort the nodes so that every node comes after any dependencies
        Stack<Node> sortedNodes = this.topologicalSort();

        //for each node, set the active state by processing the inputs to the node (inputs are 
        //just the activeState of the dependencies (parent nodes)... which in turn were determined
        //previously by the activeState of their parents and so on... all the way to the top where the
        //source processInputs() fxn simply returns the current activeState of the source node.
        Node next;
        while (sortedNodes.Count > 0)
        {
            next = sortedNodes.Pop();
            next.setActiveState(next.processInputs(this));
        }
    }

    /**
     * Prints the resulting order of nodes after a topological sort performed on the circuit.
     */
    public void printTopologicalSort()
    {
        UtilityPrinter.print("Printing topological sort...");
        Stack<Node> sortedNodes = this.topologicalSort();
        Node next;
        while (sortedNodes.Count > 0)
        {
            next = sortedNodes.Pop();
            UtilityPrinter.print(next.toString());
        }
    }

    /**
     * Sorts the circuit's nodes in topological order such that all dependencies of nodes preceed the node
     */
    public Stack<Node> topologicalSort()
    {
        //create a new stack to hold the final order
        reversePost = new Stack<Node>();
        //reset all the marked flags to false
        marked = new Dictionary<Node, bool>();
        foreach (Node n in this.nodes)
        {
            marked.Add(n, false);
        }

        //Perform a Depth First Search (DFS) to topologically sort the directed graph
        foreach (Node n in this.nodes)
        {
            if (!marked[n])
            {
                depthFirstSearch(n);
            }
        }

        return reversePost;
    }

    /**
     * Recursive Helper Function for the topological sort
     */
    private void depthFirstSearch(Node n)
    {
        marked[n] = true;
        foreach (Node child in this.getChildrenOfNode(n))
        {
            if (!marked[child])
            {
                depthFirstSearch(child);
            }
        }
        reversePost.Push(n);
    }

    /**
     * Retrieves all of the dependencies (parents) of a node
     */
    public List<Node> getParentsOfNode(Node n)
    {
        if (this.dependencies.ContainsKey(n))
        {
            return this.dependencies[n];
        }
        else
        {
            return null;
        }
    }

    /**
     * Retrieves all of the dependents (children) of a node
     */
    public List<Node> getChildrenOfNode(Node n)
    {
        if (this.dependents.ContainsKey(n))
        {
            return this.dependents[n];
        }
        else
        {
            return null;
        }
    }

    /**
     * Adds a node to the circuit if it isn't already part of the circuit
     */
    public void addNode(Node n)
    {
        if (!this.nodes.Contains(n))
        {
            this.nodes.Add(n);

            //add empty lists of dependents and dependencies for this node
            if (!this.dependents.ContainsKey(n))
            {
                this.dependents.Add(n, new List<Node>());
            }
            if (!this.dependencies.ContainsKey(n))
            {
                this.dependencies.Add(n, new List<Node>());
            }
        }
    }

    /**
     * Adds multiple nodes to the circuit if  they aren't already part of the circuit
     */
    public void addNodes(List<Node> nodes)
    {
        foreach (Node n in nodes)
        {
            this.addNode(n);
        }
    }


    /**
     * Removes a node from the circuit
     */
    public void removeNode(Node n)
    {
        if (this.nodes.Contains(n))
        {
            this.removeEdges(n);
            this.nodes.Remove(n);
        }
    }

    /**
     * Adds an edge connecting parent to child
     */
    public void addEdge(Node parent, Node child)
    {
        if (!this.dependencies.ContainsKey(child))
        {
            List<Node> parents = new List<Node>();
            parents.Add(parent);
            this.dependencies.Add(child, parents);
        }
        else
        {
            this.dependencies[child].Add(parent);
        }

        if (!this.dependents.ContainsKey(parent))
        {
            List<Node> children = new List<Node>();
            children.Add(child);
            this.dependencies.Add(parent, children);
        }
        else
        {
            this.dependents[parent].Add(child);
        }
    }

    /**
     * Removes the edge connecting child and parent if it exists
     */
    public void removeEdge(Node child, Node parent)
    {
        if (this.dependencies.ContainsKey(child))
        {
            if (this.dependencies[child].Contains(parent))
            {
                this.dependencies[child].Remove(parent);
            }
        }

        if (this.dependents.ContainsKey(parent))
        {
            if (this.dependents[parent].Contains(child))
            {
                this.dependents[parent].Remove(child);
            }
        }
    }

    /**
     * Removes any and alledges from the graph that are entering or exiting node ns
     */
    public void removeEdges(Node n)
    {
        if (this.dependencies.ContainsKey(n))
        {
            this.dependencies.Remove(n);

        }

        if (this.dependents.ContainsKey(n))
        {
            this.dependents.Remove(n);
        }
    }

}
