using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalJudge : MonoBehaviour
{

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "player_1_goal")
        {
            ScoreView.player_1_Score++;
            Destroy(gameObject);
            if (ScoreView.player_1_Score != Const.CO.GAME_END_SCORE)
                RoomManager.Instance._createBall = false;
        }
        if (hit.gameObject.tag == "player_2_goal")
        {
           ScoreView.player_2_Score++;
            Destroy(gameObject);
            if (ScoreView.player_2_Score != Const.CO.GAME_END_SCORE)
                RoomManager.Instance._createBall = false;
        }
    }
}
