using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private float typingTime = 0.05f;

    private bool isPlayerInRage;
    private bool didDialogueStart;
    private int lineIndex;

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRage && Input.GetButtonDown("Fire1")){
            if (!didDialogueStart){
                StartDialogue();
            } else  if(dialogueText.text == dialogueLines[lineIndex]){
                NextDialogueLine();
            }
        }
    }

    private void StartDialogue(){
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine(){
        lineIndex++;
        if(lineIndex < dialogueLines.Length){
            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine(){
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex]){
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter(Collider c){
        if(c.gameObject.CompareTag("homero")){
            isPlayerInRage = true;
            dialogueMark.SetActive(true);
            Debug.Log("Se puede iniciar un dialogo");
        }
    }
    private void OnTriggerExit(Collider c){
        if(c.gameObject.CompareTag("homero")){
            isPlayerInRage = false;
            dialogueMark.SetActive(false);
            Debug.Log("No see puede iniciar un dialogo");
        }
    }
}
