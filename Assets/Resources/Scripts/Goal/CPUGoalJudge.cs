using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUGoalJudge : MonoBehaviour
{
    private Transform player1;
    private Transform player2;
    private Vector3 pos1;
    private Vector3 pos2;

    void Start(){
        player1 = GameObject.Find("Racket").GetComponent<Transform>();
        player2 = GameObject.Find("CPURacket").GetComponent<Transform>();
        pos1 = player1.position;
        pos2 = player2.position;
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "player_1_goal")
        {
            ScoreView.player_1_Score++;
            Destroy(gameObject);
            player1.position = pos1;
            player2.position = pos2;
            if (ScoreView.player_1_Score != Const.CO.GAME_END_SCORE)
                CreateBall._createBall = false;

        }
        if (hit.gameObject.tag == "player_2_goal")
        {
            ScoreView.player_2_Score++;
            Destroy(gameObject);
            player1.position = pos1;
            player2.position = pos2;
            if (ScoreView.player_2_Score != Const.CO.GAME_END_SCORE)
                CreateBall._createBall = false;
        }

    }
}
