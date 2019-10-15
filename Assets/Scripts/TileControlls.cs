using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileControls : MonoBehaviour
{
    public Tile mark_tile;
    
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
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 world_mouse = Camera.main.ScreenToWorldPoint(mouse);
            world_mouse.z = 0;
            Vector3Int tile_mouse = tile_ui.WorldToCell(world_mouse);
            bool set = tile_ui.GetTile(tile_mouse) == null;
            tile_ui.ClearAllTiles();
            if(set)
            {
                tile_ui.SetTile(tile_mouse, mark_tile);
            }
        }
    }
}
