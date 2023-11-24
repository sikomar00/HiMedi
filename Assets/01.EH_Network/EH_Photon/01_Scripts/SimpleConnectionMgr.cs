using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class SimpleConnectionMgr : MonoBehaviourPunCallbacks
{
    // 경기 참여 인원
    public int masterPlayerNumber = 2;

    // 플레이어 생성 위치
    public List<Transform> spawnList = new List<Transform>();
    public List<GameObject> spawnCharacterList = new List<GameObject>();
    public List<GameObject> spawnPlayerList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Photon 환경설정을 기반으로 접속을 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //마스터 서버 접속 완료
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        print(nameof(OnConnectedToMaster));

        //로비진입
        JoinLobby();
    }

    //로비진입
    void JoinLobby()
    {
        //닉네임 설정
        PhotonNetwork.NickName = "My Nickname" + Random.Range(0, 100);
        //기본 Lobby 입장
        PhotonNetwork.JoinLobby();
    }

    //로비진입 완료
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(nameof(OnJoinedLobby));
        //방 생성 or 참여
        RoomOptions roomOptioin = new RoomOptions();

        //방에 들어올 수 있는 최대 인원
        roomOptioin.MaxPlayers = 20;

        PhotonNetwork.JoinOrCreateRoom("MetaIOC1_room", roomOptioin, TypedLobby.Default);
    }

    //방 생성 완료시 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(nameof(OnCreatedRoom));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print(nameof(OnCreateRoomFailed));

        //방 생성 실패 원인을 보여주는 팝업 띄워줘야 겠죠?
    }

    

  
    

    //방 참여 성공시 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(nameof(OnJoinedRoom));
        //Game Scene 으로 이동
        //PhotonNetwork.LoadLevel("GameScene");

        PhotonNetwork.SerializationRate = PhotonNetwork.SendRate = 60;

        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        spawnPlayerList.Add(PhotonNetwork.Instantiate(spawnCharacterList[index].name, spawnList[index].position, spawnList[index].rotation));

        //if (PhotonNetwork.CurrentRoom.PlayerCount == masterPlayerNumber)
        //{
        //    spawnPlayerList.Add(PhotonNetwork.Instantiate(spawnCharacterList[index].name, spawnList[index].position, spawnList[index].rotation));
        //}
    }


}
