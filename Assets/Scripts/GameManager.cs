using System;
using UnityEngine;
using System.Collections;
using Helpers;
using Models;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameManager _instance;
    public bool skipSerializations = true;
    public int currentJumpAndRunLevel = 0;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("no GameManager yet");
            return _instance;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 30;
#endif
        Debug.Log($"Target frame rate: {Application.targetFrameRate}");
        StartCoroutine(WaitUntilApiIsThere(() => StartGameManager()));
        DontDestroyOnLoad(this);
        _instance = this;

        StartCoroutine(ARSession.CheckAvailability());
        StartCoroutine(AllowARScene());
    }

    private void StartGameManager()
    {
        if (!PlayerPrefs.GetString("uuid").Empty())
        {
            Api.Instance.GetPlayerDetails("test",
                Application.systemLanguage == SystemLanguage.German ? Language.De : Language.En,
                s =>
                {
                    if (s != null)
                    {
                        Api.Instance.ReserializeGamestate(s, details => { SceneManager.LoadScene("MainMenu"); });
                    }
                    else
                    {
                        Debug.Log("Failed to log in");
                    }
                });
        }
        else
        {
            Debug.Log("No player id found");
        }
    }

    private IEnumerator WaitUntilApiIsThere(Action cb)
    {
        yield return new WaitUntil(() => Api.Instance != null);
        cb();
    }

    IEnumerator AllowARScene()
    {
        while (true)
        {
            while (ARSession.state == ARSessionState.CheckingAvailability ||
                   ARSession.state == ARSessionState.None)
            {
                Debug.Log("Waiting...");
                yield return null;
            }

            if (ARSession.state == ARSessionState.Unsupported)
            {
                GameState.Instance.SetVariableAndSave(ref GameState.Instance.arAvailable, false);
                Debug.Log("AR unsupported");
                yield break;
            }

            if (ARSession.state > ARSessionState.CheckingAvailability)
            {
                GameState.Instance.SetVariableAndSave(ref GameState.Instance.arAvailable, true);
                Debug.Log("AR supported");
                yield break;
            }
        }
    }
}