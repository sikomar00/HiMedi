﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //총알 속력
    float speed = 10;

    //폭발효과공장
    public GameObject exploFactory;

    void Start()
    {
        
    }

    void Update()
    {
        //계속 앞으로 가고 싶다.
        transform.position += transform.forward * speed * Time.deltaTime;

        //PhotonNetwork.Instantiate 이용할 때
        ////내가 쏜 총알만 움직이게 하자
        //if (photonView.IsMine)
        //{
        //    //계속 앞으로 가고 싶다.
        //    transform.position += transform.forward * speed * Time.deltaTime;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //폭발효과공장에서 폭발효과를 만들자.
        GameObject explo = Instantiate(exploFactory);
        //만든 효과를 나의 위치에 놓자.
        explo.transform.position = transform.position;
        //만든 효과에서 ParticleSystem 을 가져오자.
        ParticleSystem ps = explo.GetComponent<ParticleSystem>();
        //가져온 ParticleSystem 의 기능이 Play 를 실행 하자.
        ps.Play();
        //2초뒤에 explo 를 파괴하자.
        Destroy(explo, 2);

        //나를 파괴하자
        Destroy(gameObject);
        
        ////내가 쏜 총알만 
        //if (photonView.IsMine)
        //{
        //    //나를 파괴하자
        //    PhotonNetwork.Destroy(gameObject);
        //}
    }
}
