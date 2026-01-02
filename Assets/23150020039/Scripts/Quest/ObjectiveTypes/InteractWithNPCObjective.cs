using UnityEngine;

namespace Questline.Quest.ObjectiveTypes
{
    public class InteractWithNPCObjective : MonoBehaviour
    {
        [SerializeField] private string npcId;
        [SerializeField] private KeyCode interactKey = KeyCode.E;
        [SerializeField] private float interactRange = 3f;
        
        private Transform player;
        private bool playerInRange = false;

        private void Start()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        private void Update()
        {
            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);
            playerInRange = distance <= interactRange;

            if (playerInRange && Input.GetKeyDown(interactKey))
            {
                Interact();
            }
        }

        public void Interact()
        {
            QuestManager.Instance?.UpdateQuestProgress(QuestObjectiveType.InteractWithNPC, npcId);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }
}