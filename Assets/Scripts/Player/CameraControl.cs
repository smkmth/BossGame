using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform Target;
    public Camera playercamera;
    public LayerMask LockOnLayerMask;

    private FreeRotation freeRotation;
    private LockOn lockOn;


    public bool LockedOn;
    // Start is called before the first frame update
    void Start()
    {

        lockOn = GetComponent<LockOn>();
        freeRotation = GetComponent<FreeRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LockOn"))
        { 

            if (LockedOn == true) {

                LockedOn = false;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LockOnLayerMask))
                {
                    Debug.Log("Locked");
                    Target = hit.collider.transform;
                    lockOn.SetTarget(Target);
                    LockedOn = true;
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                }
                else
                {

                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

                    Debug.Log("this");

                }
            }

        }
        


        if (LockedOn)
        {
            freeRotation.FreeLook = false;
        }
        else
        {
            freeRotation.FreeLook = true;
            lockOn.IsLockedOn = false;
        }

   

    }
}
