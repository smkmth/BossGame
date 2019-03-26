using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//builds a grid of nodes to a choosen size and resolution.
public class Grid : MonoBehaviour {

    //node prefab object.
    public GameObject nodeprefab;

    //how big should the grid actually be
    public float gridsizeX;
    public float gridsizeZ;

    //the size of the nodes used
    private float nodeDiameter;
    public float nodeRad;

    //how far each node should check
    public float WallAvoidDistance;
    public float NodeRadiusToCheck;


    private Pathfinder pathfinder;


	// Use this for initialization
	void Awake () {

        List<Node> nodes = new List<Node>();

        pathfinder = GetComponent<Pathfinder>();

        //get diameter from radius
        nodeDiameter = nodeRad * 2;

        //get the size of the grid accomidating for node size 
        gridsizeX = Mathf.RoundToInt(gridsizeX / nodeDiameter);
        gridsizeZ= Mathf.RoundToInt(gridsizeZ / nodeDiameter);
        
        //convert centerpos into a vector2
        Vector3 centerpos = new Vector3(transform.position.x, transform.position.y,transform.position.z);

        //get the location of the bottomleft of the grid baised on this gameobject transform
        Vector3 worldBottomLeft = centerpos - Vector3.right * gridsizeX / 2 - Vector3.forward * gridsizeZ / 2;

        //double sized array to build the actual grid
        for(int x=0; x <= gridsizeX; x++)
        {
            for (int y = 0; y <= gridsizeZ; y++)
            {

                GameObject nodeobj = Instantiate(nodeprefab, transform);
                Vector3 gridPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRad)  + Vector3.forward * (y *nodeDiameter + nodeRad);
                nodeobj.transform.position = gridPoint;
                nodeobj.SetActive(true);
                Node node = nodeobj.GetComponent<Node>();
                node.name = x.ToString() + " " + y.ToString();
                node.WallAvoidDistance = WallAvoidDistance;
                node.RadiusToCheck = NodeRadiusToCheck;
                nodes.Add(node);
            }
            
        }

        foreach(Node node in nodes)
        {
            node.CalculateNeighbors();
        }

        pathfinder.Nodes = nodes;
		
	}
	

}
