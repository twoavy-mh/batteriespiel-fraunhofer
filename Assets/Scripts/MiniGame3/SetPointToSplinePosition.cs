using System;
using BezierSolution;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using TouchPhase = UnityEngine.TouchPhase;

namespace Minigame3
{
    public class SetPointToSplinePosition : MonoBehaviour
    {
        public event Action OnFinishedCutting;
        
        public BezierSpline spline;
        public Image maskedImage;
        public GameObject finishedImage;
        private CircleCollider2D _handleBarCollider;

        private float _splineLength;
        private float _currentPositionSpline = 0f;

        private float _currentRotation = 0f;

        private bool _isFinished = false;
        private bool _isDragging = false;

        void Start()
        {
            _handleBarCollider = GetComponent<CircleCollider2D>();
            _splineLength = spline.GetLengthApproximately(0f, 1f);
        }

        private void Update()
        {
            if (_isFinished) return;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) && InputColliderHit(
                        touch.position))
                
                {
                    _isDragging = true;
                    OnHandleBarDrag(touch.position);
                }
                else
                {
                    _isDragging = false;
                }
            } 
            else 
            {
                _isDragging = false;
            }

            // Detect if Left Mouse Button is pressed
            if (Mouse.current.leftButton.isPressed || Pointer.current.IsPressed() && InputColliderHit(Pointer.current.position.ReadValue()))
            {
                _isDragging = true;
                Vector2 currentPosition = Pointer.current.position.ReadValue();
                OnHandleBarDrag(currentPosition);
            }
            else
            {
                _isDragging = false;
            }
        }
        
        private bool InputColliderHit(Vector2 inputPosition)
        {
            return _handleBarCollider.OverlapPoint(inputPosition);
        }
        
        private void OnHandleBarDrag(Vector2 position)
        {
            float rotationToCenter = GetRotationFromHandleToCenter( new Vector3(position.x, position.y, 0f));
            
            if (rotationToCenter < _currentRotation || rotationToCenter - _currentRotation > 0.2f) return;
            
            _currentRotation = rotationToCenter;
             transform.position =spline.FindNearestPointToLine(maskedImage.transform.position, position);
            maskedImage.fillAmount = rotationToCenter;

            if (_currentRotation >= 0.99f)
            {
                _isFinished = true;
                finishedImage.SetActive(true);
                _isDragging = false;
                _currentRotation = 1f;
                OnFinishedCutting?.Invoke();
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        private float GetRotationFromHandleToCenter(Vector3 targetPosition)
        {
            Vector3 direction = maskedImage.transform.position - targetPosition;
            float angleInRadians = Mathf.Atan2(direction.y, direction.x);
            float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
            angleInDegrees = (angleInDegrees + 360) % 360;
            angleInDegrees += 90 - 54;
            angleInDegrees = (angleInDegrees + 360) % 360;

            return 1 - (angleInDegrees / 360f);
        }
    }
}
