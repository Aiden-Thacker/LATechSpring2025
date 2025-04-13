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
    public bool playerInRange;
    

    void Start() {
        dialogPanel.SetActive(false);
        chatButton.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);
        npcRenderer.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange) {
            playerController.enabled = false;
            npcRenderer.enabled = true;
            if (dialogPanel.activeInHierarchy) {
                resetText();
            } else {
                if (chatButton.gameObject.activeInHierarchy) {
                    chatButton.gameObject.SetActive(false);
                }

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
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            Debug.Log("Player in range of NPC");
            chatButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            chatButton.gameObject.SetActive(false);
        }
    }

    public void resetText() {
        firstNPCdialog.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        playerController.enabled = true;

        /*if (chatButton.gameObject.activeInHierarchy) {
            return;
        } else {
            chatButton.gameObject.SetActive(true);
        } */

        // call animation transition here
            // set animation trigger
            // wait for a few seconds
            // load next scene
    }

    IEnumerator Typing() {
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
            StartCoroutine(Typing());
        } else {
            resetText();
        }
    }
}
