using UnityEngine;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Helpers;
using Models;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameManager _instance;
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
    private async void Awake()
    {
        //GameState.Instance.Init();
        //GameState.Instance.SetVariableAndSave(ref GameState.Instance.current3dModel, GameState.Models.Cells);
        //Debug.Log(GameState.Instance.current3dModel);
        DontDestroyOnLoad(this);
        _instance = this;

        string playerId = await Api.GetPlayerDetails("test");
        
        if (playerId != null)
        {
            await Api.ReserializeGamestate(playerId);
        }
        else
        {
            Debug.Log("Failed to log in");
        }
        
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