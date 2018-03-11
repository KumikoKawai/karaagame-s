using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class FadeManager : MonoBehaviour
{

    #region Singleton

    private static FadeManager instance;

    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    private float fadeAlpha = 0; // フェード中の透明度

    private bool isFading = false; // フェード中かどうか

    public Color fadeColor = Color.black; // フェード色


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGUI()
    {

        // Fade
        if (this.isFading)
        {
            // 色と透明度を更新して白テクスチャを描画 .
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }



    }

    // 画面遷移 

    // scene シーン名
    // interval 暗転にかかる時間(秒)
    public void LoadScene(int scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }


    /// シーン遷移用コルーチン

    // scene シーン名
    // intreval 暗転にかかる時間(秒)
    private IEnumerator TransScene(int scene, float interval)
    {
        // だんだん暗く .
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        // シーン切替 .
        SceneManager.LoadScene(scene);


        // だんだん明るく .
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }
}