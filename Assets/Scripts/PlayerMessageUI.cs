using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerMessageUI : MonoBehaviour
{
    public TMP_Text messageText;

    public float displayTime = 5f;

    private Coroutine messageCoroutine;


    private void Start()
    {
        messageText.gameObject.SetActive(false);
    }


    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);


        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(HideMessage());
    }


    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(displayTime);

        messageText.gameObject.SetActive(false);
    }
}