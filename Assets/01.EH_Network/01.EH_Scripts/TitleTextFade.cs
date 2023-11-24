using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextFade : MonoBehaviour
{
    public Image titleText;

    void Awake()
    {
        StartCoroutine(FadeTextToZero(titleText));
    }

    public IEnumerator FadeTextToFullAlpha(Image text) // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 1f));
            yield return null;
        }
        if (text.color.a == 1.0f)
            yield return 30f;
        StartCoroutine(FadeTextToZero(text));
    }

    public IEnumerator FadeTextToZero(Image text)  // 알파값 1에서 0으로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 1f));
            yield return null;
        }
        if (text.color.a == 0.0f)
            yield return 30f;
        StartCoroutine(FadeTextToFullAlpha(text));
    }

    private void Start()
    {
        
    }
}