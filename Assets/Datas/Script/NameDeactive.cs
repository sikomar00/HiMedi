using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDeactive : MonoBehaviour
{
    // 이름을 저장할 static 변수
    public static string playerName = "";

    // 이름 입력 패널의 참조
    public GameObject nameInputPanel;

    private void Start()
    {
        // 만약 playerName이 이미 설정되었다면, 패널을 비활성화
        if (!string.IsNullOrEmpty(playerName))
        {
            nameInputPanel.SetActive(false);
        }
    }

    // 이름이 입력되고, '확인' 버튼이 눌렸을 때 호출될 메서드
    public void SetPlayerName(string name)
    {
        playerName = name;
        nameInputPanel.SetActive(false);
    }
}