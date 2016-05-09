using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ReferenceNodeRemover : MonoBehaviour {

    //A list of all prefabs that are just serving as references to clone when spawning new prefabs
    //the nodes that belong to these reference prefabs should not be part of the main circuit
    public List<Transform> referencePrefabs = null;
	
    // Use this for initialization
	void Start ()
    {
        //delay the removal of the reference nodes because they are added in the "Begin" method of the NodeComponent
        StartCoroutine(WaitAndRemoveCircuitNodes(2.0f));
	}


    //Have to wait a small period of time for all the begin functions in the gameobjects to finish.
    IEnumerator WaitAndRemoveCircuitNodes(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (referencePrefabs != null)
        {
            Node node;
            foreach(Transform refPrefab in referencePrefabs)
            {
                node = refPrefab.GetComponent<NodeComponent>().GetNode();
                CircuitManager.RemoveNode(node);
            }
        }
    }


}
