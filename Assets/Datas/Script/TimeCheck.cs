using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCheck : MonoBehaviour
{
    public static TimeCheck instance;
    public GameObject finishUI;
    float limitTime;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        limitTime = TimeSet.instance.time;
        //finishUI = GameObject.FindWithTag("FinishUI");
    }

    public IEnumerator TimeWatch()
    {
        yield return StartCoroutine(MoveTime());
        finishUI.SetActive(true);
    }

    IEnumerator MoveTime()
    {
        float tmpTime = 0;
        while (tmpTime < limitTime)
        {
            print(tmpTime);
            tmpTime += Time.deltaTime;
            yield return null;
        }
    }

    //public void ResetTime()
    //{
    //    TimeLimit.instance.time = 0f;
    //    finishUI.SetActive(false);
    //}
}
