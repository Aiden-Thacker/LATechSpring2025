using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public Volume volume;
    public float tick;
    public float seconds;
    public int min;
    public int hours;

    public bool activateLights;
    public GameObject[] lights;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeVolume();
    }

    void ChangeVolume()
    {
        seconds += Time.fixedDeltaTime * tick;
        if(seconds >= 60)
        {
            seconds = 0;
            min++;
        }
        if(min >= 60)
        {
            min = 0;
            hours++;
        }
        if(hours >= 13)
        {
            hours = 0;
        }
        
        if(hours >= 9 && hours < 11) // Dusk
        {
            volume.weight = ((hours-9) * 60 + min)/120.0f;
            if(!activateLights)
            {
                //Turn On lights
                for(int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(true);
                }
                activateLights = true;
            }
        }
        if(hours == 12)
        {
            Debug.Log("You Lose");
        }

        // if(hours >= 6 && hours < 7) // Dawn
        // {
        //     volume.weight = 1 - (float)min/60;
        //     if(activateLights)
        //     {
        //         //Turn On lights
        //         if(min > 45)
        //         {
        //             for(int i = 0; i < lights.Length; i++)
        //             {
        //                 lights[i].SetActive(false);
        //             }
        //             activateLights = false;
        //         }
        //     }
        // }
    }

}
