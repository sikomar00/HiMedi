using UnityEngine;
using UnityEngine.SceneManagement;

public class ToARModeSelector : MonoBehaviour
{
    public void LoadARModeScene()
    {
        SceneManager.LoadScene("1.SelectARMode"); // "ARModeScene"은 실제로 사용하려는 씬의 이름으로 변경해야 합니다.
    }
}
