using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelIconChange : MonoBehaviour
{
    public GameObject panel; // 인스펙터에서 패널을 할당합니다.
    public Button openButton; // 인스펙터에서 패널을 활성화하는 버튼을 할당합니다.
    public Button closeButton; // 패널 내부에서 패널을 닫는 버튼을 할당합니다.
    public Image buttonImage; // 버튼의 이미지를 관리할 Image 컴포넌트를 할당합니다.
    public Sprite openSprite; // 패널이 열린 상태의 버튼 이미지
    public Sprite closeSprite; // 패널이 닫힌 상태의 버튼 이미지

    private void Start()
    {
        // 초기 상태를 설정합니다.
        if (panel != null)
        {
            panel.SetActive(false); // 시작 시 패널을 비활성화합니다.
        }
        if (openButton != null)
        {
            openButton.onClick.AddListener(OpenPanel); // 버튼 클릭 이벤트에 OpenPanel 함수를 연결합니다.
        }
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePanel); // 버튼 클릭 이벤트에 ClosePanel 함수를 연결합니다.
        }
    }

    // 패널을 활성화하는 함수
    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // 패널을 활성화합니다.

            if (buttonImage != null)
            {
                buttonImage.sprite = closeSprite; // 버튼 이미지를 패널이 열린 상태의 이미지로 변경합니다.
            }
        }
    }

    // 패널을 닫는 함수
    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // 패널을 비활성화합니다.

            if (buttonImage != null)
            {
                buttonImage.sprite = openSprite; // 버튼 이미지를 패널이 닫힌 상태의 이미지로 변경합니다.
            }
        }
    }
}
