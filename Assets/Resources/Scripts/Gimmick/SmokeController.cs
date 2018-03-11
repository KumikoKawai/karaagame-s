using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour {

    private SpriteRenderer spRenderer;
    private float alphaAdd = 0.01f;

    // Use this for initialization
    void Start () {
        spRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Color color = spRenderer.color;
        color.a += alphaAdd;
        if (color.a <= 0.2f)
        {
            color.a = 0.2f;
            alphaAdd *= -1.0f;
        }
        else if (color.a >= 1.0f)
        {
            color.a = 1.0f;
            alphaAdd *= -1.0f;
        }
        spRenderer.color = color;

        transform.Translate(-0.05f, 0, 0);
        if (transform.position.x < -40.0f)
        {
            transform.position = new Vector3(40.0f, 7.0f, 0.0f);
        }
        
    }
}
