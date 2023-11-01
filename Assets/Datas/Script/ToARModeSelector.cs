using UnityEngine;
using UnityEngine.SceneManagement;

public class ToARModeSelector : MonoBehaviour
{
    public void LoadARModeScene()
    {
        SceneManager.LoadScene("1.SelectARMode"); // "ARModeScene"�� ������ ����Ϸ��� ���� �̸����� �����ؾ� �մϴ�.
    }
}
