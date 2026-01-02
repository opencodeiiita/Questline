using UnityEngine;

namespace Questline.Quest.ObjectiveTypes
{
    public class ReachLocationObjective : MonoBehaviour
    {
        [SerializeField] private string locationId;
        [SerializeField] private float triggerRadius = 2f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                QuestManager.Instance?.UpdateQuestProgress(QuestObjectiveType.ReachLocation, locationId);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, triggerRadius);
        }
    }
}