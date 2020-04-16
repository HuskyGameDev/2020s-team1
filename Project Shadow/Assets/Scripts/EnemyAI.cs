using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyAI : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer;
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public bool stateWander = true;
    public bool stateChase = false;
    public bool stateListen = false;
    public float timer = 0.0f;
    float seconds = 0;
    public bool waiting = false;
    public bool stateCheckSound1 = false;
    public bool hitWall = false;
    public bool stateCheckSound2 = false;
    public Vector3 lastHeardLocation;

    public GameObject wallCollider;
    public GameObject Ears;
    public GameObject player;
    public Vector3 destination;
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

        if(stateWander) {
            Debug.Log("Wandering!!");
            Wander();
        } else if(stateListen) {
            Debug.Log("Listening!!");
            Listen();
        } else if(stateCheckSound1) {
            //Debug.Log("Checking Sound 1!!");
            CheckSound1();
        } else if(stateCheckSound2) {
            Debug.Log("Checking Sound 2!!");
            CheckSound2();
        } else if(stateChase) {
            Debug.Log("Chasing!!");
            Chase();
        }

        oldPositon = transform.position;
    }

    public void Wander()
    {
        if(Ears.GetComponent<SoundDetection>().soundDetected) {
            stateWander = false;
            stateListen = true;
            waiting = true;
            Debug.Log("State: Listen!!");
            return;
        }
        int rand = Random.Range(1, 5);
        GameObject room = (GameObject)rooms[rand];
        destination = room.transform.position;
        //Debug.Log("enemy wandering");
        GoTo();
    }
    public void Listen() {
        
        StartCoroutine(LateCall());
        return;
    }

    IEnumerator LateCall() {
        Debug.Log("Waitng patiently");
        destination = transform.position;
        GoTo();
        yield return new WaitForSeconds(2f);

        if(Ears.GetComponent<SoundDetection>().soundDetected) {
            stateListen = false;
            stateCheckSound1 = true;
            //Debug.Log("State: SouncCheck 1!!");
            lastHeardLocation = player.transform.position;
        } else {
            stateListen = false;
            stateWander = true;
            yield break;
        }

    }

    public void CheckSound1() {
        if(Ears.GetComponent<SoundDetection>().soundDetected && Vector3.Distance(player.transform.position, transform.position) < 2) {
            stateCheckSound1 = false;
            stateChase = true;
            return;
        }
        if(!wallCollider.GetComponent<WallCollider>().hitWall) {
            Debug.Log("Going Towards Wall");
            transform.position = Vector3.MoveTowards(transform.position, lastHeardLocation, 0.07f);
        } else {
            stateCheckSound1 = false;
            stateCheckSound2 = true;
            Debug.Log("State: Sound Check 2!!");
            return;
        }
        
    }

    public void CheckSound2() {
        if(Ears.GetComponent<SoundDetection>().soundDetected) {
            stateCheckSound2 = false;
            stateChase = true;
            return;
        }
        if(Vector3.Distance(transform.position, lastHeardLocation) < 1) {
            stateListen = true;
            stateCheckSound2 = false;
            Debug.Log("State: Listen!!");
            return;
        }
        destination = lastHeardLocation;
        GoTo();
    }

    public void Chase() {
        Debug.Log("CHASING PLAYER");
        if(Ears.GetComponent<SoundDetection>().soundDetected) {
            lastHeardLocation = player.transform.position;
            destination = player.transform.position;
        } else {
            destination = lastHeardLocation;
        }
        if(Vector3.Distance(lastHeardLocation, transform.position) < 1) {
            stateChase = false;
            stateListen = true;
            return;
        }

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
}