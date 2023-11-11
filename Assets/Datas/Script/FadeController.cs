using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public CanvasGroup uiElement; // Inspector에서 할당할 UI 요소의 CanvasGroup
    public CanvasGroup otherUiElement; // 다른 패널의 CanvasGroup
    public float fadeInDuration = 1.0f; // 페이드 인에 걸리는 시간
    public float fadeOutDuration = 1.0f; // 페이드 아웃에 걸리는 시간

    // 페이드 인을 시작하는 함수
    public void FadeIn()
    {
        // 다른 UI 요소가 비활성화 상태일 때만 페이드 인을 진행합니다.
        if (otherUiElement != null && otherUiElement.alpha == 0)
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, fadeInDuration));
        }
    }

    // 페이드 아웃을 시작하는 함수
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, fadeOutDuration));
    }

    // CanvasGroup의 투명도를 점진적으로 변경하는 코루틴
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted;
        float percentageComplete;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
        //TimeCheck.instance.finishUI
        //cg.blocksRaycasts = end == 1;
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FadeController : MonoBehaviour
//{
//    public CanvasGroup uiElement; // Inspector에서 할당할 UI 요소의 CanvasGroup
//    public float fadeInDuration = 1.0f; // 페이드 인에 걸리는 시간
//    public float fadeOutDuration = 1.0f; // 페이드 아웃에 걸리는 시간

//    // 페이드 인을 시작하는 함수
//    public void FadeIn()
//    {
//        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, fadeInDuration));
//    }

//    // 페이드 아웃을 시작하는 함수
//    public void FadeOut()
//    {
//        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, fadeOutDuration));
//    }

//    // CanvasGroup의 투명도를 점진적으로 변경하는 코루틴
//    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime)
//    {
//        float _timeStartedLerping = Time.time;
//        float timeSinceStarted;
//        float percentageComplete;

//        while (true)
//        {
//            timeSinceStarted = Time.time - _timeStartedLerping;
//            percentageComplete = timeSinceStarted / lerpTime;

//            float currentValue = Mathf.Lerp(start, end, percentageComplete);

//            cg.alpha = currentValue;

//            if (percentageComplete >= 1) break;

//            yield return new WaitForEndOfFrame();
//        }

//        cg.blocksRaycasts = end == 1;
//    }
//}