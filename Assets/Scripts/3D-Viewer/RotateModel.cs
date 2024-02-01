using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float mouseRotationSpeed = 10f;
    public float mouseScrollSpeed = 1f;
    public float touchRotationSpeed = 0.5f;
    
    public GameObject cameraCenter;
    public GameObject cameraRail;
    public Camera cam;
    private void OnMouseDrag()
    {
        float rotationX = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float rotationY = Input.GetAxis("Mouse Y") * mouseRotationSpeed;
        
        float cameraCenterRotationY = cameraCenter.transform.localEulerAngles.y + rotationX;
        float cameraRailRotationX =  Mathf.Clamp(cameraRail.transform.localEulerAngles.x - rotationY, 5f, 45f);

        cameraCenter.transform.localEulerAngles = new Vector3(0f,cameraCenterRotationY,0f); 
        cameraRail.transform.localEulerAngles = new Vector3(cameraRailRotationX,0f, 0f); 
    }

    void OnGUI()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float scrollY = Input.mouseScrollDelta.y * mouseScrollSpeed;
            float cameraZoomPositionZ = Mathf.Clamp(cam.transform.localPosition.z - scrollY, -10f, -2f);
            cam.transform.localPosition = new Vector3(0f,0f,cameraZoomPositionZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float deltaY = touch.deltaPosition.y * touchRotationSpeed;
            float deltaX = touch.deltaPosition.x * touchRotationSpeed;
            float cameraCenterRotationY = cameraCenter.transform.localEulerAngles.y + deltaX;
            float cameraRailRotationX =  Mathf.Clamp(cameraRail.transform.localEulerAngles.x - deltaY, 5f, 45f);

            cameraCenter.transform.localEulerAngles = new Vector3(0f,cameraCenterRotationY,0f); 
            cameraRail.transform.localEulerAngles = new Vector3(cameraRailRotationX,0f, 0f); 
        }
    }
}
