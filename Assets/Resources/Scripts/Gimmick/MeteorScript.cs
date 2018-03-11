using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour {

    float fSpeedX;
    float fSpeedY;
    float fSpeedZ;

    // Use this for initialization
    void Start () {
        // 各方向への速度セット
        this.fSpeedX = 0.1f * (Random.value - 0.5f);
        this.fSpeedY = 0.1f + 0.05f * Random.value;
        this.fSpeedZ = 0.3f * (Random.value - 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        // 隕石の座標更新
        transform.Translate(fSpeedX, -fSpeedY, fSpeedZ, Space.World);

        if (transform.position.y < -20.0f)
        {// 隕石の削除
            Destroy(gameObject);
        }
    }
}
