using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HideHighscoreImage : MonoBehaviour
    {

        public Sprite high;
        public Sprite mid;
        public Sprite low;

        private string _meh = "did_okay_final_screen";
        private string _bad = "did_bad_final_screen";
        private IEnumerator Start()
        {
            yield return new WaitUntil(() =>
                (SceneController.Instance != null && SceneController.Instance.myScore != null));
            GameObject o;
            if (SceneController.Instance.myScore.rank == 0)
            {
                GetComponent<Image>().sprite = high;
                
            } else if (SceneController.Instance.myScore.rank < 10 && SceneController.Instance.myScore.rank > 0)
            {
                GetComponent<Image>().sprite = mid;
                o = transform.GetChild(1).gameObject;
                Set(o, _meh);
            }
            else
            {
                GetComponent<Image>().sprite = low;
                o = transform.GetChild(2).gameObject;
                Set(o, _meh);
            }
        }

        private void Set(GameObject o, string k)
        {
            o.SetActive(true);
            Debug.Log(o.transform.parent.name);
            Helpers.Utility.GetTranslatedText(k, (s) =>
            {
                GameObject.Find("newHighscore").GetComponent<TMP_Text>().text = s;
            }, new Dictionary<string, string>()
            {
                {"~score", SceneController.Instance.myScore.totalScore.ToString()},
            });
        }
        
    }
}