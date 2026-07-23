using UnityEngine;
using TMPro;
using System.Collections;

public class NpcDialogue : MonoBehaviour
{
    public GameObject interactText;
    public GameObject dialogueBox;

    public TMP_Text speakerText;
    public TMP_Text dialogueText;

    public CanCounterUI canCounterUI;

    public DialogueLine[] dialogueLines;

    public float typingSpeed = 0.03f;

    private bool playerNearby = false;
    private bool dialogueActive = false;

    private int currentLine = 0;
    private bool isTyping = false;

    private Coroutine typingCoroutine;


    void Start()
    {
        interactText.SetActive(false);
        dialogueBox.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            interactText.SetActive(false);
            dialogueBox.SetActive(false);

            dialogueActive = false;
            currentLine = 0;
        }
    }


    public void Interact()
    {
        if (!dialogueActive)
        {
            StartDialogue();
        }
        else
        {
            NextLine();
        }
    }


    void StartDialogue()
    {
        dialogueActive = true;

        interactText.SetActive(false);
        dialogueBox.SetActive(true);

        currentLine = 0;

        ShowLine();
    }


    void NextLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);

            dialogueText.text = dialogueLines[currentLine].text;

            isTyping = false;
            return;
        }


        currentLine++;

        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }


    void ShowLine()
    {
        string speaker = dialogueLines[currentLine].speaker;

        speakerText.text = speaker;


        if (speaker == "YOU")
        {
            speakerText.color = Color.cyan;
        }
        else if (speaker == "NPC")
        {
            speakerText.color = Color.yellow;
        }


        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLine].text));
    }


    IEnumerator TypeLine(string line)
    {
        isTyping = true;

        dialogueText.text = "";

        foreach (char letter in line)
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }


    void EndDialogue()
    {
        dialogueActive = false;

        dialogueBox.SetActive(false);

        currentLine = 0;


        if (canCounterUI != null)
        {
            canCounterUI.ShowCounter();
        }
    }
}