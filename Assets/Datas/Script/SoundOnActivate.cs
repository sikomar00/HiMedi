using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnActivate : MonoBehaviour
{
    public AudioSource myAudioSource; // 오디오 소스 컴포넌트에 대한 참조

    // Unity 에디터에서 오디오 소스를 할당할 수 있도록 합니다.
    // 또는 Start() 함수에서 이를 찾거나 할당할 수 있습니다.
    // 예: myAudioSource = GetComponent<AudioSource>();

    // 오브젝트가 활성화될 때 호출되는 Unity 함수입니다.
    private void OnEnable()
    {
        // 오디오 소스가 있고, 오디오 클립이 할당되어 있는지 확인합니다.
        if (myAudioSource != null && myAudioSource.clip != null)
        {
            // 사운드 재생
            myAudioSource.Play();
        }
    }
}
