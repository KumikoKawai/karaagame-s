using UnityEngine;

public class RoomMake : Photon.MonoBehaviour
{

    // 参加人数を数える
    public static int playerNumCnt;

    public static bool createRoomFlag = false;

    private GameObject _roomMakeButton;
    private GameObject _roomInButton;
    private GameObject _gameStartButton;

   

    void Start()
    {

        // マスタークライアントのsceneと同じsceneを部屋に入室した人もロードする。
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("1.0");

        //CreateRoom(); // ルーム作成関数
        playerNumCnt = 0; // 初期化
        createRoomFlag = false;

        _roomMakeButton = GameObject.Find("RoomMake");
        _roomInButton = GameObject.Find("RoomIn");
        _gameStartButton = GameObject.Find("NextSceneButton");
        _gameStartButton.SetActive(false);
        _roomMakeButton.SetActive(true);
        _roomInButton.SetActive(true);

    }

    // Roomを自分で作って参加する
    void CreateRoom()
    {
        if (!createRoomFlag)
        {
            PhotonNetwork.CreateRoom(null);
            playerNumCnt++;
            createRoomFlag = true;
        }
        if (playerNumCnt == 2)
        {
            _roomMakeButton.SetActive(false);
            _roomInButton.SetActive(false);
            _gameStartButton.SetActive(true);
        }
    }
}
