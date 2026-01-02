using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Questline.Quest;

namespace Questline.UI
{
    public class QuestLogUI : MonoBehaviour
    {
        [SerializeField] private GameObject questLogPanel;
        [SerializeField] private Transform activeQuestsContainer;
        [SerializeField] private Transform completedQuestsContainer;
        [SerializeField] private GameObject questEntryPrefab;
        [SerializeField] private KeyCode toggleKey = KeyCode.J;

        private bool isOpen = false;
        private List<GameObject> questEntries = new List<GameObject>();

        private void OnEnable()
        {
            QuestManager.OnQuestStarted += RefreshQuestLog;
            QuestManager.OnQuestCompleted += RefreshQuestLog;
            QuestManager.OnQuestUpdated += RefreshQuestLog;
        }

        private void OnDisable()
        {
            QuestManager.OnQuestStarted -= RefreshQuestLog;
            QuestManager.OnQuestCompleted -= RefreshQuestLog;
            QuestManager.OnQuestUpdated -= RefreshQuestLog;
        }

        private void Start()
        {
            questLogPanel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                ToggleQuestLog();
            }
        }

        public void ToggleQuestLog()
        {
            isOpen = !isOpen;
            questLogPanel.SetActive(isOpen);
            if (isOpen)
            {
                RefreshQuestLog(null);
            }
        }

        private void RefreshQuestLog(QuestData quest)
        {
            ClearEntries();

            if (QuestManager.Instance == null) return;

            var activeQuests = QuestManager.Instance.GetActiveQuests();
            foreach (var activeQuest in activeQuests)
            {
                CreateQuestEntry(activeQuest, activeQuestsContainer, false);
            }
        }

        private void CreateQuestEntry(QuestData quest, Transform container, bool isCompleted)
        {
            if (questEntryPrefab == null) return;

            GameObject entry = Instantiate(questEntryPrefab, container);
            questEntries.Add(entry);

            var titleText = entry.transform.Find("Title")?.GetComponent<TextMeshProUGUI>();
            var descText = entry.transform.Find("Description")?.GetComponent<TextMeshProUGUI>();
            var progressText = entry.transform.Find("Progress")?.GetComponent<TextMeshProUGUI>();

            if (titleText != null) titleText.text = quest.questTitle;
            if (descText != null) descText.text = quest.questDescription;
            if (progressText != null)
            {
                if (isCompleted)
                {
                    progressText.text = "Completed";
                }
                else
                {
                    progressText.text = $"{quest.GetCompletedObjectivesCount()}/{quest.objectives.Count} objectives";
                }
            }
        }

        private void ClearEntries()
        {
            foreach (var entry in questEntries)
            {
                Destroy(entry);
            }
            questEntries.Clear();
        }
    }
}