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



    // Use this for initialization
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        Rest();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        /*
        //rotate to look at player
        transform.LookAt (target.position);
        transform.Rotate (new Vector3 (0, -90,-90), Space.Self);

        //move towards player
        if (Vector3.Distance (transform.position, target.position) > attack1Range) 
        {
                transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
        }
        */
        agent.destination = target.transform.position;
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