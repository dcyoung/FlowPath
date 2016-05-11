using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RopeManager : MonoBehaviour
{

    private List<RopeClass> existingRopes;
    public Transform globalParent; //The global parent for all the created end pts to be children of
    public Material ropeMat;
    public Material ropeMat_active;
    private ColorPulser colorPulser;

    public Color startingColor = new Color(118.0f/255.0f, 81.0f / 255.0f, 52.0f / 255.0f, 255.0f / 255.0f);
    public Color endingColor = new Color(251.0f / 255.0f, 110.0f / 255.0f, 2.0f / 255.0f, 255.0f / 255.0f);

    void Start()
    {
		existingRopes = new List<RopeClass>();
        ProspectiveConnectionManager.SetRopeManager(transform);
        colorPulser = new ColorPulser(startingColor * 255, endingColor*255);
    }


    void FixedUpdate()
    {
        ropeMat_active.color = colorPulser.GetUpdatedColor();
    }
    void Update()
    {
        foreach(RopeClass rope in existingRopes)
        {
            // iterate through the ropes and update each
            RemoveRope(rope);//remove the current rope in order to keep it from spazzing out
            UpdateSingleRope(rope);
        }
    }

    void RemoveRope(RopeClass rope)
    {

        foreach (Transform child in rope.start_Point.transform)
        {
            if( child != rope.end_Point.transform)
            {
                Destroy(child.gameObject);
            }
        }
        rope.bNeedsBuilding = true;
    }

	private void UpdateSingleRope(RopeClass rope)
    {
        //update the positions of the start and end points using the actual port positions
        rope.start_Point.transform.position = rope.start_Port.transform.position;
        rope.end_Point.transform.position = rope.end_Port.transform.position;


        //check if this rope should be flowing
		if (rope.parentNode.isActive())
        {
            rope.bCurrentlyPulsing = true;
            if(rope.start_Point.GetComponent<Rope_Tube>().material != ropeMat_active)
            {
                rope.start_Point.GetComponent<Rope_Tube>().material = ropeMat_active;
            }
        }
        else
        {
            rope.bCurrentlyPulsing = false;
            //make the rope stops pulsing... change its material from a pulsing material to a normal material
            if (rope.start_Point.GetComponent<Rope_Tube>().material != ropeMat)
            {
                rope.start_Point.GetComponent<Rope_Tube>().material = ropeMat;
            }
        }
       
        
        //print(rope.start_Point.GetComponent<MeshRenderer>().material.color);
		if (rope.bNeedsBuilding)
        {
            rope.start_Point.GetComponent<Rope_Tube>().BuildRope();
            rope.bNeedsBuilding = false;
        }
    }

    public void CreateRope(GameObject start_Port, GameObject end_Port)
    {
        //create a new struct for this rope
		RopeClass newRope = new RopeClass();
        //specify the actual ports that makeup this connection
        newRope.start_Port = start_Port;
        newRope.end_Port = end_Port;
        //specify the parent node of the start port... whose active state determines the active state of the connection
        newRope.parentNode = start_Port.transform.parent.GetComponent<NodeComponent>().GetNode(); //this likley has a missing chain call, i'm doing it from memory

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
