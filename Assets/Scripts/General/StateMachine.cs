using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;

public enum State
{
    Idle,
    Full,
    
    CollectSearch,
    CollectWalk,
    Collect,
    
    WalkToTarget,
    TargetReached,
    
    HuntSearch,
    Hunt,
}

public class StateMachine : MonoBehaviour
{
    public State state = State.Idle;
    
    public float speed            = 1.0f;
    public int collect_speed      = 2;
    public int resource_capacity  = 50;
    public int resource_count     = 0;
    
    string collect_tag = "";
    string hunt_tag    = "";
    Vector3Int walk_to;
    GameObject hunt_target = null;
    
    float collect_timer = 0;
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }
    
    public void Idle()
    {
        state = State.Idle;
    }
    
    public void Collect(string tag)
    {
        state = State.CollectSearch;
        collect_tag = tag;
    }
    
    public void WalkTo(Vector3Int position)
    {
        state = State.WalkToTarget;
        walk_to = position;
    }
    
    public void Hunt(string tag)
    {
        state = State.HuntSearch;
        hunt_tag = tag;
    }
    
    public GameObject FindGO(string tag)
    {
        GameObject result = null;
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        if(objects.Length > 0)
        {
            GameObject nearest = objects[0];
            float nearest_distance = (nearest.transform.position - transform.position).magnitude;
            
            for(int i = 1; i < objects.Length; i++)
            {
                float distance = (objects[i].transform.position - transform.position).magnitude;
                if(distance < nearest_distance)
                {
                    nearest = objects[i];
                    nearest_distance = distance;
                }
            }
            
            result = nearest;
        }
        
        return result;
    }
    
    public Vector3Int Find(string tag, ref bool found)
    {
        GameObject nearest = FindGO(tag);
        Vector3Int result = new Vector3Int(0, 0, 0);
        found = false;
        
        if(nearest != null)
        {
            result = tilemap.WorldToCell(nearest.transform.position);
            found = true;
        }
        
        return result;
    }
    
    bool AloneObject(Vector3 position)
    {
        Vector3Int target = tilemap.WorldToCell(position);
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        for(int i = 0; i < objects.Length; i++)
        {
            if(objects[i] != gameObject)
            {
                StateMachine other = objects[i].GetComponent<StateMachine>();
                if((other.state == State.Collect || other.state == State.CollectWalk) && other.walk_to == target)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
    
    Vector3Int FindIsolated(string tag, ref bool found)
    {
        Vector3Int result = new Vector3Int(0, 0, 0);
        found = false;
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        if(objects.Length > 0)
        {
            found = true;
            Vector3 nearest = objects[0].transform.position;
            float nearest_distance = (nearest - transform.position).magnitude;
            
            for(int i = 1; i < objects.Length; i++)
            {
                float distance = (objects[i].transform.position - transform.position).magnitude;
                if(distance < nearest_distance && AloneObject(objects[i].transform.position))
                {
                    nearest = objects[i].transform.position;
                    nearest_distance = distance;
                }
            }
            
            result = tilemap.WorldToCell(nearest);
        }
        
        return result;
    }
    
    public void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    
    void IdleUpdate()
    {
    }
    
    void FullUpdate()
    {
    }
    
    void CollectSearchUpdate()
    {
        bool found = false;
        Vector3Int target = FindIsolated(collect_tag, ref found);
        if(found)
        {
            state = State.CollectWalk;
            walk_to = target;
        }
    }
    
    void CollectWalkUpdate()
    {
        Vector3 world_target = tilemap.CellToWorld(walk_to);
        if((transform.position - world_target).magnitude < 0.00001f)
        {
            state = State.Collect;
            collect_timer = 0;
        }
        else
        {
            MoveTowards(world_target);
        }
    }
    
    void CollectUpdate()
    {
        if(resource_count >= resource_capacity)
        {
            state = State.Full;
        }
        
        Vector3Int grid_position = tilemap.WorldToCell(transform.position);
        GameObject tile_object   = tilemap.GetInstantiatedObject(grid_position);
        
        if(tile_object == null || tile_object.tag != collect_tag)
        {
            state = State.CollectSearch;
        }
        else
        {
            Resource res = tile_object.GetComponent<Resource>();
            
            if(res == null)
            {
                state = State.CollectSearch;
            }
            else
            {
                collect_timer += Time.deltaTime;
                if(collect_timer >= 0.1f)
                {
                    resource_count += res.TakeHealth(Math.Min(resource_capacity - resource_count, collect_speed));
                    collect_timer = 0;
                }
            }
        }
    }
    
    void WalkToTargetUpdate()
    {
        Vector3 world_target = tilemap.CellToWorld(walk_to);
        if((transform.position - world_target).magnitude < 0.00001f)
        {
            state = State.TargetReached;
        }
        else
        {
            MoveTowards(world_target);
        }
    }
    
    void TargetReachedUpdate()
    {
    }
    
    void HuntSearchUpdate()
    {
        hunt_target = FindGO(hunt_tag);
        
        if(hunt_target != null)
        {
            state = State.Hunt;
            collect_timer = 0;
        }
    }
    
    void HuntUpdate()
    {
        if(hunt_target == null)
        {
            state = State.HuntSearch;
        }
        else
        {
            
            if(resource_count >= resource_capacity)
            {
                state = State.Full;
            }
            else
            {
                
                MoveTowards(hunt_target.transform.position);
                if((transform.position - hunt_target.transform.position).magnitude < 0.0001f)
                {
                    StateMachine other = hunt_target.GetComponent<StateMachine>();
                    if(other != null)
                    {
                        collect_timer += Time.deltaTime;
                        if(collect_timer >= 0.1f)
                        {
                            int amount = Math.Min(collect_speed * 4, other.resource_count);
                            amount = Math.Min(resource_capacity - resource_count, amount);
                            
                            other.resource_count -= amount;
                            resource_count += amount;
                            
                            collect_timer = 0;
                        }
                    }
                }
            }
        }
    }
    
    void Update()
    {
        switch(state)
        {
            case State.Idle:          IdleUpdate();          break;
            case State.Full:          FullUpdate();          break;
            case State.CollectSearch: CollectSearchUpdate(); break;
            case State.CollectWalk:   CollectWalkUpdate();   break;
            case State.Collect:       CollectUpdate();       break;
            case State.WalkToTarget:  WalkToTargetUpdate();  break;
            case State.TargetReached: TargetReachedUpdate(); break;
            case State.HuntSearch:    HuntSearchUpdate();    break;
            case State.Hunt:          HuntUpdate();          break;
        }
    }
}
