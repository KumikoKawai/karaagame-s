using UnityEngine;
using System.Collections;
using HashTable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;

enum SceneState : int
{

    Title = 0,
    select = 1,
    Loby = 2,
    Room = 3,
    ChildsGamePlay = 4,
    HostGamePlay = 5,

}

public enum GameState : int //今プレイヤーがどの状態にあるか？
{
    Room = 0,
    Play = 1,

}

public class StartScene : Photon.MonoBehaviour
{

    float ConnectTime = 0.0f;

    int selectGrid;

    SceneState sceneState;

    string RoomName = "PressRoomName";
    int playerMaxCount = 0;
    string[] playerCount = { "1", "2", "3", "4" };

    PhotonView pView;

    int playerCnt = 0;

    List<PhotonPlayer> pList = new List<PhotonPlayer>();

    [SerializeField]
    SCENES nextScene = 0;


    void Awake()
    {
        pView = this.GetComponent<PhotonView>();

        //PhotonNetwork.isMessageQueueRunning = false;

    }

    // Use this for initialization
    void Start()
    {

        sceneState = SceneState.Title;

    }

    // Update is called once per frame
    void Update()
    {

        if (ConnectTime >= 0.1f)
        {
            ConnectTime += Time.deltaTime;
            //if (PhotonNetwork.connectionStateDetailed == PeerState.JoinedLobby)
            //{
                ConnectTime = 0f;
                sceneState = SceneState.Loby;

            //}

        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log(PhotonNetwork.isMessageQueueRunning);
        }

    }

    void OnGUI()
    {
        int sw = Screen.width;
        int sh = Screen.height;

        switch (sceneState)
        {
            case SceneState.Title: //接続するまで

                if (GUI.Button(new Rect(sw / 2f, sh / 2f, 30f, 30f), "接続"))
                {
                    PhotonNetwork.ConnectUsingSettings("v1.0");
                    ConnectTime = 0.1f;
                }

                //if (ConnectTime >= 0.1f && PhotonNetwork.connectionStateDetailed != PeerState.JoinedLobby)
                //{
                //    GUILayout.Label("接続中");
                //}
                break;

            case SceneState.Loby://接続後.


                RoomName = GUI.TextField(new Rect(100, 100, 150, 20), RoomName, 16);

                GUI.Label(new Rect(100f, 170f, 50f, 50f), "Player数");
                playerMaxCount = GUI.SelectionGrid(new Rect(100, 200, 100, 20), playerMaxCount, playerCount, 4);


                if (GUILayout.Button("createroom"))
                {
                    RoomOptions ro = new RoomOptions();
                   //ro.MaxPlayers = playerMaxCount + 1;
                    ro.IsOpen = true;
                    ro.IsVisible = true;
                    string[] s = { "BS" }; //BS:BattleState.
                    ro.CustomRoomPropertiesForLobby = s;  //ロビーで表示される値.
                    ro.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "BS", "idle" } };
                    PhotonNetwork.CreateRoom(RoomName, ro, TypedLobby.Default);
                    sceneState = SceneState.Room;

                }

                if (PhotonNetwork.countOfRooms == 0)
                    return;

                foreach (RoomInfo game in PhotonNetwork.GetRoomList())
                {

                    GUI.Label(new Rect(sw / 2f, (sh * 2 / 3), 500, 30), game.Name + " " + game.PlayerCount + "/" + game.MaxPlayers + "/" + game.CustomProperties["BS"]);
                    if (GUILayout.Button("JOIN"))
                    {
                        PhotonNetwork.JoinRoom(game.Name);

                        sceneState = SceneState.Room;

                    }

                }

                break;
            case SceneState.Room://ルームに入った後.

                if (PhotonNetwork.inRoom && PhotonNetwork.isMasterClient)
                {
                    if (GUI.Button(new Rect(100, 100, 100, 100), "GameStart"))
                    {
                        PhotonNetwork.room.IsOpen = false; //エラーが起きたら困るので一回ここでルームを閉じる.

                        HashTable h = new HashTable() { { "BS", "Standing" } }; //ルームのステータスを変更.スタートアップ中.

                        PhotonNetwork.room.SetCustomProperties(h);

                        sceneState = SceneState.ChildsGamePlay;//現在意味をなしていない.

                        //room情報をセットし終わったらリモートクライアントにシーンを呼ばせる.

                        PhotonNetwork.DestroyAll();

                        SendGameStart();
                    }

                }

                if (GUILayout.Button("testInstance"))
                    PhotonNetwork.Instantiate("TestInstance", Vector3.zero, Quaternion.identity, 0);

                break;
                /* case SceneState.ChildsGamePlay:

                     int cnt = 0;
                     //マスタークライアント以外のローカルクライアントがしっかりシーンを読み終わったかを調べる.
                     foreach (PhotonPlayer pp in PhotonNetwork.otherPlayers)
                     {
                         if ((int)pp.customProperties["GS"] == (int)GameState.Play)
                         {
                             cnt++;

                         }
                     }
                     if (cnt == PhotonNetwork.otherPlayers.Length)
                         sceneState = SceneState.HostGamePlay;


                     break;
                 case SceneState.HostGamePlay:

                     Debug.Log("MC以外の準備完了");

                     PhotonNetwork.isMessageQueueRunning = false;

                     Application.LoadLevel(1);


                     break;*/
        }



        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        GUILayout.Label(ConnectTime.ToString());
        GUILayout.Label(PhotonNetwork.connectionState.ToString());
    }


    void SendGameStart()
    {
        pView.RPC("SendGameStartRPC", PhotonTargets.All);
    }

    [PunRPC]
    void SendGameStartRPC()
    {
        //メッセージを一時的に遮断.
        PhotonNetwork.isMessageQueueRunning = false;

        CSceneManager.SceneMove(nextScene);

    }

    //ルームに入った時呼ばれる.
    void OnJoinedRoom()
    {
        //プレイヤーがバトル用のシーンを読み込んだかチェック.
        HashTable h = new HashTable() { { "GS", GameState.Room } }; //playerのカスタムプロパティ.

        //ルームプロパティとは違います.
        PhotonNetwork.player.SetCustomProperties(h);

        RoomInfo r = PhotonNetwork.room;

        //ゲームプレイ中に入ったら一時的にイベントをシャットアウト.
        //そうしないと、Roomに入った瞬間インスタンス情報が流れてきてしまう.
        if ((string)r.CustomProperties["BS"] != "idle")
        {
            PhotonNetwork.isMessageQueueRunning = false;

            CSceneManager.SceneMove(nextScene);
        }
    }

    void OnPhotonJoinRoomFailed()
    {
        //ルームに入れなかった場合
        sceneState = SceneState.Loby;

    }

}

