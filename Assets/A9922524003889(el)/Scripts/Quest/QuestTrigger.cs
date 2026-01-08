using UnityEngine;

public class QuestTriggerZone : MonoBehaviour
{
    public string questID;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            QuestManager.Instance.StartQuest(questID);
    }
}
