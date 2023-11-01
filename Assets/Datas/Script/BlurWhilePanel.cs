using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlurWhilePanel : MonoBehaviour
{
    public GameObject BlurImage; // 블러 처리된 이미지가 있는 GameObject

    public void OpenPanel()
    {
        // 블러 이미지를 활성화
        BlurImage.SetActive(true);
    }

    public void ClosePanel()
    {
        // 블러 이미지를 비활성화
        BlurImage.SetActive(false);
    }
}