using UnityEngine;

public class ReachLocationObjective : QuestObjective
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Complete();
    }
}
