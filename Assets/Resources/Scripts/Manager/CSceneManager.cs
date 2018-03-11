using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum SCENES
{
	PROLOAD,
    TITLE,
    STAGESLECT,
    MATTING,
    GAME,
    CPUNORMALSTAGE,
    CPUVOLCANOSTAGE,
		CPUOCEANSTAGE,
}

static public class CSceneManager
{


    static public void SceneMove(SCENES NextScene)
    {

        FadeManager.Instance.LoadScene((int)NextScene, 1.0f);

    }
}
