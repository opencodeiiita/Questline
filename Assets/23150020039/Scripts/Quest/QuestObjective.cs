using UnityEngine;

namespace Questline.Quest
{
    public enum QuestObjectiveType
    {
        ReachLocation,
        CollectItems,
        InteractWithNPC,
        DefeatEnemies
    }

    [System.Serializable]
    public class QuestObjective
    {
        public string objectiveDescription;
        public QuestObjectiveType objectiveType;
        public string targetId;
        public int requiredAmount = 1;
        public int currentAmount = 0;
        public bool isCompleted = false;

        public float GetProgress()
        {
            return requiredAmount > 0 ? (float)currentAmount / requiredAmount : 0f;
        }

        public string GetProgressText()
        {
            return $"{currentAmount}/{requiredAmount}";
        }
    }
}