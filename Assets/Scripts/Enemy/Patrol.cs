using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PathMovement))]
public class Patrol : MonoBehaviour
{

    public enum AIStates {
        Waiting,
        Pathing


    };
    private PathMovement pathfinder;
    public List<Transform> PatrolPoints;
    public Transform Player;
    public int targetIndex;
    public float timer;
    public float RePathRate;
    public float ErrorRePathRate;

    public AIStates currentAIState;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<PathMovement>();
        timer = RePathRate;
        currentAIState = AIStates.Pathing;

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAIState)
        {
            case AIStates.Pathing:
                if (timer <= 0)
                {
                    timer = RePathRate;
                    if (pathfinder.currentMovestate != Movestate.Error)
                    {
                        pathfinder.SetDestination(Player.position);
                        currentAIState = AIStates.Pathing;

                    }
                    else
                    {
                        timer = ErrorRePathRate;
                        currentAIState = AIStates.Waiting;

                    }
                }
                break;
            case AIStates.Waiting:
                if (timer <= 0)
                {
                    timer = RePathRate;
                    pathfinder.currentMovestate = Movestate.Waiting;

                    currentAIState = AIStates.Pathing;
                }
                break;

            
        }
        timer -= Time.deltaTime;
        
      
    }
}
