using UnityEngine;

namespace GameQuestSystem.Objectives
{
    public class NPCInteractionObjective : MonoBehaviour
    {
        [SerializeField] private string npcKey;
        [SerializeField] private float range = 3f;
        [SerializeField] private KeyCode key = KeyCode.E;

        private Transform player;

        private void Start()
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null) player = obj.transform;
        }

        private void Update()
        {
            if (player == null) return;
            if (Vector3.Distance(transform.position, player.position) > range) return;
            if (Input.GetKeyDown(key))
                QuestManager.Instance.UpdateProgress(ObjectiveKind.NPC, npcKey);
        }
    }
}
