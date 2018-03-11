using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinderController : MonoBehaviour {

    float fSpeedY;

    // Use this for initialization
    void Start () {
        // 各方向への速度セット
        this.fSpeedY = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        // 噴石の座標更新
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        temp.y = 0.0f;
        transform.position = temp;
        transform.Translate(0, -fSpeedY, 0, Space.World);
        if (transform.position.y < 0.0f)
        {// 噴石の削除
            fSpeedY = 0.0f;
        }
    }
}
