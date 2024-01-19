using UnityEngine;
using Helpers;

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
        GameState.Instance.SetVariableAndSave(ref GameState.Instance.current3dModel, "tree");
        Debug.Log(GameState.Instance.current3dModel);
        DontDestroyOnLoad(this);
        _instance = this;
    }
}