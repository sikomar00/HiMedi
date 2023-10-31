using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatespheres : MonoBehaviour
{
    Vector3 movement;
    public int xi, yi, zi;
    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(xi, yi, zi);  //x y z
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(movement * Time.deltaTime);
    }
}
