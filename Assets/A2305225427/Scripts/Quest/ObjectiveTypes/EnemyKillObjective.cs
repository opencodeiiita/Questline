using UnityEngine;

namespace GameQuestSystem.Objectives
{
    public class EnemyKillObjective : MonoBehaviour
    {
        [SerializeField] private string enemyKey;

        public void OnEnemyKilled()
        {
            QuestManager.Instance.UpdateProgress(ObjectiveKind.Enemy, enemyKey);
        }
    }
}
