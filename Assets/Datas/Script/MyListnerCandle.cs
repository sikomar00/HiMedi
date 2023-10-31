using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MyListnerCandle : MonoBehaviour
{

    int state; // 1�϶��� ��� 0�϶��� ����
    int prevState;
    int currentState;

    int count;

    public Sprite[] imageSprite;

    public Image firstImage;
    public Image secondImage;
    public Image thirdImage;

    public TMP_Text text;

    public bool breath;
    public bool breath2;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        currentState = 0;
        currentTime = 0;
        prevState = 0;

        firstImage.enabled = false;
        secondImage.enabled = false;
        thirdImage.enabled = true;
        breath = false;
    }

    public void ThridImageOn()
    {
        thirdImage.enabled = true;
    }

    public void secondImageOn()
    {
        secondImage.enabled = true;
    }

    public void FirstImageOn()
    {
        firstImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if (prevState == currentState)
        // {
        //     print("same");
        //     currentTime += Time.deltaTime;
        //     if (currentTime > 5)
        //     {
        //         currentTime = 0;
        //         //text.text = "바르게 앉아주세요";
        //     }
        // }
        // else if (prevState != currentState)
        // {
        //     currentTime = 0;
        //     text.text = " ";
        //     print("diff");
        // }
            print("count" + count);
        if (count < 10 && breath == false)
        {
            //print("count" + count);
            if (count == 1)
            {
                ThridImageOn();
            }
            thirdImage.sprite = imageSprite[count];
        }
        else if (count < 100 && breath == false)
        {
            if (count == 10)
            {
                secondImageOn();
            }
            int tenNumber = count / 10;
            int oneNumber = count - (tenNumber * 10);

            secondImage.sprite = imageSprite[tenNumber];
            thirdImage.sprite = imageSprite[oneNumber];
            //print(tenNumber + oneNumber);
            //print(tenNumber);
            //print(oneNumber);
        }
        else if (count < 1000 && breath == false)
        {
            if (count == 100)
            {
                FirstImageOn();
            }

            int hundredNumber = count / 100;
            int tenNumber = (count - (hundredNumber * 100)) / 10;
            int oneNumber = ((count - (hundredNumber * 100)) - tenNumber * 10);

            firstImage.sprite = imageSprite[hundredNumber];
            secondImage.sprite = imageSprite[tenNumber];
            thirdImage.sprite = imageSprite[oneNumber];

            //print(hundredNumber + tenNumber + oneNumber);
            //print(hundredNumber);
            //print(tenNumber);
            //print(oneNumber);
        }
    }

    //Receive Data and Test Sensor
    void OnMessageArrived(string msg)
    {
        //����� Ŀ����, ������ �۾�������
        Debug.Log("moving at speed: " + msg);
        float speed = float.Parse(msg);

        if (speed <= 30f)
        {
            if (breath == false && speed <= 29.6f)
            {
                currentTime = 0;
                text.text = " ";
                breath = true;
                //prevState = state;
                print("here");
            }
            currentTime += Time.deltaTime;
            if (currentTime > 3)
            {
                //currentTime = 0;
                text.text = "바르게 앉아주세요";
            }
            print("@@@@@@@@@@@@");
            state = 1;
        }
        else if (speed > 30f)
        {
            if (breath == true && speed > 30.3f)
            {
                print("breath end");
                breath = false;
                count++;
                currentTime = 0;
                //prevState = state;
                text.text = " ";
            }
            currentTime += Time.deltaTime;
            if (currentTime > 3)
            {
                //currentTime = 0;
                text.text = "바르게 앉아주세요";
            }
            print("################");
            state = 0;
        }

    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
