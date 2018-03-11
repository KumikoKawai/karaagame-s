using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Text _text;
    public static bool gameEndFlag;

    private GameObject _titleButton;
    private GameObject _stageSelectButton;
    private GameObject _RestartButton;

    void Start()
    {
        _text.text = "";
        gameEndFlag = false;

        _titleButton = GameObject.Find("TitleButton");
        _stageSelectButton = GameObject.Find("StageSelectButton");
        _RestartButton = GameObject.Find("ReStartButton");
				Debug.Log(_titleButton);
        _titleButton.SetActive(false);
        _stageSelectButton.SetActive(false);
        _RestartButton.SetActive(false);
    }

    void Update()
    {
        if (!gameEndFlag)
        {
            if (ScoreView.player_1_Score == Const.CO.GAME_END_SCORE)
            {
                StartCoroutine(GameEndCoroutine());
                gameEndFlag = true;
            }
            if (ScoreView.player_2_Score == Const.CO.GAME_END_SCORE)
            {
                StartCoroutine(GameEndCoroutine());
                gameEndFlag = true;
            }
        }
    }

    IEnumerator GameEndCoroutine()
    {

        _text.gameObject.SetActive(true);

        _text.text = "Finish !";
        yield return new WaitForSeconds(3.0f);

        if(ScoreView.player_1_Score == Const.CO.GAME_END_SCORE)
            _text.text = "Player2 WIN !";
        else
            _text.text = "Player1 WIN !";

        yield return new WaitForSeconds(3.0f);

        _text.text = "";
        _text.gameObject.SetActive(false);
        _titleButton.SetActive(true);
        _stageSelectButton.SetActive(true);
        _RestartButton.SetActive(true);
    }
}
