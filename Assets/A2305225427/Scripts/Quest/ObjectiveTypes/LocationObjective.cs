using UnityEngine;

namespace GameQuestSystem.Objectives
{
    public class LocationObjective : MonoBehaviour
    {
        [SerializeField] private string locationKey;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            QuestManager.Instance.UpdateProgress(ObjectiveKind.Location, locationKey);
        }
    }
}
