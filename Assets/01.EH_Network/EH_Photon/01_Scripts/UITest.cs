using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    public GameObject title;
    public GameObject btn;

    void Start()
    {
        //ó�� ��ġ�� ����
        Vector3 oringPos = title.transform.position;
        //Ÿ��Ʋ�� ��ġ�� ȭ�� ������ �̵�
        RectTransform rt = title.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(Screen.width, rt.anchoredPosition.y);

        //iTween �� �̿��ؼ� ȭ�� ��� ������ ����.
        iTween.MoveTo(title, iTween.Hash(
            "delay", 1,
            "x", oringPos.x,
            "easetype", iTween.EaseType.easeOutBounce,
            "time", 1            
        ));

        btn.transform.localScale = Vector3.zero;
        iTween.ScaleTo(btn, iTween.Hash(
            "delay", 2,
            "scale", Vector3.one,
            "easetype", iTween.EaseType.easeOutBack,
            "time", 0.5f
        ));

    }

    void Update()
    {
        
    }
}
