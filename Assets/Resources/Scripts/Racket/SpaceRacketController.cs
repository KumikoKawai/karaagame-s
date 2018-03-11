using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRacketController : MonoBehaviour {

	Rigidbody _rb;
	[SerializeField]private float _speed = 0;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		RacketMove();
        ReCreateBall();
    }

	void RacketMove( ) {
		if (this.gameObject.tag == "Player1") {
			if (Input.GetKey (KeyCode.UpArrow)) {
				_rb.AddForce (0, 0, -_speed, ForceMode.Acceleration);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				_rb.AddForce (0, 0, _speed, ForceMode.Acceleration);
			}
		}
		if (this.gameObject.tag == "Player2") {
			if (Input.GetKey (KeyCode.W)) {
				_rb.AddForce (0, 0, -_speed, ForceMode.Acceleration);
			}
			if (Input.GetKey (KeyCode.S)) {
				_rb.AddForce (0, 0, _speed, ForceMode.Acceleration);
            }
		}

	}

    void ReCreateBall()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a");
            CreateBall.Instance.CCreateBall();
        }
    }
}
