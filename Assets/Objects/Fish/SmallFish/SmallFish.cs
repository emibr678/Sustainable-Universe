using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;

public class SmallFish : MonoBehaviour
{
    public int resource_loss_rate = 1;
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    StateMachine machine;
    
    public float timer = 0;
    public float reproduce_time = 0;
    public int fishCount = 5;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        

        machine = gameObject.GetComponent<StateMachine>();
        machine.state = State.Idle;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= reproduce_time && StateMachine.fishCount <= 30)
        {
            Instantiate(Resources.Load("Prefabs/SmallFish"), transform.position, Quaternion.identity);
            StateMachine.fishCount++;
            //machine.resource_count = 75;
            timer = 0;
            //machine.Collect("SmallFish");
        }
        
        if(machine.resource_count <= 0)
        {
            Destroy(gameObject);
            StateMachine.fishCount--;
        }
    }

}
