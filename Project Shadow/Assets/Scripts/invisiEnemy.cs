using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class invisiEnemy : MonoBehaviour
{
    public GameObject player;
    public Vector3 destination;
    private NavMeshAgent agent;
    public Vector3 pos;

    public GameObject Room1;
    public GameObject Room2;
    public GameObject Room3;
    public GameObject Room4;
    public GameObject Room5;
    int count = 5;

    ArrayList rooms = new ArrayList();

    public float acceleration = 2f;
    public float deceleration = 60f;
    public float closeEnoughMeters = 4f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        rooms.Add(Room1);
        rooms.Add(Room2);
        rooms.Add(Room3);
        rooms.Add(Room4);
        rooms.Add(Room5);

        int rand = Random.Range(1, 5);
        GameObject room = (GameObject)rooms[rand];
        Debug.Log("Destination set to " + room);
        Debug.Log("at: " + room.transform.position.x + ", " + room.transform.position.y + ", " + room.transform.position.z);
        Chase(room);

    }

    // Update is called once per frame
    void Update()
    {
        //Chase(player);
        /*
        pos = transform.position;
        pos.z = 1;
        transform.position = pos;
        */
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z); 

        if(Vector3.Distance(transform.position, destination) < 2.5)
        {
            int rand = Random.Range(1, 5);
            GameObject room = (GameObject)rooms[rand];
            Debug.Log("Destination set to " + room);
            Debug.Log("at: " + room.transform.position.x + ", " + room.transform.position.y + ", " + room.transform.position.z);
            Chase(room);
        }

        if (agent.hasPath)
            agent.acceleration = (agent.remainingDistance < closeEnoughMeters) ? deceleration : acceleration;
    }

    public void Chase(GameObject target)
    {
        destination = target.transform.position;
        agent.SetDestination(destination); // uses navmesh to find how to get to target
        DebugDrawPath(agent.path.corners); //draws path on view screen
    }
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
}
