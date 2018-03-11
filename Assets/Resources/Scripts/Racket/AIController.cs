using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
	 //private GameObject Racket1;
	private GameObject Floor;
	private GameObject Ball;//position用

	private int Height_s_max;
	private int Height_s_min;
	private float Height_f;
	private float prev_y;

	private Vector3 move = Vector3.zero;
	private Vector3 move_prev = Vector3.zero;
	float speed = 8.0f;
	int FlameCount = 1;
	float rand = 1;

	// Use this for initialization
  void Start()
	{
		Floor = GameObject.Find("floor");
		Height_f = Floor.GetComponent<Renderer>().bounds.size.z;
	}

	// Update is called once per frame
	void Update()
	{
		//CPURacketの移動制御
		if(GameObject.Find("CPUBall(Clone)") != null) {
			Ball = GameObject.Find("CPUBall(Clone)");
			float d =  Ball.transform.position.z - transform.position.z;

			if(FlameCount	% 2000 == 1){
				rand = Random.Range(2, 40);
			}else{
				rand = 1;
			}

			if(rand == 2 || rand == 3 || rand == 4){
				if(d > 0){
	       	move.z = speed * Mathf.Min(d, 2.0f);
					move_prev = move;
  	   	}
	  		else if(d < 0){
		     	move.z = -(speed * Mathf.Min(-d, 2.0f));
					move_prev = move;
		   	}
				else{
					move.z = 0;
				}
			}
			else if(rand == 5){
 		 		if(d > 0){
		  		move.z = speed * Mathf.Min(d, -1.0f);
					move_prev = move;
				}
		    else if(d < 0){
		     	move.z = -(speed * Mathf.Min(-d, -1.0f));
					move_prev = move;
	    	}
			}else{
				move = move_prev;
			}
			transform.position += move * Time.deltaTime;
		}

		if (transform.position.z > Height_f / 2 - 2){
			transform.position = new Vector3(transform.position.x, transform.position.y, Height_f / 2 - 2);
		}
		if (transform.position.z < -Height_f / 2 + 2){
				transform.position = new Vector3(transform.position.x, transform.position.y, -Height_f / 2 + 2);
	  }
	}
}
