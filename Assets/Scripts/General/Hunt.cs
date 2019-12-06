using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;


public class Hunt : MonoBehaviour
{
    public int food_consumption_rate = 1;
    public string target;
    int randomNumber;
    
    StateMachine machine;
    
    float consumption_timer = 0;
    int starvation = 100;
    
    void Start()
    {   
        machine = gameObject.GetComponent<StateMachine>();
    }
    
    void IdleUpdate()
    {
        machine.Idle();
    }
    
   
    void HunterUpdate()
    {
        if(machine.state == State.Idle && machine.resource_count <= 50)
        {
            machine.Hunt(target);
        }

        if (machine.state == State.Full)
        {
            //machine.Idle();
            if (randomNumber < 200)
            {
                Instantiate(Resources.Load("Prefabs/Fish"), transform.position, Quaternion.identity);
                machine.resource_count = 50;
            }
            else
            {
                machine.Idle();
                machine.resource_count = 50;
            }
        }
        
    }
    
    void HungerUpdate()
    {
        consumption_timer += Time.deltaTime;
        if(consumption_timer >= 0.5f)
        {  
            consumption_timer = 0;
        }
        
        if(starvation <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        randomNumber = UnityEngine.Random.Range(0, 1000);
        HunterUpdate();
        HungerUpdate();
    }
}