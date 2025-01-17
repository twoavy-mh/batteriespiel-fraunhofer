using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Events;
using Fair;
using Helpers;
using Models;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ApiErrorEvent.IUseApiError
{
    private static GameManager _instance;
    public bool skipSerializations = true;
    public int currentJumpAndRunLevel = 0;
    public string lastScene = "";
    public string currentScene = "";
    public bool arPopupShown = false;

    public ApiErrorEvent apiErrorEvent;
    public FairChanged fairChangedEvent;
    private GameObject _errorPrefab;

    private List<Exception> _errorStack = new List<Exception>();
    private GameObject _errorLogInstance = null;

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
        _errorPrefab = Resources.Load<GameObject>("Prefabs/UI/ErrorLog");
        apiErrorEvent ??= new ApiErrorEvent();
        fairChangedEvent ??= new FairChanged();
        apiErrorEvent.AddListener(UseApiError);
#if UNITY_ANDROID
        Application.targetFrameRate = 60;
#endif
        Debug.Log($"Target frame rate: {Application.targetFrameRate}");
        StartCoroutine(WaitUntilApiIsThere(StartGameManager));
        DontDestroyOnLoad(this);
        _instance = this;

        if (SystemInfo.operatingSystem.Contains("Windows") || Application.isEditor)
        {
            gameObject.AddComponent<IdleTimer>();
        }
        
        StartCoroutine(ARSession.CheckAvailability());
        StartCoroutine(AllowARScene());
    }

    private void StartGameManager()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        
        if (!PlayerPrefs.GetString("uuid").Empty())
        {
            try
            {
                Api.Instance.GetPlayerDetails("test",
                    Application.systemLanguage == SystemLanguage.German ? Language.De : Language.En,
                    s =>
                    {
                        if (s != null)
                        {
                            Api.Instance.ReserializeGamestate(s, details =>
                            {
                                Debug.Log("------");
                                Debug.Log(GameState.Instance.GetCurrentMicrogame());
                                Debug.Log("------");
                                SceneManager.LoadScene("MainMenu");
                            });
                        }
                        else
                        {
                            Debug.Log("Failed to log in");
                        }
                    });
            }
            catch (Exception e)
            {
                Debug.Log("backend probbaly not reachable");
            }
        }
        else
        {
            Debug.Log("No player id found");
        }
    }

    private IEnumerator WaitUntilApiIsThere(Action cb)
    {
        yield return new WaitUntil(() => Api.Instance != null);
#if UNITY_WEBGL
        yield return LocalizationSettings.InitializationOperation;
        yield return LocalizationSettings.StringDatabase.PreloadOperation;
#endif
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

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (currentScene == "")
        {
            currentScene = next.name;
        }
        else if (currentScene != next.name)
        {
            lastScene = currentScene;
            currentScene = next.name;
        }
    }

    public void UseApiError(Exception e)
    {
        if (_errorLogInstance == null)
        {
            _errorLogInstance = Instantiate(_errorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            DontDestroyOnLoad(_errorLogInstance);
        }
        _errorLogInstance.SetActive(true);
        StartCoroutine(_errorLogInstance.transform.GetChild(0).GetChild(0).GetComponent<ErrorBuilder>()
            .WaitASec(e));
        StartCoroutine(AutoCloseError());
    }

    private IEnumerator AutoCloseError()
    {
        yield return new WaitForSeconds(3);
        ClearError();
    }

    public void ClearError()
    {
        Debug.Log("inactivating 0");
        if (_errorLogInstance != null)
        {
            Debug.Log("inactivating 1");
            _errorLogInstance.SetActive(false);
        }
    }
}