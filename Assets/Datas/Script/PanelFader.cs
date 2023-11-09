using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelFader : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 인스펙터에서 할당해주세요.
    public float fadeDuration = 1f;

    void Start()
    {
        // 페이드 인을 바로 시작하려면 여기에 호출합니다.
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // 시간에 따라 알파값을 0에서 1로 변경합니다.
            canvasGroup.alpha = elapsedTime / fadeDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f; // 페이드 인 완료 후 알파값을 완전히 불투명하게 설정합니다.
    }

    // 필요한 경우 페이드 아웃을 위한 함수도 여기에 추가할 수 있습니다.
}
