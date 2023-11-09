using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCheck : MonoBehaviour
{
    public static TimeCheck instance;
    public GameObject finishUI;
    public float fadeDuration = 1f; // 페이드 인에 걸리는 시간을 초 단위로 설정
    float limitTime;
    public bool finish;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        limitTime = TimeSet.instance.time;
        //finishUI = GameObject.FindWithTag("FinishUI");
        finish = false;
    }

    public IEnumerator TimeWatch()
    {
        if (finish == false)
        {
            finish = true;
            yield return StartCoroutine(MoveTime());
            // MoveTime 코루틴이 끝나면 페이드 인 시작
            StartCoroutine(FadeInFinishUI());
        }
    }

    IEnumerator FadeInFinishUI()
    {
       // if (finishUI.activeSelf == false)
     //  {
            finishUI.SetActive(true); // UI를 활성화합니다.
            CanvasGroup canvasGroup = finishUI.GetComponent<CanvasGroup>();
            print("dadada");

            if (canvasGroup != null)
            {
                float currentTime = 0f;
                canvasGroup.alpha = 0f; // 시작 알파 값을 0으로 설정합니다.

                // 알파 값을 0에서 1로 변경
                while (canvasGroup.alpha < 1)
                {
                    currentTime += Time.deltaTime / fadeDuration; // 경과 시간에 따라 알파 값을 증가
                    canvasGroup.alpha = currentTime; // 알파 값을 업데이트
                    yield return null;
                }
                canvasGroup.alpha = 1f; // 최종 알파 값을 확실히 1로 설정
            }
            else
            {
                Debug.LogError("finishUI에 Canvas Group 컴포넌트가 없습니다!");
            }
        //}
    }


    IEnumerator MoveTime()
    {
        float tmpTime = 0;
        while (tmpTime < limitTime)
        {
            tmpTime += Time.deltaTime;
            yield return null;
        }
    }

    //public void ResetTime()
    //{
    //    TimeLimit.instance.time = 0f;
    //    finishUI.SetActive(false);
    //}

    public void ResetFinish()
    {
        finish = false;
    }
}
