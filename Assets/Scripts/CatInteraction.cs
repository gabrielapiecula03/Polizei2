using UnityEngine;
using UnityEngine.AI;

public class CatInteraction : MonoBehaviour
{
    public GameObject interactText;
    public GameObject dialogueText;

    private bool playerNearby = false;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        interactText.SetActive(false);
        dialogueText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            interactText.SetActive(true);
            dialogueText.SetActive(false);

            // zatrzymuje kota
            agent.isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            interactText.SetActive(false);
            dialogueText.SetActive(false);

            // kot znowu chodzi
            agent.isStopped = false;
        }
    }

    public void Interact()
    {
        if (playerNearby)
        {
            interactText.SetActive(false);
            dialogueText.SetActive(true);
        }
    }
}