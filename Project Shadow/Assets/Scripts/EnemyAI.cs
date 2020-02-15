﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyAI : MonoBehaviour
{

    public GameObject target;
    Vector3 pos;
    private NavMeshAgent agent; //this is the part of enemy that recognized the navmesh which is used for navigation
    public float speed = 3f;
    public float attack1Range = 1f;
    public int attack1Damage = 1;
    public float timeBetweenAttacks;

    public GameObject Room1;
    public GameObject Room2;
    public GameObject Room3;
    public GameObject Room4;
    public GameObject Room5;
    int count = 5;

    ArrayList rooms = new ArrayList();

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // below lock rotation so enemy doesnt rotate on3d axis
        agent.updateRotation = false;
        agent.updateUpAxis = false;


        Rest();
        rooms.Add(Room1);
        rooms.Add(Room2);
        rooms.Add(Room3);
        rooms.Add(Room4);
        rooms.Add(Room5);
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        // below three lines lock z position so it doesnt go below the map
        pos = transform.position;
        pos.z = 1;
        transform.position = pos;

        // this locks rotation on x and y becase 2d objects only need to rotate on z axis
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        /*
        Vector3 direction = transform.position;
        var rotation = Quaternion.LookRotation(direction);
        if (direction == new Vector3(1, 0, 0)) //if direction of vector is positive in x direction
        {
            rotation *= Quaternion.Euler(0, 0, 90); // rotate charater 90 degrees on z axis
        }
        if (direction == new Vector3(-1, 0, 0)) //if direction of vector is negative in x direction
        {
            rotation *= Quaternion.Euler(0, 0, 270); // rotate charater 270 degrees on z axis
        }
        if (direction == new Vector3(0, 1, 0)) //if direction of vector is positive in y direction
        {
            rotation *= Quaternion.Euler(0, 0, 180); // rotate charater 180 degrees on z axis
        }
        if (direction == new Vector3(0, -1, 0)) // if direction of vector is negative in y direction
        {
            rotation *= Quaternion.Euler(0, 0, 0); // rotate charater 0 degrees on z axis
        }
        transform.eulerAngles = rotation.eulerAngles; */

        int rand = Random.Range(1, 5);
        GameObject room = (GameObject)rooms[rand];
        //Wander(room);
    }

    public void Wander(GameObject room)
    {
        agent.SetDestination(room.transform.position);
        DebugDrawPath(agent.path.corners);
    }

    public void Chase()
    {
        /*
        if (Vector3.Distance (transform.position, target.transform.position) > attack1Range) 
        {transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));}
        */

        //transform.LookAt(target.transform.position);
        //transform.Rotate(new Vector3(0, -90, -90), Space.Self);
        agent.SetDestination(target.transform.position); // uses navmesh to find how to get to target
        DebugDrawPath(agent.path.corners); //draws path on view screen



    }

    //this method just shows lines for where enemy is going, not necessary for function of pathfinding
    public static void DebugDrawPath(Vector3[] corners)
    {
        if (corners.Length < 2) { return; }
        int i = 0;
        for (; i < corners.Length - 1; i++)
        {
            Debug.DrawLine(corners[i], corners[i + 1], Color.blue);
        }
        Debug.DrawLine(corners[0], corners[1], Color.red);
    }

    public void Rest()
    {

    }
}