using System.Collections.Generic;
using UnityEngine;

namespace GameQuestSystem
{
    public enum QuestState
    {
        NotStarted,
        Active,
        Finished
    }

    [CreateAssetMenu(menuName = "GameQuest/Quest")]
    public class Quest : ScriptableObject
    {
        public string id;
        public string title;
        [TextArea(3, 6)] public string description;
        public List<QuestObjective> objectives = new();
        public List<string> followUpQuests = new();

        [SerializeField] private QuestState state = QuestState.NotStarted;

        public QuestState State => state;

        public void ResetQuest()
        {
            state = QuestState.NotStarted;
            foreach (var obj in objectives)
            {
                obj.currentValue = 0;
                obj.completed = false;
            }
        }

        public void SetState(QuestState newState)
        {
            state = newState;
        }

        public int CompletedCount()
        {
            int count = 0;
            foreach (var obj in objectives)
                if (obj.completed) count++;
            return count;
        }
    }
}
