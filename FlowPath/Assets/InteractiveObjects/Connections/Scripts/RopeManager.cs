using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


struct RopeStruct
{
    public GameObject start_Point;
    public GameObject end_Point;
    public GameObject start_Port;
    public GameObject end_Port;
    public Node parentNode;
    public bool bCurrentlyPulsing;
    public bool bNeedsBuilding;

    //add any necessary stuff for the 
}
public class RopeManager : MonoBehaviour
{

    private List<RopeStruct> existingRopes;
    public Transform globalParent; //The global parent for all the created end pts to be children of
    public Material ropeMat;
    public Material ropeMat_active;
    void Start()
    {
        existingRopes = new List<RopeStruct>();
        ProspectiveConnectionManager.SetRopeManager(transform);
    }


    void Update()
    {
        for (int i = 0; i < existingRopes.Count; i++)
        {
            // iterate through the ropes and update each
            //UpdateSingleRope(existingRopes[i]);
        }
    }

    private void UpdateSingleRope(RopeStruct rope)
    {
        //update the positions of the start and end points using the actual port positions
        rope.start_Point.transform.position = rope.start_Port.transform.position;
        rope.end_Point.transform.position = rope.end_Port.transform.position;


        //check if this rope should be flowing
        if (rope.parentNode.isActive())
        {
            if (!rope.bCurrentlyPulsing)
            {
                rope.bCurrentlyPulsing = true;
                //make the rope start pulsing... change its material to a pulsing material
                rope.start_Point.GetComponent<Rope_Tube>().material = ropeMat_active;

            }
        }
        else
        {
            if (rope.bCurrentlyPulsing)
            {
                rope.bCurrentlyPulsing = false;
                //make the rope stop pulsing... change its material from a pulsing material to a normal material
                rope.start_Point.GetComponent<Rope_Tube>().material = ropeMat;
            }
        }

        if (rope.bNeedsBuilding)
        {
            rope.start_Point.GetComponent<Rope_Tube>().BuildRope();
            rope.bNeedsBuilding = false;
        }
    }

    public void CreateRope(GameObject start_Port, GameObject end_Port)
    {
        //create a new struct for this rope
        RopeStruct newRope = new RopeStruct();
        //specify the actual ports that makeup this connection
        newRope.start_Port = start_Port;
        newRope.end_Port = end_Port;
        //specify the parent node of the start port... whose active state determines the active state of the connection
        newRope.parentNode = end_Port.transform.parent.GetComponent<NodeComponent>().GetNode(); //this likley has a missing chain call, i'm doing it from memory

        //create 2 game objects
        GameObject start_Point = new GameObject();
        start_Point.AddComponent<Rigidbody>();
        start_Point.GetComponent<Rigidbody>().useGravity = false;
        GameObject end_Point = new GameObject();
        end_Point.AddComponent<Rigidbody>();
        end_Point.GetComponent<Rigidbody>().useGravity = false;

        //make the end_pt a child of the start_pt and place the created duo under the global parent
        start_Point.transform.SetParent(globalParent);
        end_Point.transform.SetParent(start_Point.transform);

        //create the actual rope tube component
        start_Point.AddComponent<Rope_Tube>();
        start_Point.GetComponent<Rope_Tube>().target = end_Point.transform;
        start_Point.GetComponent<Rope_Tube>().material = ropeMat;
        start_Point.GetComponent<Rope_Tube>().enabled = true;

        //specify these hidden end points for the new rope
        newRope.start_Point = start_Point;
        newRope.end_Point = end_Point;

        newRope.bCurrentlyPulsing = newRope.parentNode.isActive();
        newRope.bNeedsBuilding = true;
        UpdateSingleRope(newRope);

        //add the new ropes to the list of existing ropes
        existingRopes.Add(newRope);

    }



}




/*
	//public static GameObject connection_target = null;
	//public static Material rope_material_default = null;



        Material rope_mat = Resources.Load("lambert1", typeof(Material)) as Material;
	    rope_material_default = rope_mat;

        GameObject startCube = outputPort_from;

		//Rope crazy
		GameObject endCube = inputPort_to;

		//Rope not crazy
		//GameObject endCube = GameObject.Find("Cube4");

		//Comment the next two lines for endpoints to be not the cubes, but the input/output spheres
		startCube = startCube.transform.parent.gameObject;
		endCube = endCube.transform.parent.gameObject;

		connection_target = endCube;

		endCube.transform.parent = startCube.transform;

		startCube.AddComponent<Rope_Tube> ();

*/
