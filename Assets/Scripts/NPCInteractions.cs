using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteractions : MonoBehaviour {
    public GameObject dialogPanel;
    public TextMeshProUGUI firstNPCdialog;
    public string[] NPCScript;                      // each index holds a string of text, can add different sentences to different indexes
    private int index;

    public GameObject continueButton;
    public Button chatButton;
    public float wordSpeed;
    public bool playerInRange;

    void Start() {
        dialogPanel.SetActive(false);
        continueButton.SetActive(false);
        chatButton.gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange) {
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

        if (firstNPCdialog.text == NPCScript[index]) {
            continueButton.SetActive(true);
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
            resetText();
            chatButton.gameObject.SetActive(false);
        }
    }

    public void resetText() {
        firstNPCdialog.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        if (chatButton.gameObject.activeInHierarchy) {
            return;
        } else {
            chatButton.gameObject.SetActive(true);
        }
    }

    IEnumerator Typing() {
        foreach (char letter in NPCScript[index].ToCharArray()) {
            firstNPCdialog.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void nextLine() {
        continueButton.SetActive(false);

        if (index < (NPCScript.Length - 1)) {
            index++;
            firstNPCdialog.text = "";
            StartCoroutine(Typing());
        } else {
            resetText();
        }
    }
}
