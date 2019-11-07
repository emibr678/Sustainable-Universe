using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContinuous : MonoBehaviour
{
    public float speed = 30;

    private float outerLeft;
    private float outerRight;
    private float outerDown;
    private float outerUp;
    
    void Start()
    {
        outerLeft = GetComponent<CameraDrag>().outerLeft;
        outerRight = GetComponent<CameraDrag>().outerRight;
        outerDown = GetComponent<CameraDrag>().outerDown;
        outerUp = GetComponent<CameraDrag>().outerUp;      
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!Input.GetMouseButtonDown(1))
        {
            if (Input.mousePosition.x <= Screen.width && Input.mousePosition.x >= 0
            && Input.mousePosition.y <= Screen.height && Input.mousePosition.y >= 0)
            {

                if (Input.mousePosition.x >= Screen.width - 15) //move camera right
                {
                    if (this.transform.position.x < outerRight)
                    {
                        Vector3 movex = new Vector3(0.1f * speed, 0, 0);
                        transform.Translate(movex * Time.deltaTime, Space.World);
                    }
                }

                if (Input.mousePosition.x <= 15)
                {
                    if (this.transform.position.x > outerLeft) //move camera left
                    {
                        Vector3 movex = new Vector3(-0.1f * speed, 0, 0);
                        transform.Translate(movex * Time.deltaTime, Space.World);
                    }
                }

                if (Input.mousePosition.y <= 15)
                {
                    if (this.transform.position.y > outerDown) //move camera down
                    {
                        Vector3 movey = new Vector3(0, -0.1f * speed, 0);
                        transform.Translate(movey * Time.deltaTime, Space.World);
                    }
                }

                if (Input.mousePosition.y >= Screen.height - 15) //move camera up
                {
                    if (this.transform.position.y < outerUp)
                    {
                        Vector3 movey = new Vector3(0, 0.1f * speed, 0);
                        transform.Translate(movey * Time.deltaTime, Space.World);
                    }
                }

            }
        }
        
    }
}
