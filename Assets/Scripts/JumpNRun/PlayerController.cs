using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Events;
using Helpers;
using UnityEngine;
using JumpNRun;
using Models;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerController : MonoBehaviour, DieEvent.IUseDie
{
    private Rigidbody2D _rb;
    private float _baseSpeed;
    private float _speed = 0;
    public bool isColliding = false;
    public bool isStill = false;
    private bool _finished = false;

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
    private bool _dead = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _baseSpeed = Settings.MovementSpeed;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _scoreController = GameObject.Find("Canvas").GetComponentInChildren<ScoreController>();
        SceneController.Instance.dieEvent.AddListener(UseDie);
    }

    public void StartRunning()
    {
        StartCoroutine(Utility.AnimateAnything(1f, 0, Settings.MovementSpeed,
            (progress, start, end) => _speed = Mathf.Lerp(start, end, progress)));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_speed, _rb.velocity.y);
    }

    private void Update()
    {
        if (isColliding || _finished || _dead)
        {
            return;
        }

        if (transform.position.y < -3.3)
        {
            Debug.Log("die now");
        }

        if ((Input.GetKeyDown(KeyCode.Space) ||
             (Input.touchCount > 0 && Input.touches.ElementAtOrDefault(0).phase == TouchPhase.Began)) && !_mustFall)
        {
            if (!_isGrounded) _mustFall = true;
            _isGrounded = false;
            _isJumping = true;
            _jumpTimeCounter = _jumpTime;
            _rb.velocity = Vector2.up * ((Screen.height / 1080f) * 8f);
        }

        if ((Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 &&
                                             (Input.touches.ElementAtOrDefault(0).phase == TouchPhase.Stationary ||
                                              Input.touches.ElementAtOrDefault(0).phase == TouchPhase.Moved))) &&
            _isJumping)
        {
            if (_mustFall) return;

            _animator.SetTrigger("jump");
            if (_jumpTimeCounter > 0)
            {
                _rb.velocity = Vector2.up * ((Screen.height / 1080f) * 6f);
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                NowFalling();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) ||
            (Input.touchCount == 0 && Input.touches.ElementAtOrDefault(0).phase == TouchPhase.Ended))
        {
            NowFalling();
        }
    }

    private void NowFalling()
    {
        _isJumping = false;
        StartCoroutine(Utility.AnimateAnything(0.5f, 1f, 2f,
            (progress, start, end) => _rb.gravityScale = Mathf.Lerp(start, end, progress)));
    }

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            switch (other.tag)
            {
                case "Killzone":
                    SceneController.Instance.dieEvent.Invoke();
                    break;
                case "BlueLightning":
                    FadeCollectable(other.GetComponent<SpriteRenderer>());
                    SceneController.Instance.collectEvent.Invoke(Collectable.BlueLightning);
                    break;
                case "YellowLightning":
                    FadeCollectable(other.GetComponent<SpriteRenderer>());
                    SceneController.Instance.collectEvent.Invoke(Collectable.YellowLightning);
                    break;
                case "Target":
                    FadeCollectable(other.GetComponent<SpriteRenderer>());
                    _collectedCount++;
                    SceneController.Instance.collectEvent.Invoke(Collectable.LevelSpecific);
                    if (_collectedCount == 5)
                    {
                        _finished = true;
                        StartCoroutine(Utility.AnimateAnything(2f, _speed, 0,
                            (progress, start, end) => _speed = Mathf.Lerp(start, end, progress),
                            () =>
                            {
                                Debug.Log("Starting final callbacl");
                                SerializeScore();
                            }));
                        
                    }

                    break;
            }
        }
    }
    
    private void SerializeScore()
    {
        MicrogameState m = new MicrogameState();
        m.game = GameState.Instance.GetCurrentMicrogame();
        m.unlocked = true;
        m.finished = false;
        m.result = 0;
        m.jumpAndRunResult = _scoreController.GetScoreForApi();
        
        StartCoroutine(Api.Instance.SetGame(m, GameState.Instance.currentGameState.id, details =>
        {
            GameState.Instance.currentGameState = details;
            SceneManager.LoadScene($"MicroGame{((int)m.game) + 1}Onboard");
        }));
    }
    
    private void FadeCollectable(SpriteRenderer s)
    {
        StartCoroutine(Utility.AnimateAnything(0.5f, 1f, 0f,
            (progress, start, end) => s.color = new Color(1f, 1f, 1f, Mathf.Lerp(start, end, progress))));
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

    public void UseDie()
    {
        _dead = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        StartCoroutine(Utility.AnimateAnything(2f, _speed, 0,
            (progress, start, end) => _speed = Mathf.Lerp(start, end, progress),
            () => { _animator.SetTrigger("die"); }));
    }
}