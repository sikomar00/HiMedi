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
        // warningText �ʿ��� ��� �ʱ�ȭ
        warningText.SetActive(false);
    }

    public void OnSubmitButtonClicked()
    {
        string enteredName = nameInputField.text;

        if (string.IsNullOrEmpty(enteredName) || enteredName.Length < 2) // �̸��� �������� ������ ��� ǥ���մϴ�.
        {
            warningText.SetActive(true);
        }
        else
        {
            // �̸� ����
            PlayerPrefs.SetString("PlayerName", enteredName);

            // ��(��)�� ������ �̸� �κ��� �����մϴ�.
            string firstName = enteredName.Substring(1);

            // ȣĪ ���縦 �����մϴ� ('��' �Ǵ� '��').
            string honorific = DetermineHonorific(firstName);

            // ���� ������ �̸��� ȣĪ ���縦 ����Ͽ� ȯ�� �޽����� �����մϴ�.
            titleNameText.text = firstName + honorific + ", �ݰ���!";

            // �г� ��Ȱ��ȭ
            namePanel.SetActive(false);
        }
    }

    // �̸��� �´� ȣĪ ���縦 �����ϴ� �Լ�
    private string DetermineHonorific(string name)
    {
        // �̸��� ������ ���ڸ� �����ɴϴ�
        char lastChar = name[name.Length - 1];

        // ������ ������ �����ڵ� ���� ����Ͽ� ��ħ(����) ���θ� �Ǵ��մϴ�
        int unicodeValue = lastChar - 0xAC00;
        int finalConsonant = unicodeValue % 28;

        // ��ħ�� �ִ� ��� '��', ���� ��� '��'�� ��ȯ�մϴ�
        return (finalConsonant > 0) ? "��" : "��";
    }
}
