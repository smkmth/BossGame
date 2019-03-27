using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathMovement))]
public class Patrol : MonoBehaviour
{
    private PathMovement pathfinder;
    public List<Transform> PatrolPoints;
    public Transform Player;
    public int targetIndex;
    public float timer;
    public float RePathRate;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<PathMovement>();
        timer = RePathRate;
        //pathfinder.SetDestination(Player.position);



    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {

            pathfinder.SetDestination(Player.position);
            timer = RePathRate;


        }
        else
        {
            timer -= Time.deltaTime;
        }
        /*
        switch (pathfinder.currentMovestate)
        {
            case Movestate.Calculating:

                break;
            case Movestate.Error:
                break;
            case Movestate.Finished:
                break;
            case Movestate.Moving:
                break;
            case Movestate.TargetGiven:
                break;
            case Movestate.Waiting:
                pathfinder.SetDestination(PatrolPoints[targetIndex].position);
                if (targetIndex >= (PatrolPoints.Count - 1))
                {
                    targetIndex = 0;
                }
                else
                {
                    targetIndex += 1;
                }
                break;
        }
        */
    }
}
