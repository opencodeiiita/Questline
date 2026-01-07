using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameQuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        [SerializeField] private List<Quest> questPool = new();

        private Dictionary<string, Quest> runningQuests = new();
        private HashSet<string> finishedQuests = new();

        public static event Action<Quest> QuestStarted;
        public static event Action<Quest> QuestFinished;
        public static event Action<Quest, QuestObjective> ObjectiveFinished;
        public static event Action<Quest> QuestUpdated;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        private void Start()
        {
            foreach (var quest in questPool)
                quest.ResetQuest();
        }

        public void StartQuest(string questId)
        {
            if (runningQuests.ContainsKey(questId) || finishedQuests.Contains(questId)) return;

            Quest baseQuest = questPool.Find(q => q.id == questId);
            if (baseQuest == null) return;

            Quest questInstance = Instantiate(baseQuest);
            questInstance.SetState(QuestState.Active);
            runningQuests.Add(questId, questInstance);
            QuestStarted?.Invoke(questInstance);
        }

        public void UpdateProgress(ObjectiveKind type, string key, int amount = 1)
        {
            foreach (var quest in runningQuests.Values)
            {
                bool updated = false;

                foreach (var obj in quest.objectives)
                {
                    if (obj.completed) continue;
                    if (obj.type != type || obj.targetKey != key) continue;

                    obj.currentValue += amount;
                    if (obj.currentValue >= obj.requiredValue)
                    {
                        obj.completed = true;
                        ObjectiveFinished?.Invoke(quest, obj);
                    }
                    updated = true;
                }

                if (updated)
                {
                    QuestUpdated?.Invoke(quest);
                    CheckCompletion(quest);
                }
            }
        }

        private void CheckCompletion(Quest quest)
        {
            foreach (var obj in quest.objectives)
                if (!obj.completed) return;

            FinishQuest(quest.id);
        }

        private void FinishQuest(string questId)
        {
            if (!runningQuests.TryGetValue(questId, out Quest quest)) return;

            quest.SetState(QuestState.Finished);
            runningQuests.Remove(questId);
            finishedQuests.Add(questId);
            QuestFinished?.Invoke(quest);

            foreach (var next in quest.followUpQuests)
                StartQuest(next);
        }

        public bool IsCompleted(string questId)
        {
            return finishedQuests.Contains(questId);
        }

        public List<Quest> ActiveQuests()
        {
            return new List<Quest>(runningQuests.Values);
        }
    }
}
