using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;

public class Sheep : MonoBehaviour
{
    public int resource_loss_rate = 1;
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    StateMachine machine;
    
    float loss_timer = 0;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        
        machine = gameObject.GetComponent<StateMachine>();
        machine.state = State.Idle;
    }
    
    void Update()
    {
        if(machine.state == State.Idle)
        {
            machine.Collect("DeepGrass");
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
            Instantiate(Resources.Load("Prefabs/Sheep"), transform.position, Quaternion.identity);
            machine.resource_count = 75;
            machine.Collect("DeepGrass");
        }
        
        if(machine.resource_count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
