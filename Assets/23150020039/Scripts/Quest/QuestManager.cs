using System.Collections.Generic;
using UnityEngine;
using System;

namespace Questline.Quest
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }
        
        [SerializeField] private List<QuestData> availableQuests = new List<QuestData>();
        
        private Dictionary<string, QuestData> activeQuests = new Dictionary<string, QuestData>();
        private List<string> completedQuests = new List<string>();
        
        public static event Action<QuestData> OnQuestStarted;
        public static event Action<QuestData> OnQuestCompleted;
        public static event Action<QuestData, QuestObjective> OnObjectiveCompleted;
        public static event Action<QuestData> OnQuestUpdated;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            foreach (var quest in availableQuests)
            {
                if (quest != null)
                {
                    quest.Initialize();
                }
            }
        }

        public bool StartQuest(string questId)
        {
            var questData = availableQuests.Find(q => q.questId == questId);
            if (questData == null) return false;
            if (activeQuests.ContainsKey(questId)) return false;
            if (completedQuests.Contains(questId)) return false;

            var activeQuest = Instantiate(questData);
            activeQuest.Initialize();
            activeQuest.SetState(QuestState.InProgress);
            
            activeQuests.Add(questId, activeQuest);
            OnQuestStarted?.Invoke(activeQuest);
            return true;
        }

        public void UpdateQuestProgress(QuestObjectiveType objectiveType, string targetId, int amount = 1)
        {
            foreach (var kvp in activeQuests)
            {
                var quest = kvp.Value;
                bool questUpdated = false;

                foreach (var objective in quest.objectives)
                {
                    if (objective.objectiveType == objectiveType && 
                        objective.targetId == targetId && 
                        !objective.isCompleted)
                    {
                        objective.currentAmount += amount;
                        
                        if (objective.currentAmount >= objective.requiredAmount)
                        {
                            objective.isCompleted = true;
                            OnObjectiveCompleted?.Invoke(quest, objective);
                        }
                        questUpdated = true;
                    }
                }

                if (questUpdated)
                {
                    OnQuestUpdated?.Invoke(quest);
                    CheckQuestCompletion(quest);
                }
            }
        }

        private void CheckQuestCompletion(QuestData quest)
        {
            foreach (var objective in quest.objectives)
            {
                if (!objective.isCompleted) return;
            }
            CompleteQuest(quest.questId);
        }

        private void CompleteQuest(string questId)
        {
            if (activeQuests.TryGetValue(questId, out QuestData quest))
            {
                quest.SetState(QuestState.Completed);
                activeQuests.Remove(questId);
                completedQuests.Add(questId);
                OnQuestCompleted?.Invoke(quest);

                foreach (string chainedQuestId in quest.chainedQuests)
                {
                    StartQuest(chainedQuestId);
                }
            }
        }

        public List<QuestData> GetActiveQuests()
        {
            return new List<QuestData>(activeQuests.Values);
        }

        public List<string> GetCompletedQuests()
        {
            return new List<string>(completedQuests);
        }

        public bool IsQuestCompleted(string questId)
        {
            return completedQuests.Contains(questId);
        }

        public bool IsQuestActive(string questId)
        {
            return activeQuests.ContainsKey(questId);
        }
    }
}