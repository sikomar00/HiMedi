using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    //자신을 담을 static 변수
    public static NetworkManager instance = null;

    //모든 Player들의 PhotonView 를 가지는 List
    public List<PhotonView> listPlayer = new List<PhotonView>();

    private void Awake()
    {
        //만약에 instance 값이 null 이라면
        if(instance == null)
        {
            //instance 에 나 자신을 셋팅
            instance = this;
        }
        //그렇지 않으면
        else
        {
            //나를 파괴하자
            Destroy(gameObject);
        }
    }


    void Start()
    {
        //RPC 호출 빈도
        PhotonNetwork.SendRate = 30;

        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 30;

        SetSpawnPos();

        //내가 위치해야 하는 idx 구하자
        int idx = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        //나의 Player 생성
        PhotonNetwork.Instantiate("Player", spawnPos[idx], Quaternion.identity);

        //마우스 포인터를 비활성화
        Cursor.visible = false;
    }

    //spawnPosGroup Transform
    public Transform trSpawnPosGroup;

    //Spanw 위치를 담아놓을 변수
    public Vector3[] spawnPos;

    void SetSpawnPos()
    {
        //최대 인원 만큼 spawnPos 의 공간을 할당
        spawnPos = new Vector3[PhotonNetwork.CurrentRoom.MaxPlayers];
        
        //간격 (anlge)
        float angle = 360 / spawnPos.Length;
        for(int i = 0; i < spawnPos.Length; i++)
        {
            trSpawnPosGroup.Rotate(0, angle, 0);

            spawnPos[i] = trSpawnPosGroup.position + trSpawnPosGroup.forward * 5;

            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //go.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //만약에 esc 키를 누르면 
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //마우스 포인터를 활성화
            Cursor.visible = true;
        }

        //마우스 클릭했을 때
        if(Input.GetMouseButtonDown(0))
        {
            //마우스 클릭시 해당 위치에 UI가 없으면
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                //마우스 포인터를 비활성화
                Cursor.visible = false;
            }
        }
    }

    //참여한 Player 의 PhotonView 추가
    public void AddPlayer(PhotonView pv)
    {
        listPlayer.Add(pv);

        //모든 Player 가 참여했다면
        if(listPlayer.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            //Turn 을 시작하자
            ChangeTurn();
        }
    }

    //현재 Turn Idx
    int currTurnIdx = -1;
    public void ChangeTurn()
    {
        //방장이 아니라면 함수를 나가자
        if (PhotonNetwork.IsMasterClient == false) return;

        if(currTurnIdx != -1)
        {
            //발사한 사람 Turn 종료
            listPlayer[currTurnIdx].RPC("ChangeTurnRpc", RpcTarget.All, false);
        }

        //currTurnIdx 을 증가
        currTurnIdx++;

        currTurnIdx = currTurnIdx % listPlayer.Count;
        ////만약에 currTurnIdx 가 3이면
        //if(currTurnIdx == 3)
        //{
        //    //currTurnIdx 을 0 으로 한다.
        //    currTurnIdx = 0;
        //}

        //다음 사람 Turn 시작
        listPlayer[currTurnIdx].RPC("ChangeTurnRpc", RpcTarget.All, true);


    }


    //새로운 인원이 방에 들어왔을때 호출되는 함수
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        print(newPlayer.NickName +  "님이 들어왔습니다!");
    }
}
