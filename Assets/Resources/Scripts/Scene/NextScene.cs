using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{

    [SerializeField]
    SCENES nextScene = 0;

    public void mOnTap()
    {
        CSceneManager.SceneMove(nextScene);
    }

	public void ReturnTitle() {
		PreloadManager.Instance._sceneNumber = 0;
		PreloadManager.Instance._time = PreloadManager.LOAD_TIME;
		CSceneManager.SceneMove(nextScene);
	}

	public void ReturnStageSelect() {
		PreloadManager.Instance._sceneNumber = 1;
		PreloadManager.Instance._time = 5;
		CSceneManager.SceneMove(nextScene);
	}

}
