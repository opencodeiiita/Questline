using UnityEngine;
using TMPro;
using System.Collections;
using GameQuestSystem;

namespace GameQuestUI
{
    public class QuestNotificationUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float duration = 3f;

        private CanvasGroup group;
        private Coroutine routine;

        private void Awake()
        {
            group = panel.GetComponent<CanvasGroup>() ?? panel.AddComponent<CanvasGroup>();
            panel.SetActive(false);
        }

        private void OnEnable()
        {
            QuestManager.QuestStarted += q => Show($"Quest Started: {q.title}");
            QuestManager.QuestFinished += q => Show($"Quest Completed: {q.title}");
            QuestManager.ObjectiveFinished += (q, o) => Show(o.description);
        }

        public void Show(string message)
        {
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(Display(message));
        }

        private IEnumerator Display(string msg)
        {
            text.text = msg;
            panel.SetActive(true);
            group.alpha = 1f;
            yield return new WaitForSeconds(duration);
            panel.SetActive(false);
        }
    }
}
