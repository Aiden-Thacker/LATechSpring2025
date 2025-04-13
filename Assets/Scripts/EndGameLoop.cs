using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameLoop : MonoBehaviour
{ 
    public ChangeScenes sceneScript;
    public SoundEffectsMan soundMan;
    public Coroutine soundRoutine;
    public string goToScene;
    public float wordSpeed; 

    // Start is called before the first frame update
    void Start()
    {   
        goToScene = "EndCredits";
        sceneScript.fadeOut = true;
        if(soundMan != null)
        {
            Debug.Log("Not equal to null");
            soundRoutine = StartCoroutine(soundMan.playClipsOnInterval());
        }
        StartCoroutine(waitForASec());
        // start up sound again
        
    }

    // Update is called once per frame
    void Update() {
    }

    IEnumerator waitForASec() {
        yield return new WaitForSeconds(5f);
        if (goToScene != "")
        {
            if (soundRoutine != null) {
                StopCoroutine(soundRoutine);
                Debug.Log("Stopped sound");
                soundRoutine = null;
            }
        }
        Debug.Log("uh oh...");
        StartCoroutine(sceneScript.changeScenes(goToScene));
        //yield return new WaitForSeconds(5f);
    }

    public void startingCorout() {
        StartCoroutine(sceneScript.changeScenes(goToScene));
        Debug.Log("Going to next scene");
    }
}
