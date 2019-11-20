using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;

[RequireComponent(typeof(MoveToTile))]
[RequireComponent(typeof(FindNearest))]
public class ResourceCollecter : MonoBehaviour
{
    public string[] CollectTags;
    
    public float speed           = 1.0f;
    public int collect_speed     = 2;
    public int resource_capacity = 50;
    
    public int resource_count = 0;
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    MoveToTile mover;
    FindNearest finder;
    
    float timer = 0;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        
        mover  = gameObject.GetComponent<MoveToTile>();
        finder = gameObject.GetComponent<FindNearest>();
    }

    int Collect()
    {
        Vector3Int grid_position = tilemap.WorldToCell(transform.position);
        GameObject tile_object   = tilemap.GetInstantiatedObject(grid_position);
        
        if(tile_object != null)
        {
            Resource res = tile_object.GetComponent<Resource>();
            if(res != null)
            {
                int collected = res.TakeHealth(Math.Min(resource_capacity - resource_count, collect_speed));
                return collected;
            }
        }
        
        return 0;
    }
    
    void Update()
    {
        if(resource_count < resource_capacity && mover.MovingDone() && CollectTags.Length > 0)
        {
            Vector3Int grid_position = tilemap.WorldToCell(transform.position);
            
            Vector3Int target = finder.FindIsolated(CollectTags[0]);
            for(int i = 1; i < CollectTags.Length; i++)
            {
                Vector3Int pos = finder.FindIsolated(CollectTags[i]);
                if((pos - grid_position).sqrMagnitude < (target - grid_position).sqrMagnitude)
                {
                    target = pos;
                }
            }
            
            if(target == grid_position)
            {
                timer += Time.deltaTime;
                if(timer >= 0.1f)
                {
                    resource_count += Collect();
                    timer = 0;
                }
            }
            else
            {
                mover.MoveTo(target, speed);
            }
        }
    }
}
