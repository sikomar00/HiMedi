using UnityEngine;

public class GuideWithArrowBtn : MonoBehaviour
{
    public GameObject[] guidePanels; // 가이드 패널 배열
    private int currentPanelIndex = 0;

    private void Start()
    {
        ShowCurrentGuidePanel();
    }

    public void ShowNextPanel()
    {
        currentPanelIndex = Mathf.Min(currentPanelIndex + 1, guidePanels.Length - 1);
        ShowCurrentGuidePanel();
    }

    public void ShowPreviousPanel()
    {
        currentPanelIndex = Mathf.Max(currentPanelIndex - 1, 0);
        ShowCurrentGuidePanel();
    }

    private void ShowCurrentGuidePanel()
    {
        for (int i = 0; i < guidePanels.Length; i++)
        {
            guidePanels[i].SetActive(i == currentPanelIndex);
        }
    }
}
