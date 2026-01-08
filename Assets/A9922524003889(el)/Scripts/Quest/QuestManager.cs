using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> allQuests;
    public List<Quest> activeQuests = new();
    public List<Quest> completedQuests = new();

    public QuestUIManager ui;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartQuest(string questID)
    {
        Quest q = allQuests.Find(x => x.questID == questID);
        if (q == null || q.state != QuestState.NotStarted) return;

        q.StartQuest();
        activeQuests.Add(q);

        ui.Show("Quest Started: " + q.questName);
        ui.RefreshLog();
    }

    public void OnObjectiveCompleted(QuestObjective obj)
    {
        foreach (var q in activeQuests)
        {
            if (q.CurrentObjective == obj)
            {
                q.CompleteObjective();
                ui.Show("Objective Updated");

                if (q.state == QuestState.Completed)
                    CompleteQuest(q);

                ui.RefreshLog();
                return;
            }
        }
    }

    void CompleteQuest(Quest q)
    {
        activeQuests.Remove(q);
        completedQuests.Add(q);

        ui.Show("Quest Completed: " + q.questName);

        if (!string.IsNullOrEmpty(q.nextQuestID))
            StartQuest(q.nextQuestID);
    }
}
