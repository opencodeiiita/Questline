using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Questline.Quest;

namespace Questline.UI
{
    public class QuestNotificationUI : MonoBehaviour
    {
        [SerializeField] private GameObject notificationPanel;
        [SerializeField] private TextMeshProUGUI notificationText;
        [SerializeField] private float displayDuration = 3f;
        [SerializeField] private float fadeSpeed = 2f;

        private CanvasGroup canvasGroup;
        private Coroutine currentNotification;

        private void Awake()
        {
            canvasGroup = notificationPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = notificationPanel.AddComponent<CanvasGroup>();
            }
            notificationPanel.SetActive(false);
        }

        private void OnEnable()
        {
            QuestManager.OnQuestStarted += HandleQuestStarted;
            QuestManager.OnQuestCompleted += HandleQuestCompleted;
            QuestManager.OnObjectiveCompleted += HandleObjectiveCompleted;
        }

        private void OnDisable()
        {
            QuestManager.OnQuestStarted -= HandleQuestStarted;
            QuestManager.OnQuestCompleted -= HandleQuestCompleted;
            QuestManager.OnObjectiveCompleted -= HandleObjectiveCompleted;
        }

        private void HandleQuestStarted(QuestData quest)
        {
            ShowNotification($"Quest Accepted: {quest.questTitle}");
        }

        private void HandleQuestCompleted(QuestData quest)
        {
            ShowNotification($"Quest Completed: {quest.questTitle}");
        }

        private void HandleObjectiveCompleted(QuestData quest, QuestObjective objective)
        {
            ShowNotification($"Objective Complete: {objective.objectiveDescription}");
        }

        public void ShowNotification(string message)
        {
            if (currentNotification != null)
            {
                StopCoroutine(currentNotification);
            }
            currentNotification = StartCoroutine(DisplayNotification(message));
        }

        private IEnumerator DisplayNotification(string message)
        {
            notificationText.text = message;
            notificationPanel.SetActive(true);
            canvasGroup.alpha = 0f;

            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Time.deltaTime * fadeSpeed;
                yield return null;
            }

            yield return new WaitForSeconds(displayDuration);

            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
                yield return null;
            }

            notificationPanel.SetActive(false);
        }
    }
}