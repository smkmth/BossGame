using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Movestate
{
    Waiting,
    Calculating,
    Error,
    TargetGiven,
    Moving,
    Finished
}

public class PathMovement : MonoBehaviour {

    //what state this ai currently is
    public Movestate currentMovestate;

    //our movespeed, and how close we need to be
    //to a target to consider the path finished.
    public float movespeed;
    public float fudgeDistance;

    //private vars that store the list of targets, the index
    //for the target we are currently going for and the 
    //distance from current target.
    private List<Node> targetNodeList;
    private int targetListIndex;
    private float distance;
    private Vector3 Destination;
    public Transform patrolPoint;
    
    //a ref to our pathfinder 
    private Pathfinder pathfinder;
	
    // Use this for initialization
	void Start ()
    {
        pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
        
        targetListIndex = 0;
        currentMovestate = Movestate.Waiting;
	}

    public void SetDestination(Vector3 worldPoint)
    {
        Destination = worldPoint;
        currentMovestate = Movestate.Calculating;
        if (Vector3.Distance(transform.position, worldPoint) < fudgeDistance)
        {
            currentMovestate = Movestate.Finished;
            targetListIndex = 0;

        }
        else
        {
            SetPath(pathfinder.PathFind(transform.position, worldPoint));
        }
    }

    //set path validates the path, to make sure it makes sence then sets it to 
    //targetNodeList and sets the movestate so we start moving.
    void SetPath(List<Node> targetlist)
    {
        currentMovestate = Movestate.TargetGiven;
        if (pathfinder.NoSolution == true)
        {
            currentMovestate = Movestate.Error;

        }
        if (targetlist.Count <= 0)
        {
            currentMovestate = Movestate.Error;

        }
        else
        {
            int targetindex = 0;
            foreach (Node target in targetlist)
            {
                if (target == null) {
                    Debug.Assert(false, "Target " + targetindex + " is null for " + gameObject.name);
                    currentMovestate = Movestate.Error;
                }
                targetindex++;
            }

            targetNodeList = targetlist;     
        }
    }
	
	// Update is called once per frame
	void Update () {

    
        //if we have a path, we move our transfrom towards the taget at a speed. else, we either increment 
        //the targetListIndex to the next target node, or we are finished, and awaiting orders.  this is only 
        //called once we have a valid path passed to us from the setpath function
        if (currentMovestate == Movestate.TargetGiven)
        {
       
    
          
            distance = Vector3.Distance(transform.position, targetNodeList[targetListIndex].location.position);
            if (distance > fudgeDistance)
            {
                if (targetListIndex < targetNodeList.Count-1)
                {
                    float step = movespeed * Time.deltaTime;
                    Vector3 mov = Vector3.MoveTowards(transform.position, targetNodeList[targetListIndex].location.position, step);
                    transform.position = mov;
                }

            }
            else
            {
                if (targetListIndex >= (targetNodeList.Count))
                {
                    currentMovestate = Movestate.Finished;
                    targetListIndex = 0;
                }
                else
                {
                    targetListIndex += 1;
                }
            }
            
        }

		
	}
}
