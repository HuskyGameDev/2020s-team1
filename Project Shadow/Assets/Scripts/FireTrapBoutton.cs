using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapBoutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovement sp = collision.GetComponent<CharacterMovement>();
        if (sp != null)
        {
            //Player activates this trap, shoot sparkes than fire
            Spark.Ativated();
            FireTrap.Ativated();
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
