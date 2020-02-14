using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyAI : MonoBehaviour
{
    
    public GameObject target;
    private NavMeshAgent agent;
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
        Vector3 pos = transform.position;
        pos.z = 1;
        transform.position = pos;
        int rand = Random.Range(1, 5);
        GameObject room = (GameObject) rooms[rand];
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
        //rotate to look at player
        transform.LookAt (target.position);
        transform.Rotate (new Vector3 (0, -90,-90), Space.Self);

        //move towards player
        
        if (Vector3.Distance (transform.position, target.transform.position) > attack1Range) 
        {
                transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
        }
        */
        agent.SetDestination(target.transform.position);
        DebugDrawPath(agent.path.corners);
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

    public void Rest()
    {

    }
}