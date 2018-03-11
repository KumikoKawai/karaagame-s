using System.Linq;
using UnityEngine;

public class RoomManager : Photon.MonoBehaviour
{


    #region Singleton

    private static RoomManager instance;

    public static RoomManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (RoomManager)FindObjectOfType(typeof(RoomManager));

                if (instance == null)
                {
                    //Debug.LogError(typeof(RoomManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    [SerializeField]
    private float m_randomCircle = 4.0f;

    public bool _createBall = false;

    void Start()
    {
        // ポイント2: サーバーに接続する。引数は0.1でよい
        // サーバー接続に成功した後は自動でLobbyに参加する
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    // Lobbyに参加した時に呼ばれる
    void OnJoinedLobby()
    {
        //すでに存在しているRoomにランダムに入る
        PhotonNetwork.JoinRandomRoom();
    }

    // ランダムにRoomに参加するのに失敗した時に呼ばれる
    void OnPhotonRandomJoinFailed()
    {
        //Roomを自分で作って参加する
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        PlayerMake();
    }


    // Playerを生成するメソッド
    void PlayerMake()
    {
        if(PhotonNetwork.playerList.Length == 1)
        PhotonNetwork.Instantiate("Rackets",Player1Create(), Quaternion.identity, 0);

        if(PhotonNetwork.playerList.Length == 2)
        PhotonNetwork.Instantiate("Rackets",Player2Create(), Quaternion.identity, 0);
    }

    // Ballを生成するメソッド
    public void BallMake()
    {

        if (!_createBall)
        {
            PhotonNetwork.Instantiate("Ball", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            _createBall = true;
        }
    }

    private Vector3 Player2Create()
    {
            var pos = transform.position;
            pos.x = -9;
            pos.y = 0;
            pos.z = 1;
            transform.position = pos;
            return pos;

    }

    private Vector3 Player1Create()
    {
        var pos = transform.position;
        pos.x = 9;
        pos.y = 0;
        pos.z = 1;
        transform.position = pos;
        return pos;

    }

    // debug用
    void OnGUI()
    {
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
