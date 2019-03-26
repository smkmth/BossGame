using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PathMovement))]
public class Patrol : MonoBehaviour
{
    private PathMovement pathfinder;
    public List<Transform> PatrolPoints;
    public int targetIndex;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<PathMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
