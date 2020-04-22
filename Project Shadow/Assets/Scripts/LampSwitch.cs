using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

public class LampSwitch : MonoBehaviour
{
    public GameObject lamps; // a collection of lightSource that make up the player lamp;
    public Light2D lamp1;
    private Flicker flicker1;
    private Flicker flicker2;
    public Light2D lamp2;
    private bool isLightOn = false;

    public float fuelConsumptionRate;
    public float currentFuelLevel;
    public Image fuelBarImage;
    private float maxFuelLevel;
    private float fuelBar;

    public float brightness;

    public void Reset()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        maxFuelLevel = currentFuelLevel; // not sure this is a good idea
        brightness = lamp1.intensity;
        flicker1 = lamp1.GetComponent<Flicker>();
        flicker2 = lamp2.GetComponent<Flicker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))    // space key pressed
        {
            if(!lamps.activeSelf)
            {
                lamps.SetActive(true);
            }
            if (!isLightOn && currentFuelLevel > 0)  // if lamp off and has fuel
            {
                setLampOn(true);
                isLightOn = true;
            } else
                if (isLightOn)      // if lamp on
            {
                setLampOn(false);
                isLightOn = false;
            }
        }

        if(isLightOn)   //lamp consume fuel
        {
            fuelDecrease();

            if (currentFuelLevel <= 0)
            {
                currentFuelLevel = 0;

                setLampOn(false);
                isLightOn = false;
            }

            fuelBar = currentFuelLevel / maxFuelLevel;
            fuelBarImage.fillAmount = fuelBar;
        }
    }

    private void fuelDecrease()
    {
        currentFuelLevel -= fuelConsumptionRate * Time.deltaTime;
    }

    public void fuelIncrease(float fuel)
    {
        if(currentFuelLevel<maxFuelLevel)
        {
            currentFuelLevel += fuel;
            if(currentFuelLevel>maxFuelLevel)
            {
                currentFuelLevel = maxFuelLevel;
            }

            fuelBar = currentFuelLevel / maxFuelLevel;
            fuelBarImage.fillAmount = fuelBar;
        }
    }

    public void setLampOn(bool on)
    {
        if(on)
        {
            lamp1.intensity = brightness;
            flicker1.stopFlickering = false;
            lamp2.intensity = brightness;
            flicker2.stopFlickering = false;
        } else
        {
            flicker1.stopFlickering = true;
            lamp1.intensity = 0;
            flicker2.stopFlickering = true;
            lamp2.intensity = 0;
        }
    }
}
