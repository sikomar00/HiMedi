using UnityEngine;

public class BreathingObjectScaler : MonoBehaviour
{
    // 게임 오브젝트의 초기 크기 값을 저장할 변수입니다.
    private Vector3 initialScale;

    // 호흡 센서로부터 받는 호흡의 깊이나 속도를 나타내는 값입니다. 
    // 이 값에 따라 스케일이 조정됩니다.
    public float breathIntensity;

    // 호흡에 따른 스케일 변화의 최대/최소 범위를 설정합니다.
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    void Start()
    {
        // 게임 시작 시, 오브젝트의 초기 스케일을 저장합니다.
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 호흡 센서로부터 값을 받아 'breathIntensity'를 업데이트합니다.
        // 아래는 예시 값이며, 실제 게임에서는 센서로부터 값을 받아와야 합니다.
        breathIntensity = Mathf.Clamp(breathIntensity, 0, 1); // 값이 0과 1 사이로 제한됩니다.

        // 호흡 강도에 따라 적용할 스케일 비율을 계산합니다.
        float scaleRatio = Mathf.Lerp(minScale, maxScale, breathIntensity);

        // 계산된 비율에 따라 오브젝트의 스케일을 조정합니다.
        transform.localScale = initialScale * scaleRatio;
    }

    // 호흡 센서로부터 데이터를 받는 메서드입니다.
    // 이 메서드는 센서로부터 실제 데이터를 받아와 'breathIntensity' 값을 업데이트해야 합니다.
    public void OnBreathDataReceived(float data)
    {
        // 외부 장치로부터 받은 데이터를 'breathIntensity' 값으로 설정합니다.
        // 데이터 범위에 따라 적절한 변환/정규화 로직이 필요할 수 있습니다.
        breathIntensity = data;
    }
}
