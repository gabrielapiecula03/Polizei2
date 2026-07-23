using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public int cansCollected = 0;
    public int cansNeeded = 5;

    private void Awake()
    {
        Instance = this;
    }

    public void CollectCan()
    {
        cansCollected++;
        Debug.Log("Cans: " + cansCollected + "/" + cansNeeded);
    }

    public bool HasEnoughCans()
    {
        return cansCollected >= cansNeeded;
    }
}