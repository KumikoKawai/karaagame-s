using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPURacketController : MonoBehaviour
{//private GameObject Racket1;
	private GameObject Floor;
	private Vector3 screenPoint;

	//スワイプ計算
	private int Height_s_max;
	private int Height_s_min;
	private float Height_f;
	private float delta;
	private float prev_y;

	#region PRIVATE_MEMBERS //タップ判定
	private const float DOUBLE_TAP_MAX_DELAY = 0.5f;//seconds
	private float mTimeSinceLastTap = 0;
	protected int mTapCount = 0;
	#endregion //PRIVATE_MEMBERS

	///////////
	private float minSwipeDistX;
  private float minSwipeDistY;
	private Vector2 startPos;
	private Vector2 endPos;
	float speed = 0.05f;
	///////////

	// Use this for initialization
	void Start () {
		Floor = GameObject.Find("floor");
		Height_s_max = Screen.height*190/354;
		Height_s_min = Screen.height*50/354;
		Height_f = Floor.GetComponent<Renderer>().bounds.size.z;
		delta = Height_f/Height_s_max;

		mTapCount = 0;
		mTimeSinceLastTap = 0;

		minSwipeDistX = 10;
		minSwipeDistY = 5;
	}

	// Update is called once per frame
	void Update (){
		if (transform.position.z > Height_f/2 - 2) {
			transform.position = new Vector3(transform.position.x, transform.position.y, Height_f/2 -2.5f);
		}
		if (transform.position.z < -Height_f/2 + 2) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -Height_f/2 +2.5f);
		}
		if (Input.touchCount > 0){
			//タッチを取得
		  Touch touch = Input.touches [0];
		  //タッチフェーズによって場合分け
		  switch (touch.phase) {
				//タッチ開始時
		    case TouchPhase.Began:
		      startPos = touch.position;
		      break;
				//タッチ終了時
		    case TouchPhase.Moved:
		      endPos = new Vector2 (touch.position.x, touch.position.y);
		      float swipeDistX = (new Vector3 (endPos.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
		      float swipeDistY = (new Vector3 (0, endPos.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;

		      if (swipeDistX > swipeDistY && swipeDistX > minSwipeDistX)
		      {
		        float SignValueX = Mathf.Sign (endPos.x - startPos.x);
		        if (SignValueX > 0)
		        {
		          //右方向にスワイプしたとき
		        } else if (SignValueX < 0)
		        {
		          //左方向にスワイプしたとき
		        }
		      } else if (swipeDistY > minSwipeDistY)
		      {
		        float SignValueY = Mathf.Sign (endPos.y - startPos.y);
		        if (SignValueY > 0)
		        {
		          //上方向にスワイプしたとき
							Debug.Log("Up");
		        } else if (SignValueY < 0)
		        {
		          //下方向にスワイプしたとき
							Debug.Log("Down");
							//Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
							//transform.Translate(0, 0, -touchDeltaPosition.y * speed);
		        }
						if(endPos.y < Height_s_max && endPos.y > Height_s_min){
							float move = (endPos.y - startPos.y)*delta;
							Vector3 temp = transform.position;
							temp.z -= move;
							transform.position = temp;
							startPos = endPos;
						}
		      }
		      if (swipeDistX < minSwipeDistX && swipeDistY < minSwipeDistY)
		      {
		          // タップした時
		      }
		      break;
		    }
		  }
		HandleTap();
	}


	private void HandleTap ()
	{
		if (mTapCount == 1)
		{
			mTimeSinceLastTap += Time.deltaTime;
			if (mTimeSinceLastTap > DOUBLE_TAP_MAX_DELAY)
			{
				OnSingleTapConfirmed();
				mTapCount = 0;
				mTimeSinceLastTap = 0;
			}
		}
		else if (mTapCount == 2)
		{
			OnDoubleTap();
			mTimeSinceLastTap = 0;
			mTapCount = 0;
		}

		if (Input.GetMouseButtonUp(0))
		{
			mTapCount++;
			if (mTapCount == 1)
			{
				OnSingleTap();
			}
		}
	}

	protected virtual void OnSingleTap() { }

	protected virtual void OnSingleTapConfirmed(){	}

	protected virtual void OnDoubleTap()
	{
			ReCreateBall();
	}

	void ReCreateBall(){
		CreateBall.Instance.CCreateBall();
	}
}
