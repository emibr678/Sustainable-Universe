using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{

    public KeyCode speedUp;
    public KeyCode slowDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(speedUp)){
        Time.timeScale += 0.8f;
        Debug.Log("Current TimeScale");
        Debug.Log(Time.timeScale);
        }

        if(Input.GetKey(slowDown) && Time.timeScale > 0.1f){
        Time.timeScale -= 0.1f;
        Debug.Log("Current TimeScale");
        Debug.Log(Time.timeScale);
        }
    }
}
