using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContinuous : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Screen Width : " + Screen.width);
        Debug.Log("Screen hiegh : " + Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x > Screen.width -10)
        {
            Debug.Log("TEST");
        }

      

    }
}
