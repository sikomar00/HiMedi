using UnityEngine;

public class PanelBlurEffect : MonoBehaviour
{
    public GameObject blurImage; // 블러 처리된 이미지가 있는 GameObject

    // 이 스크립트가 부착된 패널이 활성화될 때 호출됩니다.
    private void OnEnable()
    {
        // 블러 이미지를 활성화합니다.
        blurImage.SetActive(true);
    }

    // 이 스크립트가 부착된 패널이 비활성화될 때 호출됩니다.
    private void OnDisable()
    {
        // 블러 이미지를 비활성화합니다.
        blurImage.SetActive(false);
    }
}
