using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sheep : MonoBehaviour
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
        
        mover     = gameObject.GetComponent<MoveToTile>();
        finder    = gameObject.GetComponent<FindNearest>();
        collecter = gameObject.GetComponent<ResourceCollecter>();
    }
    
    void Update()
    {
        if(collecter.resource_count >= collecter.resource_capacity && mover.MovingDone())
        {
            collecter.resource_count = 0;
        }
    }
}
