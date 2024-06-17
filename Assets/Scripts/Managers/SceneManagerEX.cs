using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : Singleton<SceneManagerEX>
{
    public Define.Scene CurrentScene { get; private set; } = Define.Scene.None;
    public Define.Scene previusScene { get; private set; } = Define.Scene.None;

    private Coroutine coSceneChange;

    public void ChangeScene(Define.Scene nextScene)
    {
        previusScene = CurrentScene;
        CurrentScene = nextScene;

        string sceneName = Enum.GetName(typeof(Define.Scene), nextScene); 
        if (coSceneChange != null)
            StopCoroutine(coSceneChange);

        coSceneChange = StartCoroutine(LoadScene(sceneName));
    }

    public void ChangeScene(Define.Scene nextScene, int index)
    {
        previusScene = CurrentScene;
        CurrentScene = nextScene;

        if(coSceneChange != null)
            StopCoroutine(coSceneChange);

        coSceneChange = StartCoroutine(LoadScene(index));
    }

    public void ChangeScene(Define.Scene nextScene, string name)
    {
        previusScene = CurrentScene;
        CurrentScene = nextScene;

        if (coSceneChange != null)
            StopCoroutine(coSceneChange);

        coSceneChange = StartCoroutine(LoadScene(name));
    }

    private IEnumerator LoadScene(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        LoadingUI loading = UIManager.Instance.ShowPopupUI<LoadingUI>();
        SoundManager.Instance.Stop(Define.Sound.BGM);
        yield return operation;
        if(loading != null)
            UIManager.Instance.ClosePopupUI(loading);
    }

    private IEnumerator LoadScene(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        LoadingUI loading = UIManager.Instance.ShowPopupUI<LoadingUI>();
        SoundManager.Instance.Stop(Define.Sound.BGM);
        yield return operation;

        if (loading != null)
            UIManager.Instance.ClosePopupUI(loading);
    }

    public IEnumerator LoadManagerScene()
    {
        string name = Enum.GetName(typeof(Define.Scene), Define.Scene.DontDestroy);
        AsyncOperation operation =  SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        yield return operation;

        operation = SceneManager.UnloadSceneAsync(name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        yield return operation;
    }
}
