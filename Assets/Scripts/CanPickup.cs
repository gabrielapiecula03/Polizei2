using UnityEngine;

public class CanPickup : MonoBehaviour
{
    public GameObject interactText;

    private bool playerNearby = false;


    private void Start()
    {
        interactText.SetActive(false);
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
        }
    }


    public void Interact()
    {
        if (!playerNearby)
            return;


        QuestManager.Instance.CollectCan();

        interactText.SetActive(false);

        Destroy(gameObject);
    }
}