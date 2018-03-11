using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour {

	public GameObject _text1;
	public GameObject _text2;
	public GameObject _text3;
	public float _time = 0;
	public const float ONE_CYCLE_TIME = 1.2f;

	// Use this for initialization
	void Start () {
		//_text1 = GameObject.Find ("Canvas/Text(1)");
		//_text2 = GameObject.Find ("Canvas/Text(2)");
		//_text3 = GameObject.Find ("Canvas/Text(3)");
		_text1.SetActive ( false );
		_text2.SetActive ( false );
		_text3.SetActive ( false );
		_time = ONE_CYCLE_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		_time -= Time.deltaTime;
		if (0.9 < _time && _time < ONE_CYCLE_TIME) {
			_text1.SetActive (false);
			_text2.SetActive (false);
			_text3.SetActive (false);
		}
		if (0.6 < _time && _time < 0.9) {
			_text1.SetActive (true);
			_text2.SetActive (false);
			_text3.SetActive (false);
		}
		if (0.3 < _time && _time < 0.6) {
			_text1.SetActive (true);
			_text2.SetActive (true);
			_text3.SetActive (false);
		}
		if (0 < _time && _time < 0.3) {
			_text1.SetActive (true);
			_text2.SetActive (true);
			_text3.SetActive (true);
		}
		if (_time <= 0) {
			_time = ONE_CYCLE_TIME;
			_text1.SetActive (false);
			_text2.SetActive (false);
			_text3.SetActive (false);
		}
	}
}
