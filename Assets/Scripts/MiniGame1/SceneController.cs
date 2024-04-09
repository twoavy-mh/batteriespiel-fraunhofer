using System;
using System.Collections.Generic;
using System.Linq;
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

        public NonInvestedController nonInvestedController;

        public MicrogameFinishedEvent microgameFinishedEvent;

        private bool _finished = false;

        private int _desiredValue = 3444;
        private int _startCapital = 6000;

        public int startCapital
        {
            get { return _startCapital; }
            private set { _startCapital = value; }
        }

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

            videoPlayer.loopPointReached += LoopPointReached;
        }

        private void Update()
        {
            if (!_finished)
            {
                if (stateButtons.All(x => x.Finished))
                {
                    SetEndScreen();
                }
            }
        }

        private void UpdateGame(int score, Action<bool> callback)
        {
            try
            {
                StartCoroutine(Api.Instance.SetGame(
                    new MicrogameState()
                    {
                        unlocked = true, finished = true, result = score, game = GameState.Microgames.Microgame1,
                        jumpAndRunResult = GameState.Instance.currentGameState.results[0].jumpAndRunResult
                    },
                    GameState.Instance.currentGameState.id, details =>
                    {
                        GameState.Instance.currentGameState = details;
                        callback(true);
                    }));
            }
            catch (Exception)
            {
                callback(false);
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

        private int CalculateScore(int sumBought)
        {
            int sumNeeds = stateButtons[0].needs + stateButtons[1].needs + stateButtons[2].needs;

            int desiredRemainingCapital = _startCapital - _desiredValue;

            float resources = (Math.Min(sumBought, sumNeeds) / (float)sumNeeds) * 40;
            float capital = (Math.Min(nonInvestedController.currentMoney, desiredRemainingCapital) /
                             (float)desiredRemainingCapital) * 60;

            int score = (int)Math.Floor(capital + resources);
            return score;
        }

        private void LoopPointReached(VideoPlayer vp)
        {
            SetEndScreen();
        }

        private void SetEndScreen()
        {
            _finished = true;

            int playedRounds = videoPlayer.GetComponentInChildren<Timeslot>().GetPlayedRounds();
            videoPlayer.Stop();
            microgameFinishedEvent.Invoke(GameState.Microgames.Microgame1);
            int boughtInTotal = stateButtons[0].GetBought() + stateButtons[1].GetBought() +
                                stateButtons[2].GetBought();
            int spent = _startCapital - nonInvestedController.currentMoney;
            int score = CalculateScore(boughtInTotal);

            if (GameState.Instance.currentGameState.results.Length == 0)
            {
                UpdateGame(score, b =>
                {
                    if (!b)
                    {
                        Debug.Log("something went wrong");
                    }
                });
            }
            else
            {
                if (score > GameState.Instance.currentGameState.results[0].result)
                {
                    UpdateGame(score, b =>
                    {
                        if (!b)
                        {
                            Debug.Log("something went wrong");
                        }
                    });
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
                        { "_", playedRounds.ToString() },
                        { "=", score.ToString() }
                    });
            }
            else
            {
                mobileModal.SetActive(true);
                mobileModal.GetComponentInChildren<ProgressRingController>().StartAnimation(score);
                Utility.GetTranslatedText(
                    score > 60 ? "microgame_1_finished_text_good" : "microgame_1_finished_text_bad",
                    s => mobileModal.transform.Find("Body").GetComponent<TMP_Text>().text = s,
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

        public bool GetFinished()
        {
            return _finished;
        }
    }
}