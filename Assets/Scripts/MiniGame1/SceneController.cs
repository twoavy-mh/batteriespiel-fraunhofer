using System;
using System.Linq;
using System.Threading.Tasks;
using Events;
using Helpers;
using Minigame1.Classes;
using Models;
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
            _instance = this;
            gameObject.AddComponent<AutoTweenKiller>();
        }

        private async void Update()
        {
            if (!_finished)
            {
                if (stateButtons.All(x => x.Finished))
                {
                    _finished = true;

                    int score = CalculateScore(stateButtons[0].needs, stateButtons[0].GetBought(),
                        stateButtons[1].needs, stateButtons[1].GetBought(),
                        stateButtons[2].needs, stateButtons[2].GetBought(), 20000,
                        AveragePricePerPurchasable("nickle"), AveragePricePerPurchasable("lithium"),
                        AveragePricePerPurchasable("cobalt"));

                    if (GameState.Instance.currentGameState.results.Length == 0)
                    {
                        await UpdateGame(score);
                    }
                    else
                    {
                        if (score > GameState.Instance.currentGameState.results[0].result)
                        {
                            await UpdateGame(score);
                        }
                    }

                    GameObject.Find("Video Player").GetComponent<VideoPlayer>().Pause();

                    if (Utility.GetDevice() == Device.Desktop)
                    {
                        desktopModal.SetActive(true);
                        desktopModal.GetComponentInChildren<ProgressRingController>().StartAnimation(score);
                    }
                    else
                    {
                        mobileModal.SetActive(true);
                        mobileModal.GetComponentInChildren<ProgressRingController>().StartAnimation(score);
                    }

                    GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(() =>
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name));
                    GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(() =>
                        SceneManager.LoadScene("MainMenu"));
                }
            }
        }

        private async Task<bool> UpdateGame(int score)
        {
            try
            {
                PlayerDetails p = await Api.SetGame(
                    new MicrogameState()
                        { unlocked = true, finished = true, result = score, game = GameState.Microgames.Microgame1 },
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
    }
}