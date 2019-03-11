using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRotation : MonoBehaviour
{
    public float StartRotateSpeed;
    public float MaxRotateSpeed;
    public float Step;

    [HideInInspector]
    public bool FreeLook;

    private float rot;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        if (FreeLook)
        {
            if (Input.GetAxis("HorizontalCamera") != 0)
            {
                rot = Mathf.Lerp(StartRotateSpeed, MaxRotateSpeed, Step * Time.deltaTime);
            }
        }

        transform.Rotate(0, rot * Input.GetAxis("HorizontalCamera"), 0);
    }
}
