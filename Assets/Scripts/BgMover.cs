using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera Camera;
    public Transform Player;
    public float ParallaxFactor;
    
    private Vector3 _lastScreenPosition;
    private float _totalDistanceTraveled;

    private RectTransform[] _children;
    public Sprite[] Sprites;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            
        }
    }

    void Start()
    {
        // Initialize the last screen position to the initial screen position of the player
        _lastScreenPosition = Camera.WorldToScreenPoint(Player.position);
    }

    void Update()
    {
        Vector3 currentScreenPosition = Camera.WorldToScreenPoint(Player.position);
        float distanceTraveledX = currentScreenPosition.x - _lastScreenPosition.x;
        _totalDistanceTraveled += distanceTraveledX;
        _lastScreenPosition = currentScreenPosition;
        float moveBy = _totalDistanceTraveled * ParallaxFactor;
        for (var i = 0; i < _children.Length; i++)
        {
            _children[i].localPosition = new Vector3(_children[i].localPosition.x + moveBy, _children[i].localPosition.y, _children[i].localPosition.z);
        }
    }
}