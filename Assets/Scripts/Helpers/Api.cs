using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Models;
using UnityEngine;
using UnityEngine.Networking;

namespace Helpers
{
    public class Api : MonoBehaviour
    {
        
        private static Api _instance;
        
        public static Api Instance
        {
            get
            {
                if (_instance == null) return null;
                return _instance;
            }
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        private static UnityWebRequest GetBaseRequest(string url, string method, string content)
        {
            UnityWebRequest request;
            switch (method)
            {
                case "GET":
                    request = UnityWebRequest.Get(url);
                    break;
                case "POST":
                    request = UnityWebRequest.PostWwwForm(url, content);
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    request.uploadHandler = new UploadHandlerRaw(bytes);
                    request.downloadHandler = new DownloadHandlerBuffer();
                    break;
                default:
                    request = UnityWebRequest.Get(url);
                    break;
            }

            request.SetRequestHeader("X-DIRECT", "y6biadzsv3t58kv2t8");
            if (!PlayerPrefs.GetString("bearer").Empty())
            {
                request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("bearer"));
            }

            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }

        public void ToggleLanguage(Language newLanguage, Action<PlayerDetails> callback)
        {
            PlayerDetails p = GameState.Instance.currentGameState;
            p.language = newLanguage;
            StartCoroutine(UpdatePlayer(p, details =>
            {
                callback(details);
            }));
        }

        private IEnumerator RegisterPlayer(string name, Language language, Action<PlayerRegistration> callback)
        {
            Debug.Log("Registering");
            string requestString = JsonUtility.ToJson(new PlayerRegistrationRequest(name, language));
            UnityWebRequest request = GetBaseRequest("https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users",
                "POST", requestString);
            yield return request.SendWebRequest();

            string v = request.downloadHandler.text;

            callback((PlayerRegistration)v);
        }

        private IEnumerator FetchPlayerDetails(string uuid, Action<PlayerDetails> callback)
        {
            UnityWebRequest request =
                GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{uuid}", "GET", null);
            yield return request.SendWebRequest();

            string v = request.downloadHandler.text;
            callback((PlayerDetails)v);
        }

        private IEnumerator UpdatePlayer(PlayerDetails newDetails, Action<PlayerDetails> callback)
        {
            string requestString = JsonUtility.ToJson(newDetails);
            UnityWebRequest request = GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users",
                "POST", requestString);


            yield return request.SendWebRequest();


            string v = request.downloadHandler.text;
            try
            {
                callback((PlayerRegistration)v);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            callback(null);
        }

        public IEnumerator SetGame(MicrogameState m, string playerId, Action<PlayerDetails> callback)
        {
            string requestString = JsonUtility.ToJson(m);
            UnityWebRequest request =
                GetBaseRequest(
                    $"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{playerId}/battery-results",
                    "POST", requestString);


            yield return request.SendWebRequest();
            
            string v = request.downloadHandler.text;
            try
            {
                StartCoroutine(FetchPlayerDetails(playerId, details =>
                {
                    callback(details);
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            callback(null);
        }

        public void ReserializeGamestate(string uuid, Action<PlayerDetails> callback = null)
        {
            try
            {
                StartCoroutine(FetchPlayerDetails(uuid, details =>
                {
                    if (details != null)
                    {
                        GameState.Instance.currentGameState = details;
                        callback?.Invoke(details);
                    }    
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void GetPlayerDetails(string playerName, Language language, Action<string> callback)
        {
            string bearer = PlayerPrefs.GetString("bearer", null);
            string uuid = PlayerPrefs.GetString("uuid", null);
            if (!bearer.Empty() && !uuid.Empty())
            {
                try
                {
                    StartCoroutine(FetchPlayerDetails(uuid, details =>
                    {
                        callback(details.id);
                    }));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                callback(null);
            }
            else
            {
                try
                {
                    StartCoroutine(RegisterPlayer(playerName, language, pr =>
                    {
                        if (pr != null)
                        {
                            PlayerPrefs.SetString("bearer", pr.token);
                            PlayerPrefs.SetString("uuid", pr.id);
                            PlayerPrefs.Save();
                            callback(pr.id);
                        }
                    }));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                callback(null);
            }
        }

        public IEnumerator GetLeaderboard(string uuid, Action<LeaderboardArray> callback)
        {
            UnityWebRequest request =
                GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{uuid}/leaderboard",
                    "GET", null);

            yield return request.SendWebRequest();


            string v = request.downloadHandler.text;
            try
            {
                callback((LeaderboardArray)v);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            callback(null);
        }
    }
}