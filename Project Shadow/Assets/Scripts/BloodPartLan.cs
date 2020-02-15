using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPartLan : MonoBehaviour
{
    public Transform partical;
    public static bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        partical.GetComponent<ParticleSystem>().enableEmission = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (show == true)
        {
            partical.GetComponent<ParticleSystem>().enableEmission = true;
        }
        
    }
    public static void Activate()
    {
        show = true;
    }
}
