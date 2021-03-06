﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour {

    public List<Node> ObstructedNodes; 
    // Use this for initialization
    void Start ()
    {

        CheckNodes();
    }

    void CheckNodes()
    {

        if (ObstructedNodes.Count != 0)
        {
            foreach (Node node in ObstructedNodes)
            {
                node.walkable = true;
            }
            ObstructedNodes.Clear();

        }

        Vector3 bounds = GetComponent<BoxCollider>().bounds.size;

        //Vector2 bounds = new Vector2(30, 30);

        Collider[] results = Physics.OverlapBox(transform.position, bounds, transform.rotation);
        foreach (Collider result in results)
        {
            if (result.gameObject.layer == LayerMask.NameToLayer("Node"))
            {
                ObstructedNodes.Add(result.GetComponent<Node>());

            }
        }

        foreach (Node node in ObstructedNodes)
        {
            node.walkable = false;
        }
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            CheckNodes();
        }
    }


}
