using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10.0f;
    
    void Update()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");
        float length = dir.magnitude;
        if(length > 0.001f)
        {
            dir /= length;
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
