using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    public Vector3 rotationOffset = new Vector3(0, 90, 0);

    private Camera _mainCamera; 
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = _mainCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(heading) * Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
    }
}
