using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private string _sceneToOpen;

    public void OnButtonClick()
    {
        StartCoroutine(LoadTheScene());
    }

    private IEnumerator LoadTheScene()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(_sceneToOpen);
        while (!loadScene.isDone)
        {
            yield return null;
        }
    }
}
