using System.Collections.Generic;
using UnityEngine;

namespace Questline.Quest
{
    public enum QuestState
    {
        NotStarted,
        InProgress,
        Completed
    }

    [CreateAssetMenu(fileName = "New Quest", menuName = "Questline/Quest Data")]
    public class QuestData : ScriptableObject
    {
        public string questId;
        public string questTitle;
        [TextArea(3, 5)]
        public string questDescription;
        public List<QuestObjective> objectives = new List<QuestObjective>();
        public List<string> chainedQuests = new List<string>();
        
        [SerializeField] private QuestState currentState = QuestState.NotStarted;
        
        public QuestState CurrentState => currentState;

        public void Initialize()
        {
            currentState = QuestState.NotStarted;
            foreach (var objective in objectives)
            {
                objective.currentAmount = 0;
                objective.isCompleted = false;
            }
        }

        public void SetState(QuestState newState)
        {
            currentState = newState;
        }

        public float GetOverallProgress()
        {
            if (objectives.Count == 0) return 0f;
            float totalProgress = 0f;
            foreach (var objective in objectives)
            {
                totalProgress += objective.GetProgress();
            }
            return totalProgress / objectives.Count;
        }

        public int GetCompletedObjectivesCount()
        {
            int count = 0;
            foreach (var objective in objectives)
            {
                if (objective.isCompleted) count++;
            }
            return count;
        }
    }
}