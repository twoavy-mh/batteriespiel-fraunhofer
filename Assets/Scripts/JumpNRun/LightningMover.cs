using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMover : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float maxHeight = 3.17f;
    public float minHeight = -1.51f;
    private float _dir = 1;

    void Start()
    {
        /*_rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;*/
        InvokeRepeating(nameof(RandomDirection), 0, 1);
    }

    private void RandomDirection()
    {
        float currentHeight = transform.position.y;
        float nextHeight = transform.position.y;
        if (nextHeight + _dir > maxHeight)
        {
            _dir = -1;
        }
        else if (nextHeight + _dir < minHeight)
        {
            _dir = 1;
        }
        else
        {
            nextHeight += _dir;
        }
        StartCoroutine(Helpers.Utility.AnimateAnything(1f, currentHeight, nextHeight,
            (progress, start, end) => transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(start, end, progress), transform.position.z)));
    }
}