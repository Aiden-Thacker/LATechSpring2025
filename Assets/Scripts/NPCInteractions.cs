using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.SceneManagement;

public class NPCInteractions : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI firstNPCdialog;
    public TextMeshProUGUI NPCNameDialog;
    public SpriteRenderer npcRenderer;
    public string[] NPCNames;
    public string[] NPCScript;                      // each index holds a string of text, can add different sentences to different indexes
    private int index;
    public PlayerController playerController;

    public GameObject startChat;
    public TextMeshProUGUI continueText;
    public float wordSpeed;
    public bool autoStart;
    public bool godSpeedAiden;
    public bool playerInRange;
    public bool chatChecker;
    public bool turnOffNPCRenderer;
    public string goToScene;
    public ChangeScenes sceneScript;

    public bool chatDone;
    public CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOut = false;
    public float timeToFade; 
    public CutScene hidingSpot;
    public CutSceneCountDown countDownDialog;

    public SoundEffectsMan soundMan;
    public Coroutine soundRoutine;
    

    void Start()
    {
        dialogPanel.SetActive(false);
        continueText.gameObject.SetActive(false);
        sceneScript.fadeOut = true;
        soundRoutine = StartCoroutine(soundMan.playClipsOnInterval());
        if(turnOffNPCRenderer)
        {
            npcRenderer.enabled = false;
        }
        else
        {
            npcRenderer.enabled = true;
        }

        startChat.SetActive(false);
        if(autoStart)
        {
            chatChecker = false;
        }
        else
        {
            chatChecker = true;
        }

        godSpeedAiden = true;
    }

    void Update()
    {
        if(playerInRange && chatChecker)
        {
            startChat.SetActive(true);
            Debug.Log("Show up");
        }
        if (autoStart && playerInRange)
        {
            playerController.enabled = false;
            //npcRenderer.enabled = true;
            if (dialogPanel.activeInHierarchy)
            {
                resetText();
            }
            else
            {
                dialogPanel.SetActive(true);
                Debug.Log("Help me");
                StartCoroutine(Typing());
            }
            autoStart = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            playerController.enabled = false;
            npcRenderer.enabled = true;
            godSpeedAiden = false;
            if (dialogPanel.activeInHierarchy)
            {
                resetText();
            }
            else
            {

                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (NPCNames.Length == 0)
        {
            if (firstNPCdialog.text == NPCScript[index])
            {
                continueText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("Enter key was pressed");
                    nextLine();
                }
            }
        }
        else
        {
            if (firstNPCdialog.text == NPCScript[index] && NPCNameDialog.text == NPCNames[index])
            {
                continueText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Return))
                {
                Debug.Log("Enter key was pressed");
                nextLine();
            }
            }
        }

        if(!godSpeedAiden)
        {
            startChat.SetActive(false);
        }

        if (chatDone && countDownDialog != null)
        {
            if (countDownDialog.chatDone)
            {
                StartCoroutine(FadeIn());
            }
        }

        SameSceneFade();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range of NPC");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void resetText()
    {
        firstNPCdialog.text = "";
        NPCNameDialog.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        playerController.enabled = true;
    }

    IEnumerator Typing()
    {
        if (NPCNames.Length != 0)
        {
            foreach (char letter in NPCNames[index].ToCharArray())
            {
                NPCNameDialog.text += letter;
            }
        }
        foreach (char letter in NPCScript[index].ToCharArray())
        {
            firstNPCdialog.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void nextLine()
    {
        if (index < (NPCScript.Length - 1))
        {
            continueText.gameObject.SetActive(false);
            index++;
            firstNPCdialog.text = "";
            if (NPCNames.Length != 0)
            {
                NPCNameDialog.text = "";
            }
            StartCoroutine(Typing());
        }
        else
        {
            resetText();
            chatDone = true;
            if (goToScene != "")
            {
                if (soundRoutine != null) {
                    StopCoroutine(soundRoutine);
                    soundRoutine = null;
                }
                StartCoroutine(sceneScript.changeScenes(goToScene));
            }
            else
            {
                StartCoroutine(FadeOut());
            }
            //Debug.Log("Got through coroutine");
        }

    }

    public IEnumerator FadeOut()
    {
        fadeIn = true;
        fadeOut = false;
        yield return new WaitForSeconds(2);
    }

    public IEnumerator FadeIn()
    {
        fadeIn = false;
        fadeOut = true;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    public void SameSceneFade()
    {
        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                    if (hidingSpot != null)
                        hidingSpot.TurnOffAndHide();
                    enabled = false;
                }
            }
        }

        if (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }
                GameManager.instance.startTimer = true;
            }
        }
    }
}
