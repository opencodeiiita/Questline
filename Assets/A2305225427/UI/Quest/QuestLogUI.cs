using UnityEngine;
using TMPro;
using GameQuestSystem;

namespace GameQuestUI
{
    public class QuestLogUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI content;
        [SerializeField] private KeyCode toggleKey = KeyCode.J;

        private void Start()
        {
            panel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                panel.SetActive(!panel.activeSelf);
                if (panel.activeSelf) Refresh();
            }
        }

        private void Refresh()
        {
            content.text = "";
            foreach (var quest in QuestManager.Instance.ActiveQuests())
            {
                content.text += quest.title + "\n";
                foreach (var obj in quest.objectives)
                {
                    content.text += "- " + obj.description + "\n";
                }
                content.text += "\n";
            }
        }
    }
}
