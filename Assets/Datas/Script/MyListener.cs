using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    public GameObject cubeModifier;
    public float Maxsize;
    public float Minsize;
    public bool breath;
    public int count;

    public float Upsize;
    public float Downsize;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Receive Data and Test Sensor
    void OnMessageArrived(string msg)
    {
        //����� Ŀ����, ������ �۾�������
        Debug.Log("moving at speed: " + msg);
        float speed = float.Parse(msg);

        if (speed <= 30)
        {
            if (cubeModifier.gameObject.transform.localScale.x < Maxsize)
            {
                cubeModifier.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
            breath = true;
        }
        else if (speed > 30)
        {
            print(speed);
            if (breath == true)
            {
                breath = false;
                count++;
            }
            if (cubeModifier.gameObject.transform.localScale.x > Minsize)
            {
                cubeModifier.gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            }
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