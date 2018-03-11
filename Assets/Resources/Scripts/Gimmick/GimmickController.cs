using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickController : MonoBehaviour
{
    #region Singleton

    private static GimmickController instance;

    public static GimmickController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GimmickController)FindObjectOfType(typeof(GimmickController));

                if (instance == null)
                {
                    Debug.LogError(typeof(GimmickController) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    public GameObject _prefabMeteor;
    public GameObject _prefabSmoke;
    public GameObject _prefabExplosion;

    private GameObject Smoke1;
    private GameObject Smoke2;

    private int nSmokeCreateCount;
    private int nSmokeLifeCount;
    private bool bSmokeCreate;

    private int nCinderCreateCount;
    private int nCinderLifeCount;
    private bool bCinderCreate;

    public struct SavePos
    {
        public float fx, fy, fz;
        public bool bUse;
        public GameObject CinderObj;
    }

    public SavePos[] savePos = new SavePos[3];

    // Use this for initialization
    void Start()
    {
        nSmokeCreateCount = 0;
        nSmokeLifeCount = 0;
        bSmokeCreate = false;

        nCinderCreateCount = 0;
        bCinderCreate = false;

        for (int i = 0; i < 3; i++)
        {
            savePos[i].fx = 0;
            savePos[i].fy = 0;
            savePos[i].fz = 0;
            savePos[i].bUse = false;
            savePos[i].CinderObj = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameEndFlag == false && CreateBall._createBall == true){
            if (nCinderCreateCount == 800)
            {
                CreateCinder();
                bCinderCreate = true;
                nCinderCreateCount = 0;
            }

            if (nSmokeCreateCount == 600)
            {
                Smoke1 = CreateSmoke(0.0f, 7.0f, 0.0f);
                Smoke2 = CreateSmoke(40.0f, 7.0f, 0.0f);
                bSmokeCreate = true;
                nSmokeCreateCount = 0;
            }

            if (!bSmokeCreate)
                nSmokeCreateCount++;
            if (!bCinderCreate)
                nCinderCreateCount++;
        }

        if (nCinderLifeCount == 600)
        {
            DeleteCinder();
            bCinderCreate = false;
            nCinderLifeCount = 0;
        }

        if (nSmokeLifeCount == 600)
        {
            Destroy(Smoke1);
            Destroy(Smoke2);
            bSmokeCreate = false;
            nSmokeLifeCount = 0;
        }

        if(bSmokeCreate)
            nSmokeLifeCount++;
        if(bCinderCreate)
            nCinderLifeCount++;
    }

    // 噴石生成関数
    public void CreateCinder()
    {
        bool bCreate = false;
        for (int i = 0; i < 3; i++)
        {
            if (!savePos[i].bUse)
            {
                savePos[i].fx = Random.Range(-5.0f, 5.0f);
                savePos[i].fy = 10.0f;
                savePos[i].fz = Random.Range(-3.0f, 3.0f);
                for (int j = 0; j < 3; j++)
                {
                    if (i != j)
                    {
                        if (savePos[j].bUse)
                        {
                            if (HitCircle(savePos[i].fx, savePos[i].fz, 1.0f, savePos[j].fx, savePos[j].fz, 1.0f))
                            {
                                bCreate = false;
                                break;
                            }
                        }
                    }
                    bCreate = true;
                }
                if (bCreate)
                {
                    savePos[i].bUse = true;
                    savePos[i].CinderObj = Instantiate(_prefabMeteor, new Vector3(savePos[i].fx, savePos[i].fy, savePos[i].fz), Quaternion.identity);
                }
                else
                    i--;
            }
        }
    }

    public void DeleteCinder()
    {
        for (int i = 0; i < 3; i++)
        {
            if (savePos[i].bUse)
            {
                GameObject exp = Instantiate(_prefabExplosion, new Vector3(savePos[i].fx, 0.0f, savePos[i].fz), Quaternion.identity);
                Destroy(exp, 3f);
                Destroy(savePos[i].CinderObj);
                savePos[i].fx = 0;
                savePos[i].fy = 0;
                savePos[i].fz = 0;
                savePos[i].bUse = false;
                savePos[i].CinderObj = null;
            }
        }
    }

    // 噴煙生成関数
    public GameObject CreateSmoke(float fx, float fy, float fz)
    {
        return Instantiate(_prefabSmoke, new Vector3(fx, fy, fz), new Quaternion(0.707f, 0.0f, 0.0f, 0.707f));
    }

    // xzの円形当たり判定
    public bool HitCircle(float x0, float z0, float r0, float x1, float z1, float r1)
    {
        float x = x1 - x0;
        float z = z1 - z0;
        float l = x * x + z * z;    // ２乗のままで計算する

        return l < (r0 + r1) * (r0 + r1);
    }
}
