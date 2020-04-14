using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyAI : MonoBehaviour
{

    public GameObject player;
    public GameObject destination;
    Vector3 pos;
    Vector3 oldPositon;
    private NavMeshAgent agent; //this is the part of enemy that recognized the navmesh which is used for navigation

    public GameObject Room1;
    public GameObject Room2;
    public GameObject Room3;
    public GameObject Room4;
    public GameObject Room5;
    int count = 5;

    ArrayList rooms = new ArrayList();

    public float speed = 3f;
    public float attack1Range = 1f;
    public int attack1Damage = 1;
    public float timeBetweenAttacks;

    //public float acceleration = 2f;
    //public float deceleration = 60f;
    //public float closeEnoughMeters = 4f;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // below lock rotation so enemy doesnt rotate on3d axis
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        oldPositon = transform.position;

        rooms.Add(Room1);
        rooms.Add(Room2);
        rooms.Add(Room3);
        rooms.Add(Room4);
        rooms.Add(Room5);

        Wander();
        
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

        //var relativePos = target.transform.position - transform.position;
        var relativePos = oldPositon - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg + 270;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (Vector3.Distance(transform.position, destination.transform.position) < 2.5 && !destination.Equals(player))
        {
            Wander();
        }
        if (Vector3.Distance(player.transform.position, transform.position) < 6)
        {
            destination = player;
            Debug.Log("enemy chasing player");
            Chase();
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 8 && destination.Equals(player))
        {
            Wander();
        }
        DebugDrawPath(agent.path.corners);

        oldPositon = transform.position;
        //if (agent.hasPath)
       //     agent.acceleration = (agent.remainingDistance < closeEnoughMeters) ? deceleration : acceleration;
    }

    public void Wander()
    {
        int rand = Random.Range(1, 5);
        GameObject room = (GameObject)rooms[rand];
        destination = room;
        Debug.Log("enemy wandering");
        Chase();
    }

    public void Chase()
    {
        //destination = target;
        agent.SetDestination(destination.transform.position); // uses navmesh to find how to get to target
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