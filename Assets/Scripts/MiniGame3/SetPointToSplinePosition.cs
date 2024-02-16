using BezierSolution;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetPointToSplinePosition : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    public BezierSpline spline;
    public Image maskedImage;
    public GameObject finishedImage;
    
    public float _rotationOffset = 48f;

    private float _splineLength;
    private float _currentPositionSpline = 0f;
    
    private float timeCount;
    
    private bool _isFinished = false;
    private bool _isDragging = false;

    void Start()
    {
        _splineLength = spline.GetLengthApproximately(0f, 1f);
    }
    
    public void OnBeginDrag(PointerEventData data)
    {
        if (_isDragging || _isFinished) return;
        _isDragging = true;
    }

    public void OnDrag(PointerEventData data)
    {
        if (_isFinished) return;
        
        if (data.delta.x != 0 || data.delta.y != 0);
        {
            float biggerDelta = Mathf.Abs(data.delta.x) > Mathf.Abs(data.delta.y) ? Mathf.Abs(data.delta.x) : Mathf.Abs(data.delta.y);
            float increment = biggerDelta / _splineLength;
            _currentPositionSpline += increment;
            BezierSpline.EvenlySpacedPointsHolder test = spline.CalculateEvenlySpacedPoints(10f, 3f);
            transform.position = spline.GetPoint(test.GetNormalizedTAtPercentage(_currentPositionSpline ));
            
            maskedImage.fillAmount = GetRotationFromHandleToCenter();
            if (_currentPositionSpline >= 1f)
            {
                _isFinished = true; 
                finishedImage.SetActive(true);
                data.pointerDrag = null;
                _isDragging = false;
                _currentPositionSpline = 1f;
            }
        }
    }
    
    public void OnPointerExit(PointerEventData data)
    {
        // data.pointerDrag = null;
        Debug.Log("The cursor exited the selectable UI element.");
    }

    public void OnEndDrag(PointerEventData data)
    {
        _isDragging = false;
    }
    
    private float GetRotationFromHandleToCenter()
    {
        Vector3 direction = maskedImage.transform.position- transform.position;
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
        angleInDegrees = (angleInDegrees + 360) % 360; 
        angleInDegrees += _rotationOffset;
        angleInDegrees = (angleInDegrees + 360) % 360;

        return 1 - (angleInDegrees / 360f);
    }
}


