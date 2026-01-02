using UnityEngine;
using UnityEngine.Events;

namespace Questline.Quest
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private string questIdToStart;
        [SerializeField] private bool startQuestOnEnter = true;
        [SerializeField] private bool requireQuestCompleted;
        [SerializeField] private string requiredCompletedQuestId;
        
        public UnityEvent onPlayerEnter;
        public UnityEvent onPlayerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            if (requireQuestCompleted && QuestManager.Instance != null)
            {
                if (!QuestManager.Instance.IsQuestCompleted(requiredCompletedQuestId))
                {
                    return;
                }
            }

            if (startQuestOnEnter && !string.IsNullOrEmpty(questIdToStart))
            {
                QuestManager.Instance?.StartQuest(questIdToStart);
            }

            onPlayerEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onPlayerExit?.Invoke();
            }
        }
    }
}