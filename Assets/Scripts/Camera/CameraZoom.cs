using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float ZoomSpeed = 0.3f;
    public float MinSize = 2.0f;
    public float MaxSize = 5.0f;

    void Update()
    {
        Camera cam = gameObject.GetComponent<Camera>();
		if (Input.mousePosition.x <= Screen.width && Input.mousePosition.x >= 0
        && Input.mousePosition.y <= Screen.height && Input.mousePosition.y >= 0)
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				if (cam.orthographicSize < MaxSize)
					cam.orthographicSize += ZoomSpeed;
			}

			if (Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				if (cam.orthographicSize > MinSize)
					cam.orthographicSize -= ZoomSpeed;
			}
		}			      
    }
}
