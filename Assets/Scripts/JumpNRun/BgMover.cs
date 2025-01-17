using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Events;
using Helpers;
using JumpNRun;
using UnityEngine;
using UnityEngine.UI;

public class BgMover : MonoBehaviour, DieEvent.IUseDie
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
    public Sprite[] spritesLvl6;

    private List<Sprite[]> _sprites;
    private RectTransform[] _children;

    private float _deadFactor = 1;

    private void Awake()
    {
        spritesLvl6 = new Sprite[spritesLvl1.Length + spritesLvl2.Length + spritesLvl3.Length + spritesLvl4.Length +
                                 spritesLvl5.Length];
        spritesLvl1.CopyTo(spritesLvl6, 0);
        spritesLvl2.CopyTo(spritesLvl6, spritesLvl1.Length);
        spritesLvl3.CopyTo(spritesLvl6, spritesLvl1.Length + spritesLvl2.Length);
        spritesLvl4.CopyTo(spritesLvl6, spritesLvl1.Length + spritesLvl2.Length + spritesLvl3.Length);
        spritesLvl5.CopyTo(spritesLvl6, spritesLvl1.Length + spritesLvl2.Length + spritesLvl3.Length +
                                        spritesLvl4.Length);
        Debug.Log(spritesLvl6.Length);
        SceneController.Instance.dieEvent.AddListener(UseDie);
        _sprites = new List<Sprite[]>()
        {
            spritesLvl1, spritesLvl2, spritesLvl3, spritesLvl4, spritesLvl5, spritesLvl6
        };
        _children = new RectTransform[_sprites[Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5)].Length];
        RectTransform parentRt = transform.parent.GetComponent<RectTransform>();
        Vector2 parentDimensions = new Vector2(parentRt.rect.width, parentRt.rect.height);
        GetComponent<RectTransform>().sizeDelta = parentDimensions;
        for (int i = 0; i < _sprites[Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5)].Length; i++)
        {
            GameObject go = new GameObject("Background" + i);
            go.transform.SetParent(transform);
            RectTransform rt = go.AddComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0f, 1f);
            Image img = go.AddComponent<Image>();
            img.sprite = _sprites[Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5)][i];
            rt.sizeDelta = new Vector2(img.sprite.rect.width, img.sprite.rect.height);
            rt.localScale = new Vector3(1, 1, 1);
            rt.localPosition = new Vector3(1920 * i, 0, 0);
            _children[i] = rt;
        }
    }

    void Update()
    {
        if (!PlayerController.hasStarted()) return;
        
        float dt = Time.deltaTime * _deadFactor;

        for (var i = 0; i < _children.Length; i++)
        {
            _children[i].position =
                new Vector3(_children[i].position.x -
                            ((PlayerController.GeneralSpeed * dt) * ParallaxFactor),
                    _children[i].position.y, _children[i].position.z);
        }
    }

    public void UseDie()
    {
        StartCoroutine(Utility.AnimateAnything(2f, 1f, 0f,
            (progress, start, end) => { _deadFactor = Mathf.Lerp(1, 0, progress); }));
    }
}