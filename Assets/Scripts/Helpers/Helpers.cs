using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using Application = UnityEngine.Device.Application;

namespace Helpers
{
    public enum Tailwind
    {
        None,
        Blue1,
        Blue2,
        Blue3,
        Blue4,
        Blue5,
        BlueUI,
        Violet1,
        Violet2,
        Violet3,
        Violet4,
        Red1,
        Red2,
        Red3,
        Orange1,
        Orange2,
        Orange3,
        Orange4,
        Yellow1,
        Yellow2,
        Yellow3,
        Yellow4,
        Green1,
        Green2,
        Green3,
        Green4,
        Green5,
        Azure1,
        Azure2,
        Azure3,
        Mud1,
        Mud2,
        Mud3,
        Mud4,
        Sky1,
        Sky2,
        Sky3,
        Gray1,
        Black
    }

    public enum FontType
    {
        Mono,
        Condensed,
    }

    public enum FontWeight
    {
        Bold700,
        Medium500,
        Regular400
    }

    public enum Device
    {
        Desktop,
        Mobile
    }

    public enum FontStyles
    {
        Headline,
        SubheadlineCondensed,
        SubheadlineMono,
        FließtextMedium,
        FließtextRegular,
        Button,
        Credit
    }

    class Settings
    {
        public static float RESIZE_FACTOR = 1920f / 812f;

        public static Dictionary<Tailwind, Color> ColorMap = new Dictionary<Tailwind, Color>
        {
            { Tailwind.None, new Color(0, 0, 0, 0) },
            { Tailwind.Blue1, new Color(212 / 255f, 230 / 255f, 244 / 255f, 1) },
            { Tailwind.Blue2, new Color(136 / 255f, 188 / 255f, 226 / 255f, 1) },
            { Tailwind.Blue3, new Color(31 / 255f, 130 / 255f, 192 / 255f, 1) },
            { Tailwind.Blue4, new Color(0 / 255f, 90 / 255f, 148 / 255f, 1) },
            { Tailwind.Blue5, new Color(0 / 255f, 52 / 255f, 107 / 255f, 1) },
            { Tailwind.BlueUI, new Color(2 / 255f, 28 / 255f, 57 / 255f, 1) },
            { Tailwind.Violet1, new Color(199 / 255f, 193 / 255f, 226 / 255f, 1) },
            { Tailwind.Violet2, new Color(144 / 255f, 133 / 255f, 186 / 255f, 1) },
            { Tailwind.Violet3, new Color(57 / 255f, 55 / 255f, 139 / 255f, 1) },
            { Tailwind.Violet4, new Color(41 / 255f, 40 / 255f, 106 / 255f, 1) },
            { Tailwind.Red1, new Color(226 / 255f, 0 / 255f, 26 / 255f, 1) },
            { Tailwind.Red2, new Color(158 / 255f, 28 / 255f, 34 / 255f, 1) },
            { Tailwind.Red3, new Color(119 / 255f, 28 / 255f, 44 / 255f, 1) },
            { Tailwind.Orange1, new Color(254 / 255f, 234 / 255f, 201 / 255f, 1) },
            { Tailwind.Orange2, new Color(251 / 255f, 203 / 255f, 140 / 255f, 1) },
            { Tailwind.Orange3, new Color(242 / 255f, 148 / 255f, 0 / 255f, 1) },
            { Tailwind.Orange4, new Color(235 / 255f, 106 / 255f, 0 / 255f, 1) },
            { Tailwind.Yellow1, new Color(255 / 255f, 250 / 255f, 209 / 255f, 1) },
            { Tailwind.Yellow2, new Color(255 / 255f, 243 / 255f, 129 / 255f, 1) },
            { Tailwind.Yellow3, new Color(255 / 255f, 220 / 255f, 0 / 255f, 1) },
            { Tailwind.Yellow4, new Color(253 / 255f, 195 / 255f, 0 / 255f, 1) },
            { Tailwind.Green1, new Color(238 / 255f, 239 / 255f, 177 / 255f, 1) },
            { Tailwind.Green2, new Color(209 / 255f, 221 / 255f, 130 / 255f, 1) },
            { Tailwind.Green3, new Color(177 / 255f, 200 / 255f, 0 / 255f, 1) },
            { Tailwind.Green4, new Color(143 / 255f, 164 / 255f, 2 / 255f, 1) },
            { Tailwind.Green5, new Color(106 / 255f, 115 / 255f, 65 / 255f, 1) },
            { Tailwind.Azure1, new Color(180 / 255f, 220 / 255f, 211 / 255f, 1) },
            { Tailwind.Azure2, new Color(109 / 255f, 191 / 255f, 169 / 255f, 1) },
            { Tailwind.Azure3, new Color(23 / 255f, 156 / 255f, 125 / 255f, 1) },
            { Tailwind.Mud1, new Color(215 / 255f, 225 / 255f, 201 / 255f, 1) },
            { Tailwind.Mud2, new Color(203 / 255f, 175 / 255f, 115 / 255f, 1) },
            { Tailwind.Mud3, new Color(70 / 255f, 41 / 255f, 21 / 255f, 1) },
            { Tailwind.Mud4, new Color(76 / 255f, 99 / 255f, 111 / 255f, 1) },
            { Tailwind.Sky1, new Color(51 / 255f, 184 / 255f, 202 / 255f, 1) },
            { Tailwind.Sky2, new Color(37 / 255f, 186 / 255f, 226 / 255f, 1) },
            { Tailwind.Sky3, new Color(0 / 255f, 110 / 255f, 146 / 255f, 1) },
            { Tailwind.Gray1, new Color(199 / 255f, 202 / 255f, 204 / 255f, 1) },
            { Tailwind.Black, new Color(0f, 0f, 0f, 1f) }
        };

        public static Dictionary<FontStyles, FontDetails> FontMap = new Dictionary<FontStyles, FontDetails>()
        {
            { FontStyles.Headline, new FontDetails(40, 80, FontType.Condensed, FontWeight.Bold700) },
            { FontStyles.SubheadlineMono, new FontDetails(30, 40, FontType.Mono, FontWeight.Medium500) },
            { FontStyles.SubheadlineCondensed, new FontDetails(30, 40, FontType.Condensed, FontWeight.Medium500) },
            { FontStyles.FließtextMedium, new FontDetails(24, 30, FontType.Mono, FontWeight.Medium500) },
            { FontStyles.FließtextRegular, new FontDetails(18, 24, FontType.Mono, FontWeight.Regular400) },
            { FontStyles.Button, new FontDetails(14, 14, FontType.Condensed, FontWeight.Bold700) },
            { FontStyles.Credit, new FontDetails(14, 14, FontType.Condensed, FontWeight.Regular400) },
        };

        public const float MovementSpeed = 8f;
    }

    public class FontDetails
    {
        private float _mobileTextSize;
        private float _desktopTextSize;
        public FontType fontType;
        public FontWeight fontWeight;

        public FontDetails(int mobileTextSize, int desktopTextSize, FontType fontType, FontWeight fontWeight)
        {
            this._mobileTextSize = mobileTextSize * (1920f / 812f);
            this._desktopTextSize = desktopTextSize;
            this.fontWeight = fontWeight;
            this.fontType = fontType;
        }

        public float GetFontSizeByScreen()
        {
            return Utility.GetDevice() == Device.Mobile ? _mobileTextSize : _desktopTextSize;
        }
    }

    class Utility
    {
        public static void GetTranslatedText(string key, Action<string> cb)
        {
            AsyncOperationHandle<string> op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(key);
            if (op.IsDone)
            {
                cb(op.Result);
            } else
            {
                op.Completed += (operation) =>
                {
                    cb(operation.Result);
                };
            }
        }
        
        public static T GetRandom<T>(T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static IEnumerator AnimateAnything(float duration, float start, float end, AnimationProgress func,
            Action finalCallback = null)
        {
            float counter = 0;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                func.Invoke(counter / duration, start, end);
                yield return null;
            }

            if (finalCallback != null)
            {
                finalCallback.Invoke();
            }
        }

        public static Vector3[] SpriteLocalToWorld(Transform transform, Sprite sp)
        {
            Vector3 pos = transform.position;
            Vector3[] array = new Vector3[2];
            //top left
            array[0] = pos + sp.bounds.min;
            // Bottom right
            array[1] = pos + sp.bounds.max;
            return array;
        }

        public delegate void AnimationProgress(float progress, float start, float end);

        public static Device GetDevice()
        {
            return Application.isMobilePlatform ? Device.Mobile : Device.Desktop;
        }
        
        public static Color GetGradientAtPosition(Color[] colors, float position)
        {
            Gradient gradient = new Gradient();

            // Blend color from red at 0% to blue at 100%
            GradientColorKey[] gradientColors = new GradientColorKey[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                gradientColors[i] = new GradientColorKey(colors[i], i / (colors.Length - 1));
            }
            
            // Blend alpha from opaque at 0% to transparent at 100%
            GradientAlphaKey[] alphas = new GradientAlphaKey[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                alphas[i] = new GradientAlphaKey(colors[i].a, i / (colors.Length - 1));
            }

            gradient.SetKeys(gradientColors, alphas);
            
            return gradient.Evaluate(position);
        }
        
        public static int RandomInRange(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        
    }

    public static class Extensions
    {
        public static string ArrayToJson<T>(this T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.items = array;
            return JsonUtility.ToJson(wrapper);
        }

        private class Wrapper<T>
        {
            public T[] items;
        }

        public static bool Empty(this string s)
        {
            return s == null || s.Length == 0;
        }

        public static float MapBetween(this float v, float fromMin, float fromMax, float toMin, float toMax)
        {
            return (v - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
        }
    }

    public static class Api
    {
        private static readonly HttpClient APIClient = new()
        {
            BaseAddress = new Uri("https://batterygame.web.fec.ffb.fraunhofer.de/"),
            DefaultRequestHeaders = { { "X-DIRECT", "y6biadzsv3t58kv2t8" } },
        };

        public static async Task PushGameStateChange(string uuid, MicrogameState ms)
        {
            string updatedGame = JsonUtility.ToJson(ms);
            HttpResponseMessage response = await APIClient.PostAsync($"api/battery-users/${uuid}/battery-results",
                new StringContent(updatedGame, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            await ReserializeGamestate(uuid);
        }

        public static async Task<PlayerDetails> ToggleLanguage(Language newLanguage)
        {
            PlayerDetails p = GameState.Instance.currentGameState;
            p.language = newLanguage;
            PlayerDetails updated = await UpdatePlayer(p);
            return updated;
        }
        
        private static async Task<PlayerRegistration> RegisterPlayer(string name, Language language)
        {
            try
            {
                string requestString = JsonUtility.ToJson(new PlayerRegistrationRequest(name, language));
                HttpResponseMessage response = await APIClient.PostAsync("api/battery-users",
                    new StringContent(requestString, System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                string resString = await response.Content.ReadAsStringAsync();
                return (PlayerRegistration)resString;
            }
            catch (HttpRequestException e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }

        private static async Task<PlayerDetails> FetchPlayerDetails(string uuid)
        {
            try
            {
                HttpResponseMessage response = await APIClient.GetAsync($"api/battery-users/{uuid}");
                response.EnsureSuccessStatusCode();
                string resString = await response.Content.ReadAsStringAsync();
                return (PlayerDetails)resString;
            }
            catch (HttpRequestException e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }

        private static async Task<PlayerDetails> UpdatePlayer(PlayerDetails newDetails)
        {
            string requestString = JsonUtility.ToJson(newDetails);
            HttpResponseMessage response = await APIClient.PostAsync("api/battery-users",
                new StringContent(requestString, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return (PlayerRegistration)await response.Content.ReadAsStringAsync();
        }

        public static async Task<PlayerDetails> SetGame(MicrogameState m, string playerId)
        {
            string requestString = JsonUtility.ToJson(m);
            HttpResponseMessage response = await APIClient.PostAsync($"api/battery-users/{playerId}/battery-results",
                new StringContent(requestString, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync();
            PlayerDetails p = await FetchPlayerDetails(playerId);
            return p;
        }

        public static async Task ReserializeGamestate(string uuid)
        {
            PlayerDetails d = await FetchPlayerDetails(uuid);
            if (d != null)
            {
                GameState.Instance.currentGameState = d;
            }
        }
        
        public static async Task<string> GetPlayerDetails(string name, Language language)
        {
            string bearer = PlayerPrefs.GetString("bearer", null);
            string uuid = PlayerPrefs.GetString("uuid", null);
            if (!bearer.Empty() && !uuid.Empty())
            {
                APIClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                PlayerDetails p = await FetchPlayerDetails(uuid);
                return p.id;
            }
            else
            {
                PlayerRegistration pr = await RegisterPlayer(name, language);
                if (pr != null)
                {
                    PlayerPrefs.SetString("bearer", pr.token);
                    PlayerPrefs.SetString("uuid", pr.id);
                    PlayerPrefs.Save();
                    APIClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pr.token);
                    return pr.id;
                }

                return null;
            }
        }
    }
    
}