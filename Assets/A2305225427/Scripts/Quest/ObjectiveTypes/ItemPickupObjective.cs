using UnityEngine;

namespace GameQuestSystem.Objectives
{
    public class ItemPickupObjective : MonoBehaviour
    {
        [SerializeField] private string itemKey;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            QuestManager.Instance.UpdateProgress(ObjectiveKind.Item, itemKey);
            Destroy(gameObject);
        }
    }
}
