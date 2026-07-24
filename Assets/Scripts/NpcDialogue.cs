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

    public GameObject winScreen;

    public ParticleSystem winParticles;

    public DialogueLine[] questDialogueLines;
    public DialogueLine[] finishDialogueLines;

    public float typingSpeed = 0.03f;

    private bool playerNearby = false;
    private bool dialogueActive = false;

    private int currentLine = 0;
    private bool isTyping = false;

    private bool showingFinishDialogue = false;

    private Coroutine typingCoroutine;


    private void Awake()
    {
        if (winParticles != null)
        {
            var main = winParticles.main;
            main.useUnscaledTime = true;
        }
    }


    void Start()
    {
        interactText.SetActive(false);
        dialogueBox.SetActive(false);

        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }

        if (winParticles != null)
        {
            winParticles.Stop();
        }
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
            if (QuestManager.Instance.HasEnoughCans())
            {
                showingFinishDialogue = true;
            }
            else
            {
                showingFinishDialogue = false;
            }

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

            if (showingFinishDialogue)
            {
                dialogueText.text = finishDialogueLines[currentLine].text;
            }
            else
            {
                dialogueText.text = questDialogueLines[currentLine].text;
            }

            isTyping = false;
            return;
        }


        currentLine++;


        if (showingFinishDialogue)
        {
            if (currentLine >= finishDialogueLines.Length)
            {
                EndDialogue();
                return;
            }
        }
        else
        {
            if (currentLine >= questDialogueLines.Length)
            {
                EndDialogue();
                return;
            }
        }


        ShowLine();
    }


    void ShowLine()
    {
        DialogueLine line;


        if (showingFinishDialogue)
        {
            line = finishDialogueLines[currentLine];
        }
        else
        {
            line = questDialogueLines[currentLine];
        }


        speakerText.text = line.speaker;


        if (line.speaker == "YOU")
        {
            speakerText.color = Color.cyan;
        }
        else if (line.speaker == "NPC")
        {
            speakerText.color = Color.yellow;
        }


        typingCoroutine = StartCoroutine(TypeLine(line.text));
    }


    IEnumerator TypeLine(string line)
    {
        isTyping = true;

        dialogueText.text = "";


        foreach (char letter in line)
        {
            dialogueText.text += letter;

            yield return new WaitForSecondsRealtime(typingSpeed);
        }


        isTyping = false;
    }


    void EndDialogue()
    {
        dialogueActive = false;

        dialogueBox.SetActive(false);

        currentLine = 0;


        if (showingFinishDialogue)
        {
            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }


            if (winParticles != null)
            {
                winParticles.Play();
            }


            //Time.timeScale = 0f;
        }
        else
        {
            if (canCounterUI != null)
            {
                canCounterUI.ShowCounter();
            }
        }
    }
}