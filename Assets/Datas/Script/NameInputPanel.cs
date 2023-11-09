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
        // ��� �ؽ�Ʈ�� ���� �г��� �ʱ�ȭ�մϴ�.
        warningText.SetActive(false);
        editNamePanel.SetActive(false);

        // ������ ����� �̸��� Placeholder�� �����մϴ�.
        Text placeholderText = editNameInputField.placeholder as Text;
        if (placeholderText != null)
        {
            string storedName = PlayerPrefs.GetString("PlayerName", "");
            if (!string.IsNullOrEmpty(storedName))
            {
                placeholderText.text = storedName;
            }
        }

        // �̸� �Է� �ʵ� ���� �̺�Ʈ �����ʸ� �߰��մϴ�.
        nameInputField.onValueChanged.AddListener(delegate { HideWarningText(); });
        editNameInputField.onValueChanged.AddListener(delegate { HideWarningText(); });
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
            titleNameText.GetComponent<Text>().text = firstName + honorific + ", �ݰ���!";

            // �г� ��Ȱ��ȭ
            namePanel.SetActive(false);
        }
    }

    // �̸� �Է� �ʵ尡 ����� �� ȣ��� �޼����Դϴ�.
    private void HideWarningText()
    {
        if (warningText.activeSelf)
        {
            warningText.SetActive(false);
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

    public void OnEditNameButtonClicked()
    {
        // ���� �г��� Ȱ��ȭ
        editNamePanel.SetActive(true);

        // ����� �̸��� editNameInputField�� Placeholder�� ǥ��
        string storedName = PlayerPrefs.GetString("PlayerName", "");
        Text placeholderText = editNameInputField.placeholder as Text; // Legacy UI ��� ��
        // TMP_Text placeholderText = editNameInputField.placeholder as TMP_Text; // TextMeshPro ��� ��
        placeholderText.text = storedName; // Placeholder�� ����� �̸��� ����

        // editNameInputField�� text�� ��� ����� �Է��� ���� �غ�
        editNameInputField.text = "";
    }

    // �̸� ���� ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void OnSaveEditedNameButtonClicked()
    {
        string newName = editNameInputField.text;
        if (!string.IsNullOrEmpty(newName) && newName.Length >= 2)
        {
            // �� �̸��� ����
            PlayerPrefs.SetString("PlayerName", newName);
            string firstName = newName.Substring(1);
            string honorific = DetermineHonorific(firstName);
            titleNameText.GetComponent<Text>().text = firstName + honorific + ", �ݰ���!";

            // ���� �г��� ��Ȱ��ȭ
            editNamePanel.SetActive(false);
        }
        else
        {
            // �������� ���� �̸��� ���� ��� ǥ��
            warningText.SetActive(true);
        }
    }

    // �̸� ���� ��� ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void OnCancelEditButtonClicked()
    {
        // ���� �г��� ��Ȱ��ȭ
        editNamePanel.SetActive(false);
    }
}
