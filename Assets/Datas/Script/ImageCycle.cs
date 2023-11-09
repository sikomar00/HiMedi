using System.Collections;
using UnityEngine;

public class ImageCycle : MonoBehaviour
{
    public static ImageCycle instance;
    public GameObject Plane;
    public Texture[] image;
    public float imageChangeInterval = 4f;

    public int currentIndex = 0;
    public bool cycleStarted = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (image == null || image.Length == 0) // 이미지 배열이 비어있는지 확인
        {
            Debug.LogError("Image array is empty.");
            return;
        }
        SetImage();
    }

    void Update()
    {
        //cyclingImage();
    }
    public void cyclingImage()
    {
        if (cycleStarted == false)
        {
            Debug.Log("SpaceBar");
            cycleStarted = true;
            StartCoroutine(ChangeImageCoroutine());
        }
    }


    IEnumerator ChangeImageCoroutine()
    {
        while (true)
        {
            currentIndex = (currentIndex == 0 || currentIndex == image.Length - 1) ? 1 : (currentIndex % (image.Length - 1)) + 1;
            SetImage();
            yield return new WaitForSeconds(imageChangeInterval);
        }
    }

    void SetImage()
    {
        if (currentIndex < 0 || currentIndex >= image.Length) // 인덱스가 배열의 범위를 벗어나는지 확인
        {
            Debug.LogError("Index out of range: " + currentIndex);
            return;
        }
        Plane.GetComponent<MeshRenderer>().material.mainTexture = image[currentIndex];
    }

    public void ResetCount()
    {
        currentIndex = 0;
    }

    public void ResetBool()
    {
        cycleStarted = false;
    }
}
