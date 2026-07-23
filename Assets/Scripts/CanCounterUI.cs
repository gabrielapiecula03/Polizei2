using UnityEngine;
using TMPro;

public class CanCounterUI : MonoBehaviour
{
    public TMP_Text cansText;


    private void Start()
    {
        cansText.gameObject.SetActive(false);
    }


    private void Update()
    {
        cansText.text = "Cans: "
            + QuestManager.Instance.cansCollected
            + "/"
            + QuestManager.Instance.cansNeeded;
    }


    public void ShowCounter()
    {
        cansText.gameObject.SetActive(true);
    }
}