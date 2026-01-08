public class CollectItemObjective : QuestObjective
{
    public int required = 3;
    int count = 0;

    public void Collect()
    {
        count++;
        if (count >= required)
            Complete();
    }
}
