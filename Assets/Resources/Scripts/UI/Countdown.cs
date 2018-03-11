using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    [SerializeField]
    private Text _textCountDown;

	void Start () {
        _textCountDown.text = "";
        StartCoroutine(CountDownCoroutine());
	}



    IEnumerator CountDownCoroutine()
    {
            _textCountDown.gameObject.SetActive(true);

            _textCountDown.text = "3";
            yield return new WaitForSeconds(1.0f);

            _textCountDown.text = "2";
            yield return new WaitForSeconds(1.0f);

            _textCountDown.text = "1";
            yield return new WaitForSeconds(1.0f);

            _textCountDown.text = "GO!";
            yield return new WaitForSeconds(1.0f);

            _textCountDown.text = "";
            _textCountDown.gameObject.SetActive(false);
           	//RoomManager.Instance.BallMake();
    }
}
