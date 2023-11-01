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

    public GameObject Plane;
    public Texture[] image;

    
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
        count = 0;
        // firstImage.enabled = false;
        // secondImage.enabled = false;
        breath = false;
        Plane.GetComponent<MeshRenderer>().material.mainTexture = image[0];
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            count++;
        }
        if (count < 11 && breath == false)
        {
            //print("count" + count);
            Plane.GetComponent<MeshRenderer>().material.mainTexture = image[count];
            
        }

        if (count >= 11)
        {
            count = 1;
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
