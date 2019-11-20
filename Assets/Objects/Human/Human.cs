using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using System;

public class Human : MonoBehaviour
{
    Tilemap tilemap;
    Tilemap ground_tilemap;
    MoveToTile mover;
    FindNearest finder;
    
    ResourceCollecter collecter;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        
        mover = gameObject.GetComponent<MoveToTile>();
        finder = gameObject.GetComponent<FindNearest>();
        collecter = gameObject.GetComponent<ResourceCollecter>();
    }
    
    void DropOffWood()
    {
        Vector3Int grid_position = tilemap.WorldToCell(transform.position);
        GameObject tile_object = tilemap.GetInstantiatedObject(grid_position);
        if(tile_object != null)
        {
            CivBaseSim civ_base = tile_object.GetComponent<CivBaseSim>();
            if(civ_base != null)
            {
                civ_base.wood += collecter.resource_count;
                collecter.resource_count = 0;
            }
        }
    }
    
    void Update()
    {
        if(collecter.resource_count >= collecter.resource_capacity && mover.MovingDone())
        {
            Vector3Int target = finder.Find("CivBase");
            Vector3Int grid_position = tilemap.WorldToCell(transform.position);
            if(target == grid_position)
            {
                DropOffWood();
            }
            else
            {
                mover.MoveTo(target, collecter.speed);
            }
        }
    }
}