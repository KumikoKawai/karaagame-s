using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomIn : Photon.MonoBehaviour
{

    private GameObject _roomMakeButton;
    private GameObject _roomInButton;
    private GameObject _gameStartButton;

    void Start()
    {
        _roomMakeButton = GameObject.Find("RoomMake");
        _roomInButton = GameObject.Find("RoomIn");
        _gameStartButton = GameObject.Find("NextSceneButton");
        
    }

    // すでに存在しているRoomにランダムに入る
    public void InRoom()
    {
        if (RoomMake.createRoomFlag)
        {
            PhotonNetwork.JoinRandomRoom();
            RoomMake.playerNumCnt++;
        }
        if (RoomMake.playerNumCnt == 2)
        {
            _roomMakeButton.SetActive(false);
            _roomInButton.SetActive(false);
            _gameStartButton.SetActive(true);
        }
    }


    // Roomが存在していなかったときに呼ばれる
    void OnPhotonRandomJoinFailed()
    {
       
    }

}