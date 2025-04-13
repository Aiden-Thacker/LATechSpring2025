using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public GameObject[] npcs;
    public Transform hidingSpot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffAndHide()
    {
        for(int i = 0; i < npcs.Length; i++)
        {
            npcs[i].GetComponent<SpriteRenderer>().enabled = false;
            npcs[0].GetComponent<CapsuleCollider2D>().enabled = false;
        }

        npcs[0].transform.position = hidingSpot.position;
        npcs[0].SetActive(true);
        npcs[0].GetComponent<CapsuleCollider2D>().enabled = true;
        npcs[0].GetComponent<SpriteRenderer>().enabled = false;

    }
}
