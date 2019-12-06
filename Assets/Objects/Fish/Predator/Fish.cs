using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;

public class Fish : MonoBehaviour
{
    public int resource_loss_rate = 1;
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    GameObject smallFish;
    Collider smallFishCollider;
    Collider myCollider;
    StateMachine machine;
    
    float loss_timer = 0;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        
        machine = gameObject.GetComponent<StateMachine>();
        smallFish = GameObject.Find("SmallFish");
        smallFishCollider = smallFish.GetComponent<Collider>();
        myCollider = gameObject.GetComponent<Collider>();
        machine.state = State.Idle;
    }
    
    void Update()
    {
        if(machine.state == State.Idle)
        {
            machine.Collect("SmallFish");
        }
        
        if(machine.state != State.Collect)
        {
            loss_timer += Time.deltaTime;
            if(loss_timer >= 0.1f)
            {
                machine.resource_count = Math.Max(machine.resource_count - resource_loss_rate, 0);
                
                loss_timer = 0;
            }
        }
        
        if(machine.state == State.Full)
        {
            Instantiate(Resources.Load("Prefabs/Fish"), transform.position, Quaternion.identity);
            machine.resource_count = 75;
            machine.Collect("SmallFish");
        }
        
        if(machine.resource_count <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.tag == "SmallFish")
      {
          Physics.IgnoreCollision(smallFishCollider, myCollider);
      }
  }
}
