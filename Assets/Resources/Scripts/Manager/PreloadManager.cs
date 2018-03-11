using System.Collections;
using System.Collections.Generic;
using UnityEngine;



 public class PreloadManager : MonoBehaviour {

	#region Singleton

	private static PreloadManager instance;

	public static PreloadManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (PreloadManager)FindObjectOfType(typeof(PreloadManager));

				if (instance == null)
				{
					Debug.LogError(typeof(PreloadManager) + "is nothing");
				}
			}

			return instance;
		}
	}

	#endregion Singleton


	const float FIRST_LOAD_TIME = 3;
	public const float LOAD_TIME = 5;
	const int DEFAULT_SCENE_NUMBER = 100;
	public int _sceneNumber = 0;
	public float _time = 0;


	// Use this for initialization
	void Start () {
		_time = FIRST_LOAD_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		if ( _time < 0 ) {
			switch ( _sceneNumber ) {
			case 0:
				CSceneManager.SceneMove ( SCENES.TITLE );
				break;
			case 1:
				CSceneManager.SceneMove ( SCENES.STAGESLECT );
				break;
			default:
				break;
			}
			_sceneNumber = DEFAULT_SCENE_NUMBER;
		}
		if ( _time >= 0 ) {
			_time -= Time.deltaTime;
		}
	}
}
