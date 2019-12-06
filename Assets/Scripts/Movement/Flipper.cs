using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public bool default_left = true;
    public float threshold = 0.00001f;
    
    float past_x = 0;
    SpriteRenderer srenderer;
    
    void Start()
    {
        past_x = transform.position.x;
        
        srenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float delta = transform.position.x - past_x;

        if(Mathf.Abs(delta) > threshold)
        {
            srenderer.flipX = (delta > 0) == default_left;
        }
        
        past_x = transform.position.x;
    }
}
