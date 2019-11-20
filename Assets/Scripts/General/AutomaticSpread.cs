using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutomaticSpread : MonoBehaviour
{
    public string[] acceptable_ground;
    public float spread_probability = 20.0f;
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    Vector3Int grid_position;
    
    float timer = 0.0f;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        grid_position = tilemap.WorldToCell(transform.position);
        
        spread_probability /= 4.0f;
    }
    
    bool AcceptableGround(string tag)
    {
        for(int i = 0; i < acceptable_ground.Length; i++)
        {
            if(acceptable_ground[i] == tag)
            {
                return true;
            }
        }
        
        return false;
    }
    
    bool Spread()
    {
        return Random.Range(0.0f, 100.0f) < spread_probability;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= 0.1f)
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
                GameObject tile_object   = tilemap.GetInstantiatedObject(neighbor_position);
                GameObject ground_object = ground_tilemap.GetInstantiatedObject(neighbor_position);
                
                if(tile_object == null && 
                   ground_object != null && 
                   AcceptableGround(ground_object.tag) && 
                   Spread())
                {
                    tilemap.SetTile(neighbor_position, tilemap.GetTile(grid_position));
                }
            }
            
            timer = 0;
        }
    }
}
