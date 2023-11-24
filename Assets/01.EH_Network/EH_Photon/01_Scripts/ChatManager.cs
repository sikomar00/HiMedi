using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPun
{
    //InputField 
    public InputField chatInput;

    //ChatItem Prefab
    public GameObject chatItemFactory;
    //ScrollView �� �ִ� Content �� RectTrasform
    public RectTransform rtContent;
    //ScrollView �� RectTransform
    public RectTransform rtScrollView;
    //ä���� �߰��Ǳ� ���� Content H �� ���� ������ �ִ� ����
    float prevContentH;

    //�г��� ����
    public Color nickNameColor;

    void Start()
    {
        //�г��� ���� �����ϰ� ����
        nickNameColor = new Color32(
            (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256), 
            255);

        //����Ű�� ������ InputField �� �ִ� �ؽ�Ʈ ���� �˷��ִ� �Լ� ���
        chatInput.onSubmit.AddListener(OnSubmit);

        //InputField �� ������ ���� �� ������ ȣ�����ִ� �Լ� ���
        chatInput.onValueChanged.AddListener(OnValueChanged);

        //InputField �� Focusing �� ������� �� ȣ�����ִ� �Լ� ���
        chatInput.onEndEdit.AddListener(OnEndEdit);
    }

    void Update()
    {
        
    }

    void OnSubmit(string s)
    {
        //s �� ���̰� 0 �̶�� �Լ��� ������
        if (s.Length == 0) return;

        //���ο� ä���� �߰��Ǳ� ���� content �� H ���� ����
        prevContentH = rtContent.sizeDelta.y;

        //�г����� �ٿ��� ä�ó����� ������
        //"<color=#ffff00> ���ϴ� ���� </color>"
        string chat = "<color=#" + ColorUtility.ToHtmlStringRGB(nickNameColor) + ">" +
            PhotonNetwork.NickName + "</color>" + " : " + s;

        //Rpc �Լ��� ��� ������� ä�� ������ ����
        photonView.RPC(nameof(AddChatRpc), RpcTarget.All, chat);

        //chatInput ���� �ʱ�ȭ
        chatInput.text = "";

        //chatInput �� Ȱ��ȭ ����
        chatInput.ActivateInputField();
    }

    [PunRPC]
    void AddChatRpc(string chat)
    {
        //print("OnSubmit : " + s);
        //Chatitem �� �����.
        GameObject ci = Instantiate(chatItemFactory);
        //������� item �� �θ� content �� �Ѵ�.
        ci.transform.SetParent(rtContent);

        //������� item ���� ChatItem ������Ʈ�� �����´�.
        ChatItem item = ci.GetComponent<ChatItem>();
        //������ ������Ʈ���� SetText �Լ��� ����
        item.SetText(chat);

        //�ڵ����� content �� �� ������ ������ ���
        StartCoroutine(AutoScrollBottom());
    }

    IEnumerator AutoScrollBottom()
    {
        yield return 0;

        //scrollView �� H ���� content �� H ���� ũ�ٸ� (��ũ���� ������ ���¶��)
        if(rtContent.sizeDelta.y > rtScrollView.sizeDelta.y)
        {
            //������ �ٴڿ� ����־��ٸ�
            if(prevContentH - rtScrollView.sizeDelta.y <= rtContent.anchoredPosition.y)
            {
                //content �� y ���� �缳���Ѵ�.
                rtContent.anchoredPosition = 
                    new Vector2(0, rtContent.sizeDelta.y - rtScrollView.sizeDelta.y);
            }
        }
    }
    

    void OnValueChanged(string s)
    {
        //print("OnValueChanged : " + s);
    }

    void OnEndEdit(string s)
    {
        //print("OnEndEdit : " + s);
    }
}
