using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Models;
using UnityEngine;
using UnityEngine.Networking;

namespace Helpers
{
    public class Api
    {
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

        public static void PushGameStateChange(string uuid, MicrogameState ms)
        {
            string updatedGame = JsonUtility.ToJson(ms);
            UnityWebRequest request =
                GetBaseRequest(
                    $"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{uuid}/battery-results", "POST",
                    updatedGame);
            request.SendWebRequest();
            while (!request.isDone)
            {
            }

            ReserializeGamestate(uuid);
        }

        public static PlayerDetails ToggleLanguage(Language newLanguage)
        {
            PlayerDetails p = GameState.Instance.currentGameState;
            p.language = newLanguage;
            PlayerDetails updated = UpdatePlayer(p);
            return updated;
        }

        private static PlayerRegistration RegisterPlayer(string name, Language language)
        {
            Debug.Log("Registering");
            string requestString = JsonUtility.ToJson(new PlayerRegistrationRequest(name, language));
            UnityWebRequest request = GetBaseRequest("https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users",
                "POST", requestString);
            request.SendWebRequest();
            while (!request.isDone)
            {
                Debug.Log("Not done yet");
            }

            string v = request.downloadHandler.text;
            Debug.Log(v);

            return (PlayerRegistration)v;
        }

        private static PlayerDetails FetchPlayerDetails(string uuid)
        {
            UnityWebRequest request =
                GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{uuid}", "GET", null);
            request.SendWebRequest();
            while (!request.isDone)
            {
            }

            string v = request.downloadHandler.text;
            return (PlayerDetails)v;
        }

        private static PlayerDetails UpdatePlayer(PlayerDetails newDetails)
        {
            string requestString = JsonUtility.ToJson(newDetails);
            UnityWebRequest request = GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users",
                "POST", requestString);


            request.SendWebRequest();
            while (!request.isDone)
            {
            }


            string v = request.downloadHandler.text;
            try
            {
                return (PlayerRegistration)v;
            }
            catch (Exception e)
            {
                new Toast(e.Message).Show();
            }

            return null;
        }

        public static PlayerDetails SetGame(MicrogameState m, string playerId)
        {
            string requestString = JsonUtility.ToJson(m);
            UnityWebRequest request =
                GetBaseRequest(
                    $"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{playerId}/battery-results",
                    "POST", requestString);


            request.SendWebRequest();
            while (!request.isDone)
            {
            }

            string v = request.downloadHandler.text;


            try
            {
                PlayerDetails p = FetchPlayerDetails(playerId);
                return p;
            }
            catch (Exception e)
            {
                new Toast(e.Message).Show();
            }

            return null;
        }

        public static void ReserializeGamestate(string uuid)
        {
            try
            {
                PlayerDetails d = FetchPlayerDetails(uuid);
                if (d != null)
                {
                    GameState.Instance.currentGameState = d;
                }
            }
            catch (Exception e)
            {
                new Toast(e.Message).Show();
            }
        }

        public static string GetPlayerDetails(string name, Language language)
        {
            string bearer = PlayerPrefs.GetString("bearer", null);
            string uuid = PlayerPrefs.GetString("uuid", null);
            if (!bearer.Empty() && !uuid.Empty())
            {
                try
                {
                    return FetchPlayerDetails(uuid).id;
                }
                catch (Exception e)
                {
                    new Toast(e.Message).Show();
                }

                return null;
            }
            else
            {
                try
                {
                    PlayerRegistration pr = RegisterPlayer(name, language);
                    if (pr != null)
                    {
                        PlayerPrefs.SetString("bearer", pr.token);
                        PlayerPrefs.SetString("uuid", pr.id);
                        PlayerPrefs.Save();
                        return pr.id;
                    }
                }
                catch (Exception e)
                {
                    new Toast(e.Message).Show();
                }

                return null;
            }
        }

        public static LeaderboardArray GetLeaderboard(string uuid)
        {
            UnityWebRequest request =
                GetBaseRequest($"https://batterygame.web.fec.ffb.fraunhofer.de/api/battery-users/{uuid}/leaderboard",
                    "GET", null);

            request.SendWebRequest();
            while (!request.isDone)
            {
            }


            string v = request.downloadHandler.text;
            try
            {
                return (LeaderboardArray)v;
            }
            catch (Exception e)
            {
                new Toast(e.Message).Show();
            }

            return null;
        }
    }
}