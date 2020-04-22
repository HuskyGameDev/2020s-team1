using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class MenuMosnter : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer;
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public Vector3 destination;
    Vector3 pos;
    Vector3 oldPositon;
    private NavMeshAgent agent; //this is the part of enemy that recognized the navmesh which is used for navigation

    public GameObject Room1;
    public GameObject Room2;
    public GameObject Room3;
    public GameObject Room4;
    public GameObject Room5;

    ArrayList rooms = new ArrayList();

    private int upDateCount = 0;    // update count for updating oldPosition

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

        //Wander();
        
    }

    // Update is called once per frame
    void Update()
    {
        // below three lines lock z position so it doesnt go below the map
        pos = transform.position;
        pos.z = 1;
        transform.position = pos;

        // this locks rotation on x and y becase 2d objects only need to rotate on z axis
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        //var relativePos = target.transform.position - transform.position;
        var relativePos = oldPositon - transform.position;
        if(relativePos.x > 0) {
            spriteRenderer.sprite = right;
        } 
        if(relativePos.x < 0) {
            spriteRenderer.sprite = left;
        }
        if(relativePos.y > 0) {
            spriteRenderer.sprite = up;
        }
        if(relativePos.y < 0) {
            spriteRenderer.sprite = down;
        }

        /*
         * Zong's implementation of switching sprite
         */
        if(relativePos.x>0)//moving right
        {
            if (relativePos.y > 0) // moving up
            {
                if (relativePos.x > relativePos.y) // more x than y
                {
                    spriteRenderer.sprite = left;
                }
                else
                    spriteRenderer.sprite = up;
            }
            else // moving down or not
            {
                if (relativePos.x > (0 - relativePos.y))   // more x than y
                {
                    spriteRenderer.sprite = left;
                }
                else
                    spriteRenderer.sprite = down;   // more y than x
            }
        }
        else // moving left
        {
            if(relativePos.y>0) // left & up
            {
                if((0-relativePos.x)>relativePos.y) // more left than up
                {
                    spriteRenderer.sprite = right;
                }
                else // more up than left
                {
                    spriteRenderer.sprite = up;
                }
            }
            else // moving down or not
            {
                if(relativePos.x<relativePos.y) // more left than down
                {
                    spriteRenderer.sprite = right;
                }
                else
                {
                    spriteRenderer.sprite = down;
                }
            }
        }
        Wander();

        if (oldPos()) //oldPosition update per 3 update
        {
            oldPositon = transform.position;
        }
    }

    public void Wander()
    {
        
        int rand = Random.Range(1, 5);
        GameObject room = (GameObject)rooms[rand];
        destination = room.transform.position;
        //Debug.Log("enemy wandering");
        GoTo();
    }

    public void GoTo()
    {
        agent.SetDestination(destination); // uses navmesh to find how to get to target
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

    private bool oldPos()
    {
        if (upDateCount > 2)
        {
            upDateCount = 0;
        }
        else
            upDateCount++;

        return upDateCount == 0;
    }
}