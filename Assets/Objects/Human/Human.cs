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
    
    public float speed = 1.0f;
    public int chop_speed = 2;
    public int wood_capacity = 50;
    
    int wood_count = 0;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        
        mover = gameObject.GetComponent<MoveToTile>();
        finder = gameObject.GetComponent<FindNearest>();
    }
    
    int ChopTree()
    {
        Vector3Int grid_position = tilemap.WorldToCell(transform.position);
        GameObject tile_object = tilemap.GetInstantiatedObject(grid_position);
        if(tile_object != null)
        {
            TreeSim tree = tile_object.GetComponent<TreeSim>();
            if(tree != null)
            {
                int chopped = tree.TakeHealth(Math.Min(wood_capacity - wood_count, chop_speed));
                return chopped;
            }
        }
        
        return 0;
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
                civ_base.wood += wood_count;
                wood_count = 0;
            }
        }
    }
    
    void Update()
    {
        if(wood_count < wood_capacity)
        {
            if(mover.MovingDone())
            {
                Vector3Int target = finder.Find("Tree");
                Vector3Int grid_position = tilemap.WorldToCell(transform.position);
                if(target == grid_position)
                {
                    wood_count += ChopTree();
                }
                else
                {
                    mover.MoveTo(target, speed);
                }
            }
        }
        else
        {
            if(mover.MovingDone())
            {
                Vector3Int target = finder.Find("CivBase");
                Vector3Int grid_position = tilemap.WorldToCell(transform.position);
                if(target == grid_position)
                {
                    DropOffWood();
                }
                else
                {
                    mover.MoveTo(target, speed);
                }
            }
        }
    }
}
