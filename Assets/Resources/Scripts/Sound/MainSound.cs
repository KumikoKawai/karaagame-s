using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour {

	public bool DontDestroyEnabled = true;
	public GameObject _go;
	public GameObject _go2;

	// Use this for initialization
	void Start () {
		if ( DontDestroyEnabled ) {
			DontDestroyOnLoad (this);
		}
		_go = this.gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		_go2 = GameObject.Find ("Audio Source");
		if (_go2.tag == "GameSound") {
			ChangeDestroyEnabled ();
		}
	}

	public void ChangeDestroyEnabled() {
		Destroy ( _go );
	}
}
