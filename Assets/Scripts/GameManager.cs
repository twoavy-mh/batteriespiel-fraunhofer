using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
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
        if (!skipSerializations)
        {
            if (!PlayerPrefs.GetString("uuid").Empty())
            {
                string playerId = Api.GetPlayerDetails("test", Application.systemLanguage == SystemLanguage.German ? Language.De : Language.En);
                if (playerId != null)
                {
                    Api.ReserializeGamestate(playerId);
                }
                else
                {
                    Debug.Log("Failed to log in");
                }
                SceneManager.LoadScene("MainMenu");
                
            }
            else
            {
                Debug.Log("No player id found");
            }
        }
        else
        {
            /*
             * public bool unlocked;
               public bool finished;
               public int result;
             */
            PlayerDetails p = new PlayerDetails();
            p.language = Language.De;
            p.id = "22c7d1dd-bc2b-4d20-8d96-3903d1282386";
            p.name = "dev";
            p.current3dModel = GameState.Models.Cells;
            p.finishedIntro = true;
            p.totalScore = 0;
            p.results = new[]
            {
                new MicrogameState()
                {
                    game = GameState.Microgames.Microgame1,
                    unlocked = true,
                    finished = true,
                    result = 1
                },
                new MicrogameState()
                {
                    game = GameState.Microgames.Microgame2,
                    unlocked = true,
                    finished = true,
                    result = 1
                },
                new MicrogameState()
                {
                    game = GameState.Microgames.Microgame3,
                    unlocked = true,
                    finished = true,
                    result = 1
                },
                new MicrogameState()
                {
                    game = GameState.Microgames.Microgame4,
                    unlocked = true,
                    finished = true,
                    result = 1
                },
                new MicrogameState()
                {
                    game = GameState.Microgames.Microgame5,
                    unlocked = true,
                    finished = true,
                    result = 1
                },
            };
            GameState.Instance.currentGameState = p;
        }
        //GameState.Instance.Init();
        //GameState.Instance.SetVariableAndSave(ref GameState.Instance.current3dModel, GameState.Models.Cells);
        //Debug.Log(GameState.Instance.current3dModel);
        
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