using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSet : MonoBehaviour
{
    public static TimeSet instance;
    public float time;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //나 삭제하지마 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        time = 60f;
    }
}
