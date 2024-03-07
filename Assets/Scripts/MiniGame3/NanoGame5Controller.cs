using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class NanoGame5Controller : MonoBehaviour
    {
        public event Action OnFinishedGame;
        public event Action OnSkipGame;
        
        public Button skipButton;
        
        public GameObject cutterSpawnPoint;
        public GameObject cutterPrefabMobile;
        public GameObject cutterPrefabDesktop;
        public GameObject beltArrowPrefabMobile;
        public GameObject beltArrowPrefabDesktop;
        
        private bool _gameStarted = false;
        private bool _gameFinished = false;
        
        private static int _startOffset = 422;
        private static int _screenOffset = 2580;
        private static int _dividerOffset = 223;
        
        private List<GameObject> _beltObjects = new List<GameObject>();
        private List<int> _startPositions = new List<int>() { -(_screenOffset/2) - _startOffset , -_startOffset,(_screenOffset/2) - _startOffset };
        
        // Start is called before the first frame update
        void Start()
        {
            foreach (int startOffset in _startPositions)
            {
                InstantiateNewCutter(startOffset);
            }
            
            skipButton.onClick.AddListener(SkipGame);
            
            _gameStarted = true;
        }

        void FixedUpdate()
        {
            if (_gameStarted && !_gameFinished)
            {
                foreach (GameObject cutter in _beltObjects)
                {
                    if (cutter.name.StartsWith("Cutter"))
                    {
                        MoveCutterGame(cutter);
                    }
                    else
                    {
                        MoveBeltDivider(cutter);
                        
                    }
                }
            }
        }

        private void StopGame()
        {
            skipButton.onClick.RemoveListener(SkipGame);
            _gameFinished = true;
            
            foreach (GameObject cutter in _beltObjects)
            {
                if (cutter.name.StartsWith("Cutter"))
                    cutter.GetComponentInChildren<SetPointToSplinePosition>().OnFinishedCutting -= OnGameFinished;
            }
        }

        void OnGameFinished()
        {
            StopGame();
            OnFinishedGame?.Invoke();
        }
        
        private void SkipGame()
        {
            StopGame();
            OnSkipGame?.Invoke();
        }

        private void MoveCutterGame(GameObject cutter)
        {
            if (cutter.transform.localPosition.x < -_screenOffset)
            {
                cutter.transform.localPosition = new Vector3(_screenOffset/2, cutter.transform.localPosition.y, 0);
            }
            else
            {
                cutter.transform.localPosition = new Vector3(cutter.transform.localPosition.x - 1920f / 1000F,
                    cutter.transform.localPosition.y, 0);
            }
        }

        private void MoveBeltDivider(GameObject beltDivider)
        {
            if (beltDivider.transform.localPosition.x < -_screenOffset)
            {
                beltDivider.transform.localPosition = new Vector3(_screenOffset/2, beltDivider.transform.localPosition.y, 0);
            }
            else
            {
                beltDivider.transform.localPosition = new Vector3(beltDivider.transform.localPosition.x - 1920f / 1000F,
                    beltDivider.transform.localPosition.y, 0);
            }
        }
        
        private void InstantiateNewCutter(int startOffset)
        {
                GameObject newCutter = Instantiate(Application.isMobilePlatform? cutterPrefabMobile:cutterPrefabDesktop, cutterSpawnPoint.transform);
                newCutter.transform.localScale = Vector3.one;
                newCutter.transform.localPosition = new Vector3(startOffset,-(newCutter.GetComponent<RectTransform>().sizeDelta.y/2),0);
                newCutter.name = Application.isMobilePlatform? "CutterMobile":"CutterDesktop";
                newCutter.GetComponentInChildren<SetPointToSplinePosition>().OnFinishedCutting += OnGameFinished;
                _beltObjects.Add(newCutter);
                
                
                GameObject newBeltDivider = Instantiate(Application.isMobilePlatform? beltArrowPrefabMobile:beltArrowPrefabDesktop, cutterSpawnPoint.transform);
                newBeltDivider.transform.localScale = Vector3.one;
                newBeltDivider.transform.localPosition = new Vector3(startOffset - _dividerOffset,0,0);
                newBeltDivider.name = Application.isMobilePlatform? "BeltDividerMobile":"BeltDividerDesktop";
                _beltObjects.Add(newBeltDivider);
                
        }
    }
}
