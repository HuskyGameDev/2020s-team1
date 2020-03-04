using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSourceControl : MonoBehaviour
{
    public Light lightSource;
    //public Light lightSource2;
    private bool isLightOn = false;
    public float fuelDecreasePerSecond;
    public float currentFuelLevel;
    public SpriteRenderer fuelBar;
    private float maxFuelLevel;
    public float fuelBarSize;

    // Start is called before the first frame update
    void Start()
    {
        maxFuelLevel = currentFuelLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isLightOn && currentFuelLevel > 0)
        {
            lightSource.intensity = 3;
            isLightOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isLightOn)
        {
            lightSource.intensity = 0;
            isLightOn = false;
        }

        if (isLightOn)
        {
            fuelDecrease();
            if(currentFuelLevel < 0)
            {
                currentFuelLevel = 0;
                fuelBar.size = new Vector2(fuelBarSize * (currentFuelLevel / maxFuelLevel), fuelBar.size.y);
                lightSource.intensity = 0;
                isLightOn = false;
            }
            else
            {
                fuelBar.size = new Vector2(fuelBarSize * (currentFuelLevel / maxFuelLevel), fuelBar.size.y);
            }
            
        }
        

    }

    public void fuelDecrease()
    {
        currentFuelLevel -= fuelDecreasePerSecond*Time.deltaTime;
    }

    public void fuelIncrease(float fuel)
    {
        if(currentFuelLevel < maxFuelLevel)
        {
            currentFuelLevel += fuel;
            if(currentFuelLevel > maxFuelLevel)
            {
                currentFuelLevel = maxFuelLevel;
            }

            fuelBar.size = new Vector2(fuelBarSize * (currentFuelLevel / maxFuelLevel), fuelBar.size.y);

        }
        
    }
}
