using UnityEngine;

namespace GameQuestSystem
{
    public enum ObjectiveKind
    {
        Location,
        Item,
        NPC,
        Enemy
    }

    [System.Serializable]
    public class QuestObjective
    {
        public string description;
        public ObjectiveKind type;
        public string targetKey;
        public int requiredValue = 1;
        public int currentValue;
        public bool completed;

        public float Progress()
        {
            return requiredValue == 0 ? 0f : (float)currentValue / requiredValue;
        }
    }
}
