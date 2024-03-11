using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events;
using Helpers;
using Minigame1.Classes;
using Models;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Minigame1
{
    public class SceneController : MonoBehaviour
    {
        public ShowWhatYouBuyEvent showWhatYouBuyEvent;
        public BrokerBuyEvent brokerBuyEvent;
        public MoneySpentEvent moneySpentEvent;
        private static SceneController _instance;

        public StateButton[] stateButtons;
        public GameObject mobileModal;
        public GameObject desktopModal;
        public VideoPlayer videoPlayer;

        public MicrogameFinishedEvent microgameFinishedEvent;

        private bool _finished = false;

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
            showWhatYouBuyEvent ??= new ShowWhatYouBuyEvent();
            brokerBuyEvent ??= new BrokerBuyEvent();
            moneySpentEvent ??= new MoneySpentEvent();
            microgameFinishedEvent ??= new MicrogameFinishedEvent();
            _instance = this;
            gameObject.AddComponent<AutoTweenKiller>();
        }

        private void Update()
        {
            if (!_finished)
            {
                if (stateButtons.All(x => x.Finished))
                {
                    _finished = true;
                    videoPlayer.Stop();
                    microgameFinishedEvent.Invoke(GameState.Microgames.Microgame1);
                    int boughtInTotal = stateButtons[0].GetBought() + stateButtons[1].GetBought() +
                                        stateButtons[2].GetBought();
                    int startCapital = 20000;
                    int capitalNow = startCapital - boughtInTotal;
                    int spent = startCapital - capitalNow;
                    int score = CalculateScore(stateButtons[0].needs, stateButtons[0].GetBought(),
                        stateButtons[1].needs, stateButtons[1].GetBought(),
                        stateButtons[2].needs, stateButtons[2].GetBought(), 20000,
                        AveragePricePerPurchasable("nickle"), AveragePricePerPurchasable("lithium"),
                        AveragePricePerPurchasable("cobalt"));

                    if (GameState.Instance.currentGameState.results.Length == 0)
                    {
                        UpdateGame(score);
                    }
                    else
                    {
                        if (score > GameState.Instance.currentGameState.results[0].result)
                        {
                           UpdateGame(score);
                        }
                    }

                    if (Utility.GetDevice() == Device.Desktop)
                    {
                        desktopModal.SetActive(true);
                        desktopModal.GetComponentInChildren<ProgressRingController>().StartAnimation(score);
                        Utility.GetTranslatedText(
                            score > 60 ? "microgame_1_finished_text_good" : "microgame_1_finished_text_bad",
                            s => desktopModal.transform.Find("Body").GetComponent<TMP_Text>().text = s,
                            new Dictionary<string, string>()
                            {
                                { "~", boughtInTotal.ToString() },
                                { "#", spent.ToString() },
                                { "_", "playedRounds" },
                                { "=", score.ToString() }
                            });
                    }
                    else
                    {
                        mobileModal.SetActive(true);
                        mobileModal.GetComponentInChildren<ProgressRingController>().StartAnimation(score);
                        Utility.GetTranslatedText(
                            score > 60 ? "microgame_1_finished_text_good" : "microgame_1_finished_text_bad",
                            s => desktopModal.transform.Find("Body").GetComponent<TMP_Text>().text = s,
                            new Dictionary<string, string>()
                            {
                                { "~", boughtInTotal.ToString() },
                                { "#", spent.ToString() },
                                { "_", "playedRounds" },
                                { "=", score.ToString() }
                            });
                    }

                    GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(() =>
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name));
                    GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(() =>
                        SceneManager.LoadScene("MainMenu"));
                }
            }
        }

        private bool UpdateGame(int score)
        {
            try
            {
                PlayerDetails p = Api.SetGame(
                    new MicrogameState()
                    {
                        unlocked = true, finished = true, result = score, game = GameState.Microgames.Microgame1,
                        jumpAndRunResult = GameState.Instance.currentGameState.results[0].jumpAndRunResult
                    },
                    GameState.Instance.currentGameState.id);
                GameState.Instance.currentGameState = p;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int AveragePricePerPurchasable(string key)
        {
            Timeslot s = GameObject.Find("Video Player").GetComponent<Timeslot>();
            int sum = 0;
            foreach (TimeslotEntry timeslotEntry in s.GetBrokerDay())
            {
                if (key == "nickle") sum += timeslotEntry.resourceInfo.nicklePrice;
                else if (key == "lithium") sum += timeslotEntry.resourceInfo.lithiumPrice;
                else if (key == "cobalt") sum += timeslotEntry.resourceInfo.cobaltPrice;
            }

            int avg = sum / s.GetBrokerDay().Count;
            Debug.Log(avg);
            return avg;
        }

        private int CalculateScore(int requiredNickle, int boughtNickle, int requiredLithium, int boughtLithium,
            int requiredCobalt, int boughtCobalt, int startCapital, int nickelPrice, int lithiumPrice, int cobaltPrice)
        {
            int desiredValue = (requiredNickle * nickelPrice) + (requiredLithium * lithiumPrice) +
                               (requiredCobalt * cobaltPrice);
            int purchasedValue = (boughtNickle * nickelPrice) + (boughtLithium * lithiumPrice) +
                                 (boughtCobalt * cobaltPrice);
            int remainingCapital = startCapital - purchasedValue;

            float f = (purchasedValue / (float)desiredValue) * 50;
            float g = (remainingCapital / (float)startCapital) * 50;

            int score = (int)Math.Floor(f + g);
            return score;
        }

        public bool GetFinished()
        {
            return _finished;
        }
    }
}