using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public QuestLogUI questLog;

    public void Show(string msg)
    {
        StopAllCoroutines();
        text.text = msg;
        StartCoroutine(Clear());
    }

    System.Collections.IEnumerator Clear()
    {
        yield return new WaitForSeconds(3);
        text.text = "";
    }

    public void RefreshLog()
    {
        questLog.Refresh();
    }
}
