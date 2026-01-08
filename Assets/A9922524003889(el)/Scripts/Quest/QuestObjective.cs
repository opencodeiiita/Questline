using UnityEngine;
using System;

namespace QuestSystem
{
    [Serializable]
    public class QuestObjective
    {
        public ObjectiveType type;
        public string description;
        public string targetID; // Unique ID for the target (e.g., "VillageGate", "Goblin")
        
        public int requiredAmount = 1;
        public int currentAmount = 0;

        public bool isCompleted;
        public bool isActive;

        public void ResetObjective()
        {
            isCompleted = false;
            isActive = false;
            currentAmount = 0;
        }

        public void UpdateStatus(bool active)
        {
            isActive = active;
        }

        public void Resolve()
        {
            if (!isActive || isCompleted) return;

            currentAmount++;
            if (currentAmount >= requiredAmount)
            {
                isCompleted = true;
                isActive = false;
            }
        }
    }
}