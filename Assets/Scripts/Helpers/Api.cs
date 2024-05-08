using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Models;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
            if (PlayerPrefs.GetInt("fairCode", -1) != -1)
            {
                request.SetRequestHeader("Fair-Code", PlayerPrefs.GetInt("fairCode").ToString());
            }
            if (!PlayerPrefs.GetString("bearer").Empty())
            {
                request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("bearer"));
            }

            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }

        private IEnumerator RegisterPlayer(string name, Language language, Action<PlayerRegistration> callback)
        {
            Debug.Log("Registering");
            string requestString = JsonUtility.ToJson(new PlayerRegistrationRequest(name, language));
            UnityWebRequest request = GetBaseRequest("https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users",
                "POST", requestString);
            yield return request.SendWebRequest();

            string v = request.downloadHandler.text;
            Debug.Log(v);

            callback((PlayerRegistration)v);
        }

        private IEnumerator FetchPlayerDetails(string uuid, Action<PlayerDetails> callback)
        {
            UnityWebRequest request =
                GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{uuid}", "GET", null);
            request.timeout = 10;
            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                if (request.responseCode == 410)
                {
                    GameManager.Instance.apiErrorEvent.Invoke(new Exception("Player not found"));
                    PlayerPrefs.DeleteAll();
                    SceneManager.LoadScene("Onboarding");
                }
                else
                {
                    GameManager.Instance.apiErrorEvent.Invoke(new Exception(request.error));    
                }
                yield break;
            }
            
            string v = request.downloadHandler.text;
            Debug.Log(v);
            callback((PlayerDetails)v);
        }

        public IEnumerator UpdatePlayer(PlayerDetails newDetails, Action<PlayerDetails> callback)
        {
            string requestString = JsonUtility.ToJson((PlayerUpdateDetails)newDetails);
            Debug.Log(requestString);
            UnityWebRequest request = GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users",
                "POST", requestString);


            yield return request.SendWebRequest();


            string v = request.downloadHandler.text;
            Debug.Log(v);
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
                    throw new HttpRequestException("backend not reachable");
                    return;
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
                    throw new HttpRequestException("backend not reachable");
                    return;
                }

                callback(null);
            }
        }

        public IEnumerator CreateNewFair(Action<int> callback)
        {
            UnityWebRequest request =
                GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/fair",
                    "POST", null);

            yield return request.SendWebRequest();
            string v = request.downloadHandler.text;
            callback(Int32.Parse(v));
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
                yield break;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            callback(null);
        }
        
        public IEnumerator LeaveFairMode(Action<PlayerDetails> callback)
        {
            UnityWebRequest request =
                GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{PlayerPrefs.GetString("uuid")}/leave-trade-show",
                    "POST", "");

            yield return request.SendWebRequest();
            string v = request.downloadHandler.text;
            try
            {
                callback((PlayerDetails)v);
            }
            catch (Exception e) 
            {
                Debug.Log(e.Message);
            }
        }
        
    }
}