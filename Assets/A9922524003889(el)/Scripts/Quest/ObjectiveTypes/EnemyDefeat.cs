public class DefeatEnemyObjective : QuestObjective
{
    public int killsRequired = 2;
    int kills = 0;

    public void OnEnemyKilled()
    {
        kills++;
        if (kills >= killsRequired)
            Complete();
    }
}
