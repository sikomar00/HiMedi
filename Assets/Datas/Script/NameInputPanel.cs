using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputPanel : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject namePanel;
    public TMP_Text titleNameText;
    public GameObject warningText;

    void Start()
    {
        // warningText 필요한 경우 초기화
        warningText.SetActive(false);
    }

    public void OnSubmitButtonClicked()
    {
        string enteredName = nameInputField.text;

        if (string.IsNullOrEmpty(enteredName) || enteredName.Length < 2) // 이름이 적절하지 않으면 경고를 표시합니다.
        {
            warningText.SetActive(true);
        }
        else
        {
            // 이름 저장
            PlayerPrefs.SetString("PlayerName", enteredName);

            // 성(姓)을 제외한 이름 부분을 추출합니다.
            string firstName = enteredName.Substring(1);

            // 호칭 조사를 결정합니다 ('아' 또는 '야').
            string honorific = DetermineHonorific(firstName);

            // 성을 제외한 이름과 호칭 조사를 사용하여 환영 메시지를 구성합니다.
            titleNameText.text = firstName + honorific + ", 반가워!";

            // 패널 비활성화
            namePanel.SetActive(false);
        }
    }

    // 이름에 맞는 호칭 조사를 결정하는 함수
    private string DetermineHonorific(string name)
    {
        // 이름의 마지막 글자를 가져옵니다
        char lastChar = name[name.Length - 1];

        // 마지막 글자의 유니코드 값을 계산하여 받침(종성) 여부를 판단합니다
        int unicodeValue = lastChar - 0xAC00;
        int finalConsonant = unicodeValue % 28;

        // 받침이 있는 경우 '아', 없는 경우 '야'를 반환합니다
        return (finalConsonant > 0) ? "아" : "야";
    }
}
