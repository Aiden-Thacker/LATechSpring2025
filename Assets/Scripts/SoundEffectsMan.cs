using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectsMan : MonoBehaviour
{
    Scene theSceneImIn;

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void schoolSceneSd() {
        while (currentScene == theSceneImIn) {

        }
    }

    IEnumerator playClipsOnInterval() {
        yield return new WaitForSeconds(10f);
        while (currentScene == theSceneImIn) {
            foreach (AudioClip clip in audioClips) {
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(10f);
            }
        }
    }
}

