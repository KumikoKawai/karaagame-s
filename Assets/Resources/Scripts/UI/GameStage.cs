using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStage : MonoBehaviour {

    [SerializeField]
    SCENES nextScene;

   
    public void OnTapNextScene()
    {
        CSceneManager.SceneMove(nextScene);
    }
}
