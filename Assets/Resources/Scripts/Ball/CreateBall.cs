using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBall : MonoBehaviour {

    #region Singleton

    private static CreateBall instance;

    public static CreateBall Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (CreateBall)FindObjectOfType(typeof(CreateBall));

                if (instance == null)
                {
                    Debug.LogError(typeof(CreateBall) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _root;

    public static bool _createBall = true;

    public void CCreateBall()
    {
        if (_createBall == false)
        {
            // プレハブ生成
            Instantiate(_prefab, _root);
            _createBall = true;
        }
    }
}
