using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputPanel : MonoBehaviour
{
    public InputField nameInputField;
    public GameObject namePanel;
    public GameObject titleNameText;
    public GameObject warningText;

    public GameObject editNamePanel;
    public InputField editNameInputField;

    void Start()
    {
        // 경고 텍스트와 수정 패널을 초기화합니다.
        warningText.SetActive(false);
        editNamePanel.SetActive(false);

        // 이전에 저장된 이름을 Placeholder로 설정합니다.
        Text placeholderText = editNameInputField.placeholder as Text;
        if (placeholderText != null)
        {
            string storedName = PlayerPrefs.GetString("PlayerName", "");
            if (!string.IsNullOrEmpty(storedName))
            {
                placeholderText.text = storedName;
            }
        }

        // 이름 입력 필드 변경 이벤트 리스너를 추가합니다.
        nameInputField.onValueChanged.AddListener(delegate { HideWarningText(); });
        editNameInputField.onValueChanged.AddListener(delegate { HideWarningText(); });
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
            titleNameText.GetComponent<Text>().text = firstName + honorific + ", 반가워!";

            // 패널 비활성화
            namePanel.SetActive(false);
        }
    }

    // 이름 입력 필드가 변경될 때 호출될 메서드입니다.
    private void HideWarningText()
    {
        if (warningText.activeSelf)
        {
            warningText.SetActive(false);
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

    public void OnEditNameButtonClicked()
    {
        // 수정 패널을 활성화
        editNamePanel.SetActive(true);

        // 저장된 이름을 editNameInputField의 Placeholder로 표시
        string storedName = PlayerPrefs.GetString("PlayerName", "");
        Text placeholderText = editNameInputField.placeholder as Text; // Legacy UI 사용 시
        // TMP_Text placeholderText = editNameInputField.placeholder as TMP_Text; // TextMeshPro 사용 시
        placeholderText.text = storedName; // Placeholder에 저장된 이름을 설정

        // editNameInputField의 text를 비워 사용자 입력을 받을 준비
        editNameInputField.text = "";
    }

    // 이름 저장 버튼을 눌렀을 때 호출되는 메서드
    public void OnSaveEditedNameButtonClicked()
    {
        string newName = editNameInputField.text;
        if (!string.IsNullOrEmpty(newName) && newName.Length >= 2)
        {
            // 새 이름을 저장
            PlayerPrefs.SetString("PlayerName", newName);
            string firstName = newName.Substring(1);
            string honorific = DetermineHonorific(firstName);
            titleNameText.GetComponent<Text>().text = firstName + honorific + ", 반가워!";

            // 수정 패널을 비활성화
            editNamePanel.SetActive(false);
        }
        else
        {
            // 적절하지 않은 이름에 대한 경고 표시
            warningText.SetActive(true);
        }
    }

    // 이름 수정 취소 버튼을 눌렀을 때 호출되는 메서드
    public void OnCancelEditButtonClicked()
    {
        // 수정 패널을 비활성화
        editNamePanel.SetActive(false);
    }
}
