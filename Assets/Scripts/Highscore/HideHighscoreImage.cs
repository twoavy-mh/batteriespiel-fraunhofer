using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

            if (!SceneController.Instance.hasScore)
            {
                RectTransform rt = transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<RectTransform>();
                rt.position = new Vector3(rt.position.x, 0, rt.position.z);
                gameObject.SetActive(false);
                yield break;
            }
            
            if (SceneController.Instance.myScore.rank == 1)
            {
                GetComponent<Image>().sprite = high;
                
            } else if (SceneController.Instance.myScore.rank < 10 && SceneController.Instance.myScore.rank > 1)
            {
                GetComponent<Image>().sprite = mid;
                o = transform.GetChild(1).gameObject;
                transform.GetChild(2).gameObject.SetActive(false);
                Set(o, _meh);
            }
            else
            {
                GetComponent<Image>().sprite = low;
                transform.GetChild(1).gameObject.SetActive(false);
                o = transform.GetChild(2).gameObject;
                Set(o, _meh);
            }

            GetComponent<Image>().DOColor(Color.white, 0.5f);
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