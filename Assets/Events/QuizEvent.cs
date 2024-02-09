using UnityEngine.Events;

namespace Events
{
    public class QuizEvent : UnityEvent<int, int>
    {
        public interface IUseAnswered
        {
            public void UseAnswered(int answerIndex, int correctAnswerIndex);
        }
    }
}