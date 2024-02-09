using System;

namespace Minigame5.Classes
{
    public class QuizSlot
    {
        public string question;
        public string[] answers;
        public int correctAnswerIndex;
        public bool correctlyAnswered = false;
        
        public QuizSlot(string question, string[] answers, int correctAnswerIndex)
        {
            this.question = question;
            this.answers = answers;
            this.correctAnswerIndex = correctAnswerIndex;
        }
    }
}