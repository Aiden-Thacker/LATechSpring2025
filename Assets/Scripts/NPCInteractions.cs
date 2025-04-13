using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class NPCInteractions : MonoBehaviour {
    public GameObject dialogPanel;
    public TextMeshProUGUI firstNPCdialog;
    public TextMeshProUGUI NPCNameDialog;
    public SpriteRenderer npcRenderer;
    public string[] NPCNames;
    public string[] NPCScript;                      // each index holds a string of text, can add different sentences to different indexes
    private int index;
    public PlayerController playerController;

    public Button chatButton;
    public TextMeshProUGUI continueText;
    public float wordSpeed;
    public bool autoStart;
    public bool playerInRange;
    public bool chatChecker;
    public bool turnOffNPCRenderer;
    public string goToScene;
    public ChangeScenes sceneScript;

    public CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOut = false;
    public float timeToFade; 
    public CutScene hidingSpot;
    

    void Start() {
        dialogPanel.SetActive(false);
        chatButton.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);
        if(turnOffNPCRenderer)
        {
            npcRenderer.enabled = false;
        }else
        {
            npcRenderer.enabled = true;
        }
    }

    void Update() {
        if(autoStart && playerInRange)
        {
            chatButton.gameObject.SetActive(false);
            playerController.enabled = false;
            //npcRenderer.enabled = true;
            if (dialogPanel.activeInHierarchy) {
                resetText();
            } else {
                chatChecker = false;

                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
            autoStart = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerInRange) {
            playerController.enabled = false;
            npcRenderer.enabled = true;
            if (dialogPanel.activeInHierarchy) {
                resetText();
            } else {
                chatChecker = false;

                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if(NPCNames.Length == 0)
        {
            if (firstNPCdialog.text == NPCScript[index]) {
                continueText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Return)) {
                    Debug.Log("Enter key was pressed");
                    nextLine();
                }
            }
        }else 
        {
            if (firstNPCdialog.text == NPCScript[index] && NPCNameDialog.text == NPCNames[index]) {
                continueText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return)) {
                Debug.Log("Enter key was pressed");
                nextLine();
            }
            }
        }

        if(chatButton.gameObject.activeInHierarchy && chatChecker == false)
        {
            chatButton.gameObject.SetActive(false);
        }

        SameSceneFade();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            Debug.Log("Player in range of NPC");
            chatButton.gameObject.SetActive(true);
            chatChecker = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            chatChecker = false;
        }
    }

    public void resetText() {
        firstNPCdialog.text = "";
        NPCNameDialog.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        playerController.enabled = true;
    }

    IEnumerator Typing() {
        if(NPCNames.Length != 0)
        {
            foreach (char letter in NPCNames[index].ToCharArray())
            {
                NPCNameDialog.text += letter;
            }
        }
        foreach (char letter in NPCScript[index].ToCharArray()) {
            firstNPCdialog.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void nextLine() {
        if (index < (NPCScript.Length - 1)) {
            continueText.gameObject.SetActive(false);
            index++;
            firstNPCdialog.text = "";
            if(NPCNames.Length != 0)
            {
                NPCNameDialog.text = "";
            }
            StartCoroutine(Typing());
        } else {
            resetText();
            Debug.Log("Starting coroutine");
            if(goToScene != "")
            {
                StartCoroutine(sceneScript.changeScenes(goToScene));
            }else
            {
                StartCoroutine(Fade());
            }
                
            Debug.Log("Got through coroutine");
        }
    }

    public IEnumerator Fade() {
        fadeIn = true;
        fadeOut = false;
        SameSceneFade();
        yield return new WaitForSeconds(2);
        fadeIn = false;
        fadeOut = true;
        SameSceneFade();
        yield return new WaitForSeconds(2);
    }

    public void SameSceneFade()
    {
        if (fadeIn) {
            if (canvasGroup.alpha < 1) {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1) {
                    fadeIn = false;
                    if(hidingSpot != null)
                        hidingSpot.TurnOffAndHide();
                }
            }
        }

        if (fadeOut) {
            if (canvasGroup.alpha >= 0) {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0) {
                    fadeOut = false;
                }
            }
        }
    }
}
