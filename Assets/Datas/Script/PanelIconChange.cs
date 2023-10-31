using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelIconChange : MonoBehaviour
{
    public GameObject panel; // �ν����Ϳ��� �г��� �Ҵ��մϴ�.
    public Button openButton; // �ν����Ϳ��� �г��� Ȱ��ȭ�ϴ� ��ư�� �Ҵ��մϴ�.
    public Button closeButton; // �г� ���ο��� �г��� �ݴ� ��ư�� �Ҵ��մϴ�.
    public Image buttonImage; // ��ư�� �̹����� ������ Image ������Ʈ�� �Ҵ��մϴ�.
    public Sprite openSprite; // �г��� ���� ������ ��ư �̹���
    public Sprite closeSprite; // �г��� ���� ������ ��ư �̹���

    private void Start()
    {
        // �ʱ� ���¸� �����մϴ�.
        if (panel != null)
        {
            panel.SetActive(false); // ���� �� �г��� ��Ȱ��ȭ�մϴ�.
        }
        if (openButton != null)
        {
            openButton.onClick.AddListener(OpenPanel); // ��ư Ŭ�� �̺�Ʈ�� OpenPanel �Լ��� �����մϴ�.
        }
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePanel); // ��ư Ŭ�� �̺�Ʈ�� ClosePanel �Լ��� �����մϴ�.
        }
    }

    // �г��� Ȱ��ȭ�ϴ� �Լ�
    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // �г��� Ȱ��ȭ�մϴ�.

            if (buttonImage != null)
            {
                buttonImage.sprite = closeSprite; // ��ư �̹����� �г��� ���� ������ �̹����� �����մϴ�.
            }
        }
    }

    // �г��� �ݴ� �Լ�
    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // �г��� ��Ȱ��ȭ�մϴ�.

            if (buttonImage != null)
            {
                buttonImage.sprite = openSprite; // ��ư �̹����� �г��� ���� ������ �̹����� �����մϴ�.
            }
        }
    }
}
