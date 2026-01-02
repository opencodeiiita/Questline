using UnityEngine;
using System.Collections.Generic;

namespace Questline.Quest
{
    public class SampleQuestSetup : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;

        public QuestData CreateQuest1_TheBeginning()
        {
            var quest = ScriptableObject.CreateInstance<QuestData>();
            quest.questId = "quest_the_beginning";
            quest.questTitle = "The Beginning";
            quest.questDescription = "Your journey starts here. Find your way to the village entrance.";
            quest.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    objectiveDescription = "Reach the village entrance",
                    objectiveType = QuestObjectiveType.ReachLocation,
                    targetId = "village_entrance",
                    requiredAmount = 1
                }
            };
            quest.chainedQuests = new List<string> { "quest_gather_supplies" };
            return quest;
        }

        public QuestData CreateQuest2_GatherSupplies()
        {
            var quest = ScriptableObject.CreateInstance<QuestData>();
            quest.questId = "quest_gather_supplies";
            quest.questTitle = "Gather Supplies";
            quest.questDescription = "The merchant needs help. Collect herbs and speak with the merchant.";
            quest.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    objectiveDescription = "Collect 3 healing herbs",
                    objectiveType = QuestObjectiveType.CollectItems,
                    targetId = "healing_herb",
                    requiredAmount = 3
                },
                new QuestObjective
                {
                    objectiveDescription = "Talk to the merchant",
                    objectiveType = QuestObjectiveType.InteractWithNPC,
                    targetId = "merchant_npc",
                    requiredAmount = 1
                }
            };
            quest.chainedQuests = new List<string> { "quest_the_hunt" };
            return quest;
        }

        public QuestData CreateQuest3_TheHunt()
        {
            var quest = ScriptableObject.CreateInstance<QuestData>();
            quest.questId = "quest_the_hunt";
            quest.questTitle = "The Hunt";
            quest.questDescription = "Wolves threaten the village. Hunt them down and return.";
            quest.objectives = new List<QuestObjective>
            {
                new QuestObjective
                {
                    objectiveDescription = "Defeat 5 wolves",
                    objectiveType = QuestObjectiveType.DefeatEnemies,
                    targetId = "wolf_enemy",
                    requiredAmount = 5
                },
                new QuestObjective
                {
                    objectiveDescription = "Return to the village",
                    objectiveType = QuestObjectiveType.ReachLocation,
                    targetId = "village_center",
                    requiredAmount = 1
                }
            };
            quest.chainedQuests = new List<string>();
            return quest;
        }
    }
}