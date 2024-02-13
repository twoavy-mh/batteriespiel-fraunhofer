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
                        "qeustion1_answer1",
                        "qeustion1_answer2",
                        "qeustion1_answer3",
                        "qeustion1_answer4"
                    },
                    2),
                new QuizSlot(
                    "question2",
                    new string[]
                    {
                        "qeustion2_answer1",
                        "qeustion2_answer2",
                        "qeustion2_answer3",
                        "qeustion2_answer4"
                    },
                    0),
                new QuizSlot(
                    "question3",
                    new string[]
                    {
                        "qeustion3_answer1",
                        "qeustion3_answer2",
                        "qeustion3_answer3",
                        "qeustion3_answer4"
                    },
                    0),
                new QuizSlot(
                    "question4",
                    new string[]
                    {
                        "qeustion4_answer1",
                        "qeustion4_answer2",
                        "qeustion4_answer3",
                        "qeustion4_answer4"
                    },
                    3),
                new QuizSlot(
                    "question5",
                    new string[]
                    {
                        "qeustion5_answer1",
                        "qeustion5_answer2",
                        "qeustion5_answer3",
                        "qeustion5_answer4"
                    },
                    2),
                new QuizSlot(
                    "question6",
                    new string[]
                    {
                        "qeustion6_answer1",
                        "qeustion6_answer2",
                        "qeustion6_answer3",
                        "qeustion6_answer4"
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
