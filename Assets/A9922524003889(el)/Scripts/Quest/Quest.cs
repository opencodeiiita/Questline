using System.Collections.Generic;

public enum QuestState
{
    NotStarted,
    InProgress,
    Completed
}

[System.Serializable]
public class Quest
{
    public string questID;
    public string questName;
    public QuestState state = QuestState.NotStarted;

    public List<QuestObjective> objectives;
    public int currentObjectiveIndex = 0;

    public string nextQuestID;

    public QuestObjective CurrentObjective =>
        currentObjectiveIndex < objectives.Count ? objectives[currentObjectiveIndex] : null;

    public bool IsCompleted => currentObjectiveIndex >= objectives.Count;

    public void StartQuest()
    {
        state = QuestState.InProgress;
        CurrentObjective?.StartObjective();
    }

    public void CompleteObjective()
    {
        currentObjectiveIndex++;

        if (!IsCompleted)
            CurrentObjective.StartObjective();
        else
            state = QuestState.Completed;
    }
}
