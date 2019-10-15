using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mark : MonoBehaviour
{
    Tilemap world;
    Tilemap ground;
    Tilemap tile_ui;
    
    void Start()
    {
        world   = GameObject.Find("World").GetComponent<Tilemap>();
        ground  = GameObject.Find("Ground").GetComponent<Tilemap>();
        tile_ui = GameObject.Find("TileUI").GetComponent<Tilemap>();
    }

    void Update()
    {
        
    }
}
