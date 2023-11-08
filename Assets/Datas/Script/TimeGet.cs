using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class TimeGet : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OneMinute()
    {
        time = 60f;
        TimeSet.instance.time = time;
    }

    public void ThreeMinute()
    {
        time = 180f;
        TimeSet.instance.time = time;
    }

    public void FIveMinute()
    {
        time = 300f;
        TimeSet.instance.time = time;
    }

}
