using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAndOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOut = false;

    public float timeToFade; 

    // Update is called once per frame
    void Update() {
        if (fadeIn == true) {
            if (canvasGroup.alpha < 1) {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1) {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut == true) {
            if (canvasGroup.alpha >= 0) {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0) {
                    fadeOut = false;
                }
            }
        }
    }
}
