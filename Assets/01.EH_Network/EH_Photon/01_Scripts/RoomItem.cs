using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomInfo;

    // Ŭ�� �Ǿ��� �� ȣ�� ���� �Լ��� ���� ����
    public Action<string> onChangeRoomName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, int maxPlayer)
    {
        //���� ���ӿ����� �̸��� ���̸��� ����
        name = roomName;

        //�� ������ Text �� ���� 
        //�� �̸� ( 5 / 10 )
        roomInfo.text = roomName + " ( " + currPlayer + " / " + maxPlayer + " )";
    }

    public void OnClick()
    {
        //onChangeRoomName �� null �� �ƴ϶��
        if(onChangeRoomName != null)
        {
            onChangeRoomName(name);
        }


        ////1. InputRoomName ���ӿ�����Ʈ ã��.
        //GameObject go = GameObject.Find("InputRoomName");
        ////2. ã�� ���ӿ�����Ʈ���� InputField ������ ��������
        //InputField inputField = go.GetComponent<InputField>();
        ////3. ������ ������Ʈ�� �̿��ؼ� Text �� ����
        //inputField.text = name;
    }
}
