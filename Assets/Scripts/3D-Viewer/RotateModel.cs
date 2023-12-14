using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
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
