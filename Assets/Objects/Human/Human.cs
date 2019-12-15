using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;

public enum HumanJob
{
    Idle,
    Lumberjack,
    Hunter
}

public class Human : MonoBehaviour
{
    public int food_consumption_rate = 1;
    public HumanJob job = HumanJob.Idle;
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    StateMachine machine;
    
    float consumption_timer = 0;
    int starvation = 100;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        
        machine = gameObject.GetComponent<StateMachine>();
    }
    
    void IdleUpdate()
    {
        machine.Idle();
    }
    
    void LumberjackUpdate()
    {
        if(machine.state == State.Idle)
        {
            machine.Collect("Tree");
        }
        
        if(machine.state == State.Full)
        {
            bool found_civbase = false;
            bool found_house   = false;
            Vector3Int civ_base_position = machine.Find("CivBase", ref found_civbase);
            Vector3Int house_position    = machine.Find("House", ref found_house);
            if(found_civbase && found_house)
            {
                float base_dist = (tilemap.CellToWorld(civ_base_position) - transform.position).magnitude;
                float house_dist = (tilemap.CellToWorld(house_position) - transform.position).magnitude;
                
                if(house_dist < base_dist)
                {
                    machine.WalkTo(house_position);
                }
                else
                {
                    machine.WalkTo(civ_base_position);
                }
            }
            else
            {
                machine.WalkTo(civ_base_position);
            }
        }
        
        if(machine.state == State.TargetReached)
        {
            bool found       = false;
            bool found_house = false;
            
            Vector3Int civ_base_position = machine.Find("CivBase", ref found);
            Vector3Int house_position    = machine.Find("House", ref found_house);
            if(found || found_house)
            {
                Vector3Int grid_position = tilemap.WorldToCell(transform.position);
                if(civ_base_position == grid_position || house_position == grid_position)
                {
                    GameObject tile_object = tilemap.GetInstantiatedObject(civ_base_position);
                    if(tile_object != null)
                    {
                        CivBaseSim civ_base = tile_object.GetComponent<CivBaseSim>();
                        if(civ_base != null)
                        {
                            civ_base.wood += machine.resource_count;
                            machine.resource_count = 0;
                        }
                    }
                }
                
                machine.Collect("Tree");
            }
        }
    }
    
    void HunterUpdate()
    {
        if(machine.state == State.Idle)
        {
            machine.Hunt("Sheep");
        }
        
        if(machine.state == State.Full)
        {
            bool found_civbase = false;
            bool found_house   = false;
            Vector3Int civ_base_position = machine.Find("CivBase", ref found_civbase);
            Vector3Int house_position    = machine.Find("House", ref found_house);
            if(found_civbase && found_house)
            {
                float base_dist = (tilemap.CellToWorld(civ_base_position) - transform.position).magnitude;
                float house_dist = (tilemap.CellToWorld(house_position) - transform.position).magnitude;
                
                if(house_dist < base_dist)
                {
                    machine.WalkTo(house_position);
                }
                else
                {
                    machine.WalkTo(civ_base_position);
                }
            }
            else
            {
                machine.WalkTo(civ_base_position);
            }
        }
        
        if(machine.state == State.TargetReached)
        {
            bool found       = false;
            bool found_house = false;
            
            Vector3Int civ_base_position = machine.Find("CivBase", ref found);
            Vector3Int house_position    = machine.Find("House", ref found_house);
            if(found || found_house)
            {
                Vector3Int grid_position = tilemap.WorldToCell(transform.position);
                if(civ_base_position == grid_position || house_position == grid_position)
                {
                    GameObject tile_object = tilemap.GetInstantiatedObject(civ_base_position);
                    if(tile_object != null)
                    {
                        CivBaseSim civ_base = tile_object.GetComponent<CivBaseSim>();
                        if(civ_base != null)
                        {
                            civ_base.food += machine.resource_count;
                            machine.resource_count = 0;
                        }
                    }
                }
            }
            
            machine.Hunt("Sheep");
        }
    }
    
    void HungerUpdate()
    {
        consumption_timer += Time.deltaTime;
        if(consumption_timer >= 0.5f)
        {
            bool found = false;
            Vector3Int civ_base_position = machine.Find("CivBase", ref found);
            if(found)
            {
                GameObject tile_object = tilemap.GetInstantiatedObject(civ_base_position);
                if(tile_object != null)
                {
                    CivBaseSim civ_base = tile_object.GetComponent<CivBaseSim>();
                    if(civ_base != null)
                    {
                        if(civ_base.food > food_consumption_rate)
                        {
                            civ_base.food -= food_consumption_rate;
                            starvation = 100;
                        }
                        else
                        {
                            starvation--;
                        }
                    }
                }
            }
            
            consumption_timer = 0;
        }
        
        if(starvation <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        switch(job)
        {
            case HumanJob.Idle:       IdleUpdate(); break;
            case HumanJob.Lumberjack: LumberjackUpdate(); break;
            case HumanJob.Hunter:     HunterUpdate(); break;
        }
        HungerUpdate();
    }
}