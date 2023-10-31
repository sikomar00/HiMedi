using UnityEngine;
using System.IO;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RenderTextureCamera : MonoBehaviour
{
    [Space(20)]
    public int TextureResolution = 512; // 생성될 텍스처의 해상도

    private string screensPath; // 스크린샷 저장 경로
    private int TextureResolutionX; // 렌더 텍스처의 가로 해상도
    private int TextureResolutionY; // 렌더 텍스처의 세로 해상도

    private Camera Render_Texture_Camera; // 이 오브젝트의 카메라 컴포넌트 참조
    private RenderTexture CameraOutputTexture; // 카메라 출력이 저장되는 렌더 텍스처

    public RenderTexture GetRenderTexture()
    {
        return CameraOutputTexture; // 현재 렌더 텍스처 반환
    }

    void Start() 
    {
        Render_Texture_Camera = GetComponent<Camera>(); // 카메라 컴포넌트 가져오기
        StartRenderingToTexture(); // 텍스처 렌더링 시작
    }

    void StartRenderingToTexture() 
    {
        // 오브젝트의 스케일에 따라 해상도 동적 계산
        if (transform.lossyScale.x >= transform.lossyScale.y)
        {
            TextureResolutionX = TextureResolution;
            TextureResolutionY = (int)(TextureResolution * transform.lossyScale.y / transform.lossyScale.x);
        }
        else // 스케일에 따라 렌더링 해상도 조정
        {
            TextureResolutionX = (int)(TextureResolution * transform.lossyScale.x / transform.lossyScale.y);
            TextureResolutionY = TextureResolution;
        }

        // 이전 RenderTexture가 있었다면 제거
        if (CameraOutputTexture)
        {
            Render_Texture_Camera.targetTexture = null;
            CameraOutputTexture.Release();
            CameraOutputTexture = null;
        }

        // 새 RenderTexture 생성 및 카메라 설정
        CameraOutputTexture = new RenderTexture(TextureResolutionX, TextureResolutionY, 0);
        CameraOutputTexture.Create();
        Render_Texture_Camera.targetTexture = CameraOutputTexture;

        // 카메라 레이어 설정
        if (transform.parent) gameObject.layer = transform.parent.gameObject.layer;
        Render_Texture_Camera.cullingMask = 1 << gameObject.layer;
    }

    public void MakeScreen() 
    {
        StartRenderingToTexture();  // 렌더링 재시작

        // 저장 경로 설정
        if (screensPath == null) 
        {
            // 플랫폼 별 저장 경로 설정
            #if UNITY_ANDROID && !UNITY_EDITOR
            screensPath = Application.temporaryCachePath;
            #elif UNITY_IPHONE && !UNITY_EDITOR
            screensPath = Application.temporaryCachePath;
            #else
            screensPath = Application.dataPath + "/Screens"; // 에디터 모드일 때의 경로
            #endif

            // 경로가 없으면 생성
            if (!Directory.Exists(screensPath))
                Directory.CreateDirectory(screensPath);
        }

        StartCoroutine(TakeScreen()); // 스크린샷 촬영 코루틴 시작
    }

    private IEnumerator TakeScreen() 
    {
        yield return new WaitForEndOfFrame(); // 현재 프레임 끝날 때까지 대기

        // 캡처된 이미지로부터 텍스처 생성
        Texture2D FrameTexture = new Texture2D(CameraOutputTexture.width, CameraOutputTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = CameraOutputTexture;
        FrameTexture.ReadPixels(new Rect(0, 0, CameraOutputTexture.width, CameraOutputTexture.height), 0, 0);
        RenderTexture.active = null;

        FrameTexture.Apply(); // 텍스처 변경 적용
        saveImg(FrameTexture.EncodeToPNG()); // PNG로 변환 후 저장
    }

    private string saveImg(byte[] imgPng)
    {
        // 파일명 정의 및 이미지 저장
        string fileName = screensPath + "/screen_" + System.DateTime.Now.ToString("dd_MM_HH_mm_ss") + ".png";

        Debug.Log("write to " + fileName); // 로깅

        File.WriteAllBytes(fileName, imgPng); // 파일 쓰기

        #if UNITY_EDITOR
        AssetDatabase.Refresh(); // 에디터에서 에셋 데이터베이스 새로고침
        #endif

        return fileName; // 파일 경로 반환
    }
}
