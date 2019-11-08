using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeSim : MonoBehaviour
{
    Tilemap tilemap;
    Tilemap ground_tilemap;
    Vector3Int grid_position;
    
    public int health;
    
    public int TakeHealth(int want)
    {
        if(health > want)
        {
            health -= want;
        }
        else
        {
            want = health;
            health = 0;
        }
        
        return want;
    }
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        grid_position = tilemap.WorldToCell(transform.position);
    }
    
    void Update()
    {
        if(health <= 0)
        {
            tilemap.SetTile(grid_position, null);
        }
        else
        {
            Vector3Int[] neighbors_directions = new Vector3Int[]
            {
                new Vector3Int(1, 0, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(0, 1, 0),
                new Vector3Int(0, -1, 0)
            };
            
            foreach(Vector3Int dir in neighbors_directions)
            {
                Vector3Int neighbor_position = grid_position + dir;
                Tile tile = (Tile)tilemap.GetTile(neighbor_position);
                Tile ground_tile = (Tile)ground_tilemap.GetTile(neighbor_position);
                
                if(tile == null && ground_tile != null)
                {
                    //Assume grass for now.
                    if(Random.Range(0.0f, 100.0f) < 0.01f)
                    {
                        tilemap.SetTile(neighbor_position, tilemap.GetTile(grid_position));
                    }
                }
            }
        }
    }
}
