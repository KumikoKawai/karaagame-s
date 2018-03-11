using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanController : MonoBehaviour {

	#region Singleton

	private static OceanController instance;

	public static OceanController Instance
	{
			get
			{
					if (instance == null)
					{
							instance = (OceanController)FindObjectOfType(typeof(OceanController));

							if (instance == null)
							{
									Debug.LogError(typeof(OceanController) + "is nothing");
							}
					}

					return instance;
			}
	}

	public GameObject _prefabSquid;

	private GameObject squid;

	private int nSquidAppearCount;
	private int nSquidLifeCount;
	public bool bSquidAppear;

	#endregion Singleton
	// Use this for initialization
	void Start () {
		nSquidAppearCount = 0;//
		nSquidLifeCount = 0;
		bSquidAppear = false;

		squid = CreateSquid(0.0f, 0.2f, -2.5f);
		squid.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.gameEndFlag == false && CreateBall._createBall == true){
				if (nSquidAppearCount == 300)
				{
						squid.SetActive(true);
						nSquidAppearCount = 0;
				}

				if (!bSquidAppear)//イカが海上に出ていない
						nSquidAppearCount++;//出るまでのカウントを増やす

				if (nSquidLifeCount == 600)//イカが海上に出てから600フレーム後
				{
						squid.SetActive(false);
						bSquidAppear = false;
						nSquidLifeCount = 0;
				}

				if(bSquidAppear)
						nSquidLifeCount++;
		}

	}

	public GameObject CreateSquid(float fx, float fy, float fz)
	{
			return Instantiate(_prefabSquid, new Vector3(fx, fy, fz), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
	}
}
