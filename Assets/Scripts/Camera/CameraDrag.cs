using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 20;
    private Vector3 dragOrigin;

    public bool cameraDragging = true;

    public float outerLeft = -10f;
    public float outerRight = 10f;
    public float outerUp = 10f;
    public float outerDown = -10f;


    void Update()
    {
        /*
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        float left = Screen.width * 0.2f;
        float right = Screen.width - (Screen.width * 0.2f);

        if (mousePosition.x < left)
        {
            cameraDragging = true;
        }
        else if (mousePosition.x > right)
        {
            cameraDragging = true;
        }*/

        if (cameraDragging)
        {

            if (Input.GetMouseButtonDown(1))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(1)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
           
            // moves in x-axis
            Vector3 movex = new Vector3(pos.x * dragSpeed, 0, 0); 
            if (movex.x > 0f)
            {
                if (this.transform.position.x < outerRight)
                {
                    transform.Translate(movex * Time.deltaTime, Space.World);
                }
            }
            else
            {
                if (this.transform.position.x > outerLeft)
                {
                    transform.Translate(movex * Time.deltaTime, Space.World);
                }
            }

            // moves in y-axis   
            Vector3 movey = new Vector3(0, pos.y * dragSpeed, 0);
            if (movey.y > 0f)
            {
                if (this.transform.position.y < outerUp)
                {
                    transform.Translate(movey * Time.deltaTime, Space.World);
                }
            }
            else
            {
                if (this.transform.position.y > outerDown)
                {
                    transform.Translate(movey * Time.deltaTime, Space.World);
                }
            }
        }
    }

    
}


   