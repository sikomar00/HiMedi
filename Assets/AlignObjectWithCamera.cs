using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignObjectWithCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // 메인 카메라를 가져옴
        mainCamera = Camera.main;

        // 오브젝트의 현재 위치를 가져옴
        Vector3 objectPosition = transform.position;

        // 오브젝트의 위치를 카메라와 수직으로 정렬
        objectPosition.y = mainCamera.transform.position.y;

        // 오브젝트의 새 위치를 설정
        transform.position = objectPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 메인 카메라를 가져옴
        mainCamera = Camera.main;

        // 오브젝트의 현재 위치를 가져옴
        Vector3 objectPosition = transform.position;

        // 오브젝트의 위치를 카메라와 수평으로 정렬
        objectPosition.x = mainCamera.transform.position.x;

        // 오브젝트의 새 위치를 설정
        transform.position = objectPosition;
    }
}
