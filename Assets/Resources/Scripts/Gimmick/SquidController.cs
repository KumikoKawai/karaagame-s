using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidController : MonoBehaviour {

	// Use this for initialization
	private int nCount = 0;
	private OceanController OceanController;

	void Start(){
		OceanController = GameObject.Find("OceanController").GetComponent<OceanController>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision other)
	{
			if (other.gameObject.name == "CPUBall(Clone)")
			{
				if(nCount == 3){
					gameObject.SetActive(false);
					nCount = 0;
					OceanController.bSquidAppear = false;
				}else{
					nCount++;
				}
			}
	}
}
