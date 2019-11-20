using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
 

public class EnemyAI : MonoBehaviour
{
         public Transform target;
         public float speed = 3f;
         public float attack1Range = 1f;
         public int attack1Damage = 1;
         public float timeBetweenAttacks;
 
 
         // Use this for initialization
         void Start ()
         {
             Rest ();
         }
     
         // Update is called once per frame
         void Update ()
         {
        MoveToPlayer();
         }
 
         public void MoveToPlayer ()
         {
             //rotate to look at player
             transform.LookAt (target.position);
             transform.Rotate (new Vector3 (0, -90,-90), Space.Self);
         
             //move towards player
             if (Vector3.Distance (transform.position, target.position) > attack1Range) 
             {
                     transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
             }
         }
 
         public void Rest ()
         {
			;
         }
}
