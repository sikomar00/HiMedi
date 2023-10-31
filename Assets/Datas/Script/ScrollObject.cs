using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 5f; // 이동 속도입니다.
    public GameObject myObject; // Unity 에디터에서 드래그하여 넣을 오브젝트입니다.

    private Vector3 startPosition;
    private float objectWidth;
    private Camera mainCamera;

    void Start()
    {
        startPosition = transform.position;

        // 메인 카메라를 찾습니다.
        mainCamera = Camera.main;

        if (myObject != null)
        {
            // 렌더러나 콜라이더 등으로부터 오브젝트의 너비를 계산합니다.
            Renderer myRenderer = myObject.GetComponent<Renderer>();
            if (myRenderer != null)
            {
                objectWidth = myRenderer.bounds.size.x;
            }
            else
            {
                // 렌더러가 없는 경우, 콜라이더를 사용할 수 있습니다.
                Collider myCollider = myObject.GetComponent<Collider>();
                if (myCollider != null)
                {
                    objectWidth = myCollider.bounds.size.x;
                }
                // 그 외의 경우, 추가적인 처리가 필요할 수 있습니다.
            }
        }
    }

    void Update()
    {
        // 오브젝트를 일정 속도로 오른쪽으로 이동시킵니다.
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        // 오브젝트의 현재 위치가 화면의 어느 부분인지를 계산합니다.
        Vector3 viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);

        // 오브젝트가 화면 밖으로 나갔는지 확인합니다.
        if (viewPortPosition.x - (objectWidth / 2 / mainCamera.aspect) > 1.0f)
        {
            // 화면 밖으로 나간 경우, 위치를 재조정합니다.
            Vector3 viewPortStartPosition = new Vector3(0 - (objectWidth / 2 / mainCamera.aspect), viewPortPosition.y, viewPortPosition.z);
            Vector3 worldStartPosition = mainCamera.ViewportToWorldPoint(viewPortStartPosition);
            worldStartPosition.z = startPosition.z; // z축은 변하지 않아야 합니다.
            transform.position = worldStartPosition;
        }
    }
}
