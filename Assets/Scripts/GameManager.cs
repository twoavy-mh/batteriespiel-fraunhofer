using UnityEngine;
using System.Collections;
using Helpers;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameManager _instance;
        
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
        GameState.Instance.Init();
        GameState.Instance.SetVariableAndSave(ref GameState.Instance.current3dModel,  GameState.Models.Cells);
        Debug.Log(GameState.Instance.current3dModel);
        DontDestroyOnLoad(this);
        _instance = this;
        
        StartCoroutine(ARSession.CheckAvailability());
        StartCoroutine(AllowARScene());
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