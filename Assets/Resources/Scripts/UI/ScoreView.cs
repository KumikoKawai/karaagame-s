using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {

    // スコア格納用変数
    public static int player_1_Score = 0;
    public static int player_2_Score = 0;

    [SerializeField]
    private int player_ID;

    void Start()
    {
        player_1_Score = 0;
        player_2_Score = 0;
    }

    // Update is called once per frame
    void Update () {

		if(player_ID == 1)
            this.GetComponent<Text>().text = player_1_Score + "点";

        if(player_ID == 2)
            this.GetComponent<Text>().text = player_2_Score + "点";

    }
}
