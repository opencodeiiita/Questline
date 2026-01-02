using UnityEngine;

namespace Questline.Quest.ObjectiveTypes
{
    public class CollectItemObjective : MonoBehaviour
    {
        [SerializeField] private string itemId;

        public void Collect()
        {
            QuestManager.Instance?.UpdateQuestProgress(QuestObjectiveType.CollectItems, itemId);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }
    }
}