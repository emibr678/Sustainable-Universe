using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 4.0f;
    public float MinSize = 2.0f;
    public float MaxSize = 5.0f;


    void Update()
    {
        Camera cam = gameObject.GetComponent<Camera>();
        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");
        float length = dir.magnitude;
        if(length > 0.001f)
        {
            dir /= length;
            transform.position += dir * speed * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(cam.orthographicSize < MaxSize)
                cam.orthographicSize += 0.1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cam.orthographicSize > MinSize)
                cam.orthographicSize -= 0.1f;
        }
    }
}
