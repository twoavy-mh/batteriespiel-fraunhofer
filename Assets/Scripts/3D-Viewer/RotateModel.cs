using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float mouseRotationSpeed = 10f;
    public float touchRotationSpeed = 0.5f;
    
    public Boolean rotateXAxis = true;
    public Boolean rotateYAxis = false;
    
    public Camera cam;
    private void OnMouseDrag()
    {
        float rotationX = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float rotationY = Input.GetAxis("Mouse Y") * mouseRotationSpeed;
        
        Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
        Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);

        if (rotateYAxis)
        {
            transform.rotation = Quaternion.AngleAxis(-rotationX, up) * transform.rotation;
        }

        if (rotateXAxis)
        {
            transform.rotation = Quaternion.AngleAxis(-rotationY, right) * transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float deltaY = touch.deltaPosition.y;
            transform.Rotate(transform.rotation.x, transform.rotation.y + deltaY, transform.rotation.z);
        }
    }
}
