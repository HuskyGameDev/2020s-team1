using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;


public class Flicker : MonoBehaviour
{
    /* flicker solution from:https://answers.unity.com/questions/742466/camp-fire-light-flicker-control.html */

    public float maxReduction;
    public float maxIncrease;
    public float rateDamping;
    public float strength;
    public bool stopFlickering;

    public Light2D light;
    private float baseIntensity;
    private bool flickering;

    /*public void Reset()
    {
        maxReduction = 0.2f;
        maxIncrease = 0.2f;
        rateDamping = 0.1f;
        strength = 300;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        if(light == null)
        {
            Debug.LogError("Flicker script must have a Light2D component on the same GameObject.");
            return;
        }

        baseIntensity = light.intensity;
        StartCoroutine(DoFlicker());
    }

    // Update is called once per frame
    void Update()
    {
        if(light==null)
        {
            light = GetComponent<Light2D>();
        }
        if(!stopFlickering && !flickering)
        {
            StartCoroutine(DoFlicker());
        }
    }

    private IEnumerator DoFlicker()
    {
        flickering = true;
        while(!stopFlickering)
        {
            light.intensity = Mathf.Lerp(light.intensity, Random.Range(baseIntensity - maxReduction, baseIntensity + maxIncrease), strength * Time.deltaTime);
            yield return new WaitForSeconds(rateDamping);
        }
        flickering = false;
    }
}
