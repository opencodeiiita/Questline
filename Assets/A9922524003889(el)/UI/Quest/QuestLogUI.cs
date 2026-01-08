using UnityEngine;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    public Transform activeContent;
    public Transform completedContent;
    public GameObject questItemPrefab;

    public void Refresh()
    {
        Clear(activeContent);
        Clear(completedContent);

        foreach (var q in QuestManager.Instance.activeQuests)
            Create(activeContent, q.questName);

        foreach (var q in QuestManager.Instance.completedQuests)
            Create(completedContent, q.questName);
    }

    void Create(Transform parent, string txt)
    {
        var obj = Instantiate(questItemPrefab, parent);
        obj.GetComponent<TextMeshProUGUI>().text = txt;
    }

    void Clear(Transform parent)
    {
        foreach (Transform c in parent)
            Destroy(c.gameObject);
    }
}
