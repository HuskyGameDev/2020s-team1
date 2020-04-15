using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{

    public Vector3 oldPosition;
    public Vector3 relativePos;
    public CircleCollider2D Sound;
    public bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            if(isRunning) {
                isRunning = false;
            } else isRunning = true;
        }

        relativePos = oldPosition - transform.position;
        if(relativePos.x > 0 || relativePos.y > 0 || relativePos.x < 0 || relativePos.y < 0) {
            if(isRunning) {
                Sound.radius = 7;
            } else Sound.radius = 5;
        } else {
            Sound.radius = 0;
        }

        oldPosition = transform.position;
    }
}
