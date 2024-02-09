using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Minigame5
{
    [System.Serializable]
    public class QuizController : MonoBehaviour
    {
        public SelfTranslatingText questionText;
        
        [SerializeField]
        public SelfTranslatingText[] answers;
        public int correctAnswerIndex;
        
    // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void SetQuizSlot(string question, string[] answers, int correctAnswerIndex)
        {
            questionText.translationKey = question;
            // for (int i = 0; i < answers.Length; i++)
            // {
            //     answers[i].translationKey = answers[i];
            // }
            this.correctAnswerIndex = correctAnswerIndex;
        }
    }
}
