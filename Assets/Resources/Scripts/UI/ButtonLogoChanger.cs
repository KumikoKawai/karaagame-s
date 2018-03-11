using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogoChanger : MonoBehaviour {
	
	public Button _button;
	public GameObject _buttonImage;
	public GameObject _buttonImage2;
	public string buttonName;
	public string buttonImageName;
	public string buttonImageName2;

	// Use this for initialization
	void Start () {
		_button = GameObject.Find ( buttonName ).GetComponent<Button>();
		_buttonImage = GameObject.Find ( buttonImageName );
		_buttonImage2 = GameObject.Find ( buttonImageName2 );
		_buttonImage.SetActive ( true );
		_buttonImage2.SetActive ( false );

	}
	// Update is called once per frame
	void Update () {
		if (/*Input.GetMouseButtonDown(0) &&*/ getClickObject( ) == _button ) {
			_buttonImage.SetActive (false);
			_buttonImage2.SetActive (true);
		} 

		
	}

	// 左クリックしたオブジェクトを取得する関数(3D)
	public GameObject getClickObject() {
		GameObject result = null;
		// 左クリックされた場所のオブジェクトを取得
		if(Input.GetMouseButtonDown(0)) {
			　　　Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			　　　RaycastHit hit = new RaycastHit();
			Debug.Log ( "左クリック押したな！" );
			　　　if (Physics.Raycast(ray, out hit)){
				　　　　　result = hit.collider.gameObject;
					Debug.Log ( result + "を検知しました" );
				　　　}
			Debug.Log ( result + "を検知しました" );
			　}
		　return result;
	}


	public void OnTapNormalButton( ) {
		
		_buttonImage.SetActive ( false );
		_buttonImage2.SetActive ( true );

	}



}
