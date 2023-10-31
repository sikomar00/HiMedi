using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ��Դϴ�.
    public GameObject myObject; // Unity �����Ϳ��� �巡���Ͽ� ���� ������Ʈ�Դϴ�.

    private Vector3 startPosition;
    private float objectWidth;
    private Camera mainCamera;

    void Start()
    {
        startPosition = transform.position;

        // ���� ī�޶� ã���ϴ�.
        mainCamera = Camera.main;

        if (myObject != null)
        {
            // �������� �ݶ��̴� �����κ��� ������Ʈ�� �ʺ� ����մϴ�.
            Renderer myRenderer = myObject.GetComponent<Renderer>();
            if (myRenderer != null)
            {
                objectWidth = myRenderer.bounds.size.x;
            }
            else
            {
                // �������� ���� ���, �ݶ��̴��� ����� �� �ֽ��ϴ�.
                Collider myCollider = myObject.GetComponent<Collider>();
                if (myCollider != null)
                {
                    objectWidth = myCollider.bounds.size.x;
                }
                // �� ���� ���, �߰����� ó���� �ʿ��� �� �ֽ��ϴ�.
            }
        }
    }

    void Update()
    {
        // ������Ʈ�� ���� �ӵ��� ���������� �̵���ŵ�ϴ�.
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        // ������Ʈ�� ���� ��ġ�� ȭ���� ��� �κ������� ����մϴ�.
        Vector3 viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);

        // ������Ʈ�� ȭ�� ������ �������� Ȯ���մϴ�.
        if (viewPortPosition.x - (objectWidth / 2 / mainCamera.aspect) > 1.0f)
        {
            // ȭ�� ������ ���� ���, ��ġ�� �������մϴ�.
            Vector3 viewPortStartPosition = new Vector3(0 - (objectWidth / 2 / mainCamera.aspect), viewPortPosition.y, viewPortPosition.z);
            Vector3 worldStartPosition = mainCamera.ViewportToWorldPoint(viewPortStartPosition);
            worldStartPosition.z = startPosition.z; // z���� ������ �ʾƾ� �մϴ�.
            transform.position = worldStartPosition;
        }
    }
}
