using System;
using System.Collections;
using Events;
using Helpers;
using UnityEngine;
using JumpNRun;
using Models;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _speed;
    public bool isColliding = false;
    public bool isStill = false;

    private bool _isGrounded = true;
    private bool _mustFall = false;
    private bool _isJumping = false;
    private float _jumpTimeCounter;
    private float _jumpTime = 0.6f;

    private Animator _animator;
    private Coroutine _boink = null;
    private ScoreController _scoreController;

    private int _collectedCount = 0;

    public float smallest = 0f;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = Settings.MovementSpeed;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _scoreController = GameObject.Find("Canvas").GetComponentInChildren<ScoreController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_speed, _rb.velocity.y);
    }

    private void Update()
    {
        if (isColliding)
        {
            return;
        }

        if (transform.position.y < -3.3)
        {
            Debug.Log("die now");
        }

        if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetMouseButtonDown(0))) && !_mustFall)
        {
            if (!_isGrounded) _mustFall = true;
            _isGrounded = false;
            _isJumping = true;
            _jumpTimeCounter = _jumpTime;
            StartCoroutine(Utility.AnimateAnything(0.5f, _speed, _speed * 0.7f,
                (progress, start, end) => _speed = Mathf.Lerp(start, end, progress)));
            _rb.velocity = Vector2.up * 8f;
        }

        if ((Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetMouseButton(0))) && _isJumping)
        {
            _animator.SetTrigger("jump");
            if (_jumpTimeCounter > 0)
            {
                _rb.velocity = Vector2.up * 6f;
                _jumpTimeCounter -= Time.deltaTime;
                SceneController.Instance.decayEvent.Invoke(new DecayInstance(0.1f, -10));
            }
            else
            {
                NowFalling();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || (Input.touchCount == 0 && Input.GetMouseButtonUp(0)))
        {
            NowFalling();
        }
    }

    private void NowFalling()
    {
        StartCoroutine(Utility.AnimateAnything(0.5f, _speed, Settings.MovementSpeed,
            (progress, start, end) => _speed = Mathf.Lerp(start, end, progress)));
        _isJumping = false;
        _rb.gravityScale = 2f;
    }

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            switch (other.tag)
            {
                case "Killzone":
                    _speed = 0;
                    _rb.velocity = Vector2.down * 8f;
                    //_animator.SetTrigger("die");
                    break;
                case "Lithium":
                    FadeCollectable(other.GetComponent<SpriteRenderer>());
                    SceneController.Instance.collectEvent.Invoke(Collectable.Lithium);
                    //GameState.Instance.Collect(50);
                    break;
                case "BlueLightning":
                    FadeCollectable(other.GetComponent<SpriteRenderer>());
                    SceneController.Instance.collectEvent.Invoke(Collectable.BlueLightning);
                    //GameState.Instance.Collect(100);
                    break;
                case "YellowLightning":
                    FadeCollectable(other.GetComponent<SpriteRenderer>());
                    //GameState.Instance.Collect(200);
                    SceneController.Instance.collectEvent.Invoke(Collectable.YellowLightning);
                    break;
                case "Target":
                    FadeCollectable(other.GetComponent<SpriteRenderer>());
                    _collectedCount++;
                    if (_collectedCount == 1)
                    {
                        MicrogameState m = new MicrogameState();
                        m.game = GameState.Instance.GetCurrentMicrogame();
                        m.unlocked = true;
                        m.finished = false;
                        m.result = _scoreController.GetScoreForApi();
                        await Api.SetGame(m, GameState.Instance.currentGameState.id);
                        SceneManager.LoadScene($"Microgame{GameManager.Instance.currentJumpAndRunLevel + 1}Done");
                    }
                    break;
            }
        }
    }

    private void FadeCollectable(SpriteRenderer s)
    {
        StartCoroutine(Utility.AnimateAnything(0.5f, 1f, 0f, (progress, start, end) => s.color = new Color(1f, 1f, 1f, Mathf.Lerp(start, end, progress))));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.transform.tag)
        {
            case "Obstacle":
                if (_boink != null)
                {
                    return;
                }

                _boink = StartCoroutine(Boink(other.collider, () =>
                {
                    _boink = null;
                    StartCoroutine(Utility.AnimateAnything(1f, 0, Settings.MovementSpeed,
                        (progress, start, end) => _speed = Mathf.Lerp(start, end, progress)));
                }));
                break;
            case "Floor":
                _isGrounded = true;
                _rb.gravityScale = 1f;
                _mustFall = false;
                _speed = Settings.MovementSpeed;
                break;
        }
    }

    

    private IEnumerator Boink(Collider2D obstacle, Action cb = null)
    {
        isColliding = true;
        _animator.SetTrigger("bounce");
        StartCoroutine(Utility.AnimateAnything(0.5f, 0, -6,
            (progress, start, end) => _speed = Mathf.Lerp(start, end, progress)));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Utility.AnimateAnything(1f, _speed, 0,
            (progress, start, end) => _speed = Mathf.Lerp(start, end, progress), () => isStill = true));
        yield return new WaitForSeconds(1f);
        isStill = false;
        isColliding = false;
        cb?.Invoke();
    }
}