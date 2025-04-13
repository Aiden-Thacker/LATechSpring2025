using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutSceneCountDown : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI firstNPCdialog;
    public TextMeshProUGUI NPCNameDialog;
    public string[] NPCNames;
    public string[] NPCScript;                      // each index holds a string of text, can add different sentences to different indexes
    private int index;
    public PlayerController playerController;
    public TextMeshProUGUI continueText;
    public float wordSpeed;
    public bool autoStart;
    public bool chatDone;
    public NPCInteractions preConvo;


    void Start()
    {
        dialogPanel.SetActive(false);
        continueText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (preConvo.enabled == false)
        {
            if (!chatDone && autoStart)
            {
                playerController.enabled = false;
                if (dialogPanel.activeInHierarchy)
                {
                    resetText();
                }
                else
                {
                    dialogPanel.SetActive(true);
                    Debug.Log("God help us now");
                    StartCoroutine(Typing());
                }
                autoStart = false;
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
            preConvo.enabled = true;
        }
    }
}
