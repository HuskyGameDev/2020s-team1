using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpikeTrapButton : MonoBehaviour
    
{
    public GameObject SpikeTrap1A;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovement sp = collision.GetComponent<CharacterMovement>();
        if (sp != null)
        {
            SpikeTrapAS.Ativated();
        }
       // SpikeTrapAS var = new SpikeTrapAS();
       // var.Ativated();



    }
    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
