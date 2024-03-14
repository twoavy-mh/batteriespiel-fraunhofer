using System;
using System.Collections;
using System.Collections.Generic;
using Minigame5;
using Minigame5.Classes;
using UnityEngine;

namespace Minigame5
{
    public class QuizSlots : MonoBehaviour
    {
        private QuizController _quizController;

        private List<QuizSlot> _quizSlotsList;
        private int _currentSlotIndex;

        // Start is called before the first frame update
        void Start()
        {
            _quizSlotsList = new List<QuizSlot>()
            {
                new QuizSlot(
                    "question1",
                    new string[]
                    {
                        "question1_answer1",
                        "question1_answer2",
                        "question1_answer3",
                        "question1_answer4"
                    },
                    2),
                new QuizSlot(
                    "question2",
                    new string[]
                    {
                        "question2_answer1",
                        "question2_answer2",
                        "question2_answer3",
                        "question2_answer4"
                    },
                    0),
                new QuizSlot(
                    "question3",
                    new string[]
                    {
                        "question3_answer1",
                        "question3_answer2",
                        "question3_answer3",
                        "question3_answer4"
                    },
                    0),
                new QuizSlot(
                    "question4",
                    new string[]
                    {
                        "question4_answer1",
                        "question4_answer2",
                        "question4_answer3",
                        "question4_answer4"
                    },
                    3),
                new QuizSlot(
                    "question5",
                    new string[]
                    {
                        "question5_answer1",
                        "question5_answer2",
                        "question5_answer3",
                        "question5_answer4"
                    },
                    2),
                new QuizSlot(
                    "question6",
                    new string[]
                    {
                        "question6_answer1",
                        "question6_answer2",
                        "question6_answer3",
                        "question6_answer4"
                    },
                    3),
            };

            _currentSlotIndex = 0;

        }

        public void InitQuiz()
        {
            _quizController = FindFirstObjectByType<QuizController>();
            _quizController.SetQuizSlot(GetCurrentSlot(), GetCurrentQuizCount());
        }

        public int GetSlotsListLengh()
        {
            return _quizSlotsList.Count;
        }

        public QuizSlot GetCurrentSlot()
        {
            return _quizSlotsList[_currentSlotIndex];
        }

        public int GetCurrentSlotIndex()
        {
            return _currentSlotIndex;
        }

        private string GetCurrentQuizCount()
        {
            return (_currentSlotIndex + 1) + ":";
        }

        public void IncrementSlotIndex()
        {
            _currentSlotIndex++;
            if (_currentSlotIndex < _quizSlotsList.Count)
            {
                _quizController.SetQuizSlot(GetCurrentSlot(), GetCurrentQuizCount());
            }
        }
    }
}
