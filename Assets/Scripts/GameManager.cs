using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LoseMenu loseScreen;
    public Volume volume;
    public float tick;
    public float seconds;
    public int min;
    public int hours;

    public bool activateLights;
    public List<GameObject> lights;

    public bool startTimer;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance.loseScreen = null;
            instance.volume = null;
            instance.lights.Clear();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Update()
    {
        if (loseScreen == null)
            loseScreen = FindFirstObjectByType<LoseMenu>();

        if (volume == null)
            volume = FindFirstObjectByType<Volume>();

        GameObject[] allTagged = GameObject.FindGameObjectsWithTag("Lights");
        foreach (GameObject obj in allTagged)
        {
            obj.SetActive(false);
            lights.Add(obj);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startTimer)
        {
            ChangeVolume();
        }
    }

    void ChangeVolume()
    {
        seconds += Time.fixedDeltaTime * tick;
        if (seconds >= 60)
        {
            seconds = 0;
            min++;
        }
        if (min >= 60)
        {
            min = 0;
            hours++;
        }
        if (hours >= 13)
        {
            hours = 0;
        }

        if (hours >= 9 && hours < 11) // Dusk
        {
            if (volume != null)
            {
                volume.weight = ((hours - 9) * 60 + min) / 120.0f;
                if (!activateLights)
                {
                    //Turn On lights
                    for (int i = 0; i < lights.Count; i++)
                    {
                        lights[i].SetActive(true);
                    }
                    activateLights = true;
                }
            }

        }
        if (hours == 12 && loseScreen != null)
        {
            Debug.Log("You Lose");
            loseScreen.Lose();
        }
    }
}
