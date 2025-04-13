using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMinuteFix : MonoBehaviour
{
    public GameObject mari;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!mari.activeInHierarchy && timer >= 20f)
        {
            gameObject.GetComponent<NPCInteractions>().enabled = true;
        }
    }
}
