using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class BgMover : MonoBehaviour
{
    public bool isUpper;
    
    public Transform Root;
    public Transform Player;
    public PlayerController PlayerController;
    public float ParallaxFactor;
    
    public Sprite[] spritesLvl1;
    public Sprite[] spritesLvl2;
    public Sprite[] spritesLvl3;
    public Sprite[] spritesLvl4;
    public Sprite[] spritesLvl5;
    
    private List<Sprite[]> _sprites;
    private RectTransform[] _children;

    private void Awake()
    {
        _sprites = new List<Sprite[]>()
        {
            spritesLvl1, spritesLvl2, spritesLvl3, spritesLvl4, spritesLvl5
        };
        _children = new RectTransform[_sprites[(int)GameState.Instance.GetCurrentMicrogame()].Length];
        RectTransform parentRt = transform.parent.GetComponent<RectTransform>();
        Vector2 parentDimensions = new Vector2(parentRt.rect.width, parentRt.rect.height);
        GetComponent<RectTransform>().sizeDelta = parentDimensions;
        for (int i = 0; i < _sprites[(int)GameState.Instance.GetCurrentMicrogame()].Length; i++)
        {
           GameObject go = new GameObject("Background" + i);
           go.transform.SetParent(transform);
           RectTransform rt = go.AddComponent<RectTransform>();
           rt.anchorMin = new Vector2(0, 1);
           rt.anchorMax = new Vector2(0, 1);
           rt.pivot = new Vector2(0f, 1f);
           Image img = go.AddComponent<Image>();
           img.sprite = _sprites[(int)GameState.Instance.GetCurrentMicrogame()][i];
           rt.sizeDelta = new Vector2(img.sprite.rect.width, img.sprite.rect.height);
           rt.localScale = new Vector3(1, 1, 1);
           rt.localPosition = new Vector3(2048*i, isUpper ? -300 : -450, 0);
           _children[i] = rt;
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if (!PlayerController.isColliding)
        {
            for (var i = 0; i < _children.Length; i++)
            {
                _children[i].position =
                    new Vector3(_children[i].position.x -
                                (1f * ParallaxFactor * dt),
                        _children[i].position.y, _children[i].position.z);
            } 
        }
    }
}