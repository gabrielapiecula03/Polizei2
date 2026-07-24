using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public int cansCollected = 0;
    public int cansNeeded = 5;

    public PlayerMessageUI playerMessageUI;


    private void Awake()
    {
        Instance = this;
    }


    public void CollectCan()
    {
        cansCollected++;

        Debug.Log("Cans: " + cansCollected + "/" + cansNeeded);


        if (cansCollected >= cansNeeded)
        {
            playerMessageUI.ShowMessage(
                "Okay, I got them all. Time to bring them to that other guy."
            );
        }
    }


    public bool HasEnoughCans()
    {
        return cansCollected >= cansNeeded;
    }
}