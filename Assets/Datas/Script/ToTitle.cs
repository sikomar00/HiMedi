using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    public void LoadTitleScene()
    {
        print("dadad");
        SceneManager.LoadScene("0.Title"); // "ARModeScene"�� ������ ����Ϸ��� ���� �̸����� �����ؾ� �մϴ�.
    }
}
