using UnityEngine;
using System.Collections;
using HashTable = ExitGames.Client.Photon.Hashtable;

enum Phase : int
{
    Prepare = 0,
    Instance = 1,
    Play = 2,
    GameOver = 3,
    None = 4,

};

public class GameMain : MonoBehaviour
{

    PhotonView pView;

    public GameObject g;

    Phase phaze = Phase.None;


    void Awake()
    {
        pView = GetComponent<PhotonView>();
    }

    // Use this for initialization
    void Start()
    {

        PhotonNetwork.isMessageQueueRunning = true; //ゲームプレイ用のシーンが読み込まれたら必ずtrueにする!

        //-------------Playerがゲーム中だとする------

        HashTable h = new HashTable() { { "GS", GameState.Play } };

        PhotonNetwork.player.SetCustomProperties(h);

        //--------------------------------------

        if ((string)PhotonNetwork.room.CustomProperties["BS"] != "Play")
        {
            phaze = Phase.Prepare;
        }
        else //途中参加
        {
            phaze = Phase.Instance;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
            Debug.Log(PhotonNetwork.player.ID);

        switch (phaze)
        {
            case Phase.Prepare:

                if (PhotonNetwork.player.IsMasterClient)
                {
                    int count = 0;
                    int otherPlayers = PhotonNetwork.otherPlayers.Length;

                    if (otherPlayers != 0)
                    {
                        foreach (PhotonPlayer pp in PhotonNetwork.otherPlayers)
                        {
                            if ((GameState)pp.CustomProperties["GS"] == GameState.Play)
                                count++;
                        }

                    }

                    //他プレイヤーの準備が整っていれば始める.
                    if (count == otherPlayers)
                    {
                        //MC用のキャラインスタンス通知,
                        /*TestInstance(); //親

                        //他プレイヤーに自分のキャラインスタンスを通知.
                        if (otherPlayers != 0)
                        {
                            pView.RPC("SendTestInstanceRPC", PhotonTargets.OthersBuffered);
                        }*/

                        pView.RPC("SetPhazeRPC", PhotonTargets.All, (int)Phase.Instance);
                    }
                }

                break;
            case Phase.Instance:

                TestInstance();

                //MCだけルームの状態を変更させる.
                if (PhotonNetwork.isMasterClient)
                {
                    HashTable hh = new HashTable { { "BS", "Play" } };

                    PhotonNetwork.room.SetCustomProperties(hh);

                    PhotonNetwork.room.IsOpen = true; //Start時に閉めておいたのオープンにさせる.

                }

                phaze = Phase.Play; //インスタンスしたら各々プレイ状態にはいる.

                break;
            case Phase.Play:
                break;
            case Phase.GameOver:
                break;
            case Phase.None:
                break;
        }


    }

    void OnGUI()
    {
        string s = "";
        if (PhotonNetwork.player.IsMasterClient)
            s = "MCです";
        else
            s = "MCではありません";

        GUILayout.Label("私は" + s);
    }


    void TestInstance() //モデルをマニュアルでインスタンス.
    {
        //int id1 = PhotonNetwork.AllocateViewID();

        //pView.RPC("TestInstanceRPC",PhotonTargets.AllBuffered,id1,PhotonNetwork.player);

        PhotonNetwork.Instantiate("TestInstance", Vector3.zero, Quaternion.identity, 0);

    }



    [PunRPC]
    void SetPhazeRPC(int phaze)
    {
        this.phaze = (Phase)phaze;
    }
    //以降下のコードバグあり。コメント化。
    /*[RPC]
    void TestInstanceRPC(int vid,PhotonPlayer pp)
    {
        GameObject go = (GameObject)GameObject.Instantiate(g, Vector3.zero, Quaternion.identity);

        PhotonView p = go.GetComponent<PhotonView>();

        p.viewID = vid;

        p.ownerId = pp.ID;
    }

    [RPC]
    void SendTestInstanceRPC()
    {

        TestInstance();

    }*/

}
