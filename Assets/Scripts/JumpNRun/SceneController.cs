using Events;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class SceneController : MonoBehaviour
{
    private static SceneController _instance;

    public RegenerateEvent regenerateEvent;
    public DecayEvent decayEvent;
    public CollectedEvent collectEvent;
    
    
    public static SceneController Instance
    {
        get
        {
            if (_instance == null) Debug.Log("no SceneController yet");
            return _instance;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        regenerateEvent ??= new RegenerateEvent();
        decayEvent ??= new DecayEvent();
        collectEvent ??= new CollectedEvent();
        _instance = this;
        DataStore.Instance.Init();
    }
}