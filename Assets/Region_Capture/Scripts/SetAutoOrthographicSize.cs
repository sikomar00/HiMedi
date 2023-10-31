// using UnityEngine;

// [ExecuteInEditMode]
// public class SetAutoOrthographicSize : MonoBehaviour
// {
//     void Update()
//     {
//         if (GetComponent<Camera>())
//         {
//             GetComponent<Camera>().orthographicSize = transform.lossyScale.y * 5.0f;
//         }
//     }
// }


using UnityEngine;

[ExecuteInEditMode]
public class SetAutoOrthographicSize : MonoBehaviour
{
    private Camera _camera;

    // 가로와 세로 스케일 계수를 위한 public 변수들
    public float horizontalScaleFactor = 5.0f;
    public float verticalScaleFactor = 5.0f;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (_camera != null)
        {
            // 세로 크기를 조정합니다.
            _camera.orthographicSize = transform.lossyScale.y * verticalScaleFactor;

            // 가로 크기를 조정하기 위해 종횡비를 계산합니다.
            float aspectRatio = (transform.lossyScale.x * horizontalScaleFactor) / (_camera.orthographicSize * 2);
            SetCameraRectWithAspectRatio(aspectRatio);
        }
        else
        {
            Debug.LogWarning("Camera component is not found!");
        }
    }

    void SetCameraRectWithAspectRatio(float targetAspectRatio)
    {
        // 현재 화면의 종횡비를 가져옵니다.
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = screenAspect / targetAspectRatio;

        if (scaleHeight < 1.0f)
        {  
            Rect rect = _camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            _camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = _camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            _camera.rect = rect;
        }
    }
}
