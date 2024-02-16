using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoTweenKiller : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene s)
    {
        KillAllTweens();
    }
        
    private void KillAllTweens()
    {
        int killedCount = DOTween.KillAll();
        Debug.Log(killedCount);
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
