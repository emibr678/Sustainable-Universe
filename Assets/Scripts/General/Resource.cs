using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Resource : MonoBehaviour
{
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
    
    Tilemap tilemap;
    Vector3Int grid_position;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        grid_position = tilemap.WorldToCell(transform.position);
    }
    
    void LateUpdate()
    {
        if(health <= 0)
        {
            tilemap.SetTile(grid_position, null);
        }
    }
}
