using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1.0";
    public string nickName = "Y2K";

    private void Awake()
    {
        //하나의 클라이언트가 룸내의 모든 클라이언트들에게 로드해야할 레벨을 정의
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        //포톤게임버전을 설정(같은 게임 버전끼리 공유)
        PhotonNetwork.GameVersion = gameVersion;
        //클라이언트 닉네임을 설정
        PhotonNetwork.NickName = nickName;
        //포톤을 이용한 온라인 연결
        PhotonNetwork.ConnectUsingSettings();
    }
    
    //포톤연결이 성공하면 불려지는 콜백 메소드
    public override void OnConnectedToMaster()
    {
        Debug.Log("접속!!!!");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방이없음 실패");
        CreateRoom();
    }

    void CreateRoom()
    {
        //CreateRoom(방이름,방옵션)
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("룸입장");
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
