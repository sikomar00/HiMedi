using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour
{
    Text chatText;
    RectTransform rt;

    void Awake()
    {
        chatText = GetComponent<Text>();
        rt = GetComponent<RectTransform>();
    }

    public void SetText(string s)
    {
        //�ؽ�Ʈ ����
        chatText.text = s;
        //�ؽ�Ʈ�� ���缭 ũ�⸦ ����
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);
    }
}
