using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetection : MonoBehaviour
{
    public bool soundDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Detected Sound!!");
        //Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "SoundField_Player") {
            soundDetected = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Sound Went Away!!");
        soundDetected = false;
    }
}
