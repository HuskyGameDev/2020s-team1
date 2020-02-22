using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatSK : MonoBehaviour
{
    public ParticleSystem particle;
    public static bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        particle.GetComponent<ParticleSystem>().enableEmission = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (show == true)
        {
            particle.GetComponent<ParticleSystem>().enableEmission = true;
        }
    }

    public static void Activate()
    {
        show = true;
    }
}
