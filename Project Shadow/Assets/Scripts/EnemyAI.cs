using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyAI : MonoBehaviour
{

    public GameObject target;
    Vector3 pos;
    Vector3 destination;
    Vector3 oldPositon;
    private NavMeshAgent agent; //this is the part of enemy that recognized the navmesh which is used for navigation
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
        // below lock rotation so enemy doesnt rotate on3d axis
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        oldPositon = transform.position;
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

        oldPositon = transform.position;
        //if (agent.hasPath)
       //     agent.acceleration = (agent.remainingDistance < closeEnoughMeters) ? deceleration : acceleration;
    }

    public void Wander(GameObject room)
    {
        agent.SetDestination(room.transform.position);
        DebugDrawPath(agent.path.corners);
    }

    public void Chase()
    {
        
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