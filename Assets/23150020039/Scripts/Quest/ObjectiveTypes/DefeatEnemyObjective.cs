using UnityEngine;

namespace Questline.Quest.ObjectiveTypes
{
    public class DefeatEnemyObjective : MonoBehaviour
    {
        [SerializeField] private string enemyTypeId;

        public void OnDefeated()
        {
            QuestManager.Instance?.UpdateQuestProgress(QuestObjectiveType.DefeatEnemies, enemyTypeId);
        }
    }
}