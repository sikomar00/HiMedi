using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //BGM ������
    public enum EBgm
    {
        BGM_CONNECTION,
        BGM_LOBBY,
        BGM_GAME
    }
    //SFX ������
    public enum ESfx
    {
        SFX_BUTTON,
        SFX_JUMP,

    }

    //bgm audio clip ���� �� �ִ� �迭
    public AudioClip[] bgms;
    //sfx audio clip ���� �� �ִ� �迭
    public AudioClip[] sfxs;

    //bgm �÷����ϴ� AudioSource
    public AudioSource audioBgm;
    //sfx �÷����ϴ� AudioSource
    public AudioSource audioSfx;


    //���� ���� static ����
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //BGM Play
    public void PlayBGM(EBgm bgmIdx)
    {
        //�÷��� �� bgm ����
        audioBgm.clip = bgms[(int)bgmIdx];
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    //SFX Play
    public void PlaySFX(ESfx sfxIdx)
    {
        //�÷���
        audioSfx.PlayOneShot(sfxs[(int)sfxIdx]);
    }
}
