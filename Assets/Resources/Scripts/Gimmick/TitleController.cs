using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	private GameObject _startButton;
	private GameObject _battlePanel;

	// Use this for initialization
	void Start () {
		_startButton = GameObject.Find ("StartButton");
		_battlePanel = GameObject.Find ( "BattlePanel" );
		_startButton.SetActive (true);
		_battlePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTapStartButton( ) {
		_startButton.SetActive (false);
		_battlePanel.SetActive ( true );
	}

/*
 //以下は使用しません
	static bool _playerBattleFlag;
	public void OnTapBattleButton( int number ) {
		switch (number) {
		case 0:
			//SceneManager.LoadScene ("StageSelect");
			break;
		case 1:
			_playerBattleFlag = true;
			//SceneManager.LoadScene ("StageSelect");
			break;
		default:
			break;
		}
	}
*/


}
