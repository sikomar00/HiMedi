using UnityEngine;

public class ABPanels : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;

    // 패널 A를 활성화 시키고 패널 B를 비활성화하는 메서드
    public void ActivatePanelA()
    {
        panelA.SetActive(true);
        panelB.SetActive(false);
    }

    // 패널 B를 활성화 시키고 패널 A를 비활성화하는 메서드
    public void ActivatePanelB()
    {
        panelB.SetActive(true);
        panelA.SetActive(false);
    }
}
