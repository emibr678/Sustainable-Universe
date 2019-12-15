using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placer : MonoBehaviour
{
    public string obj;
    
    void Start()
    {
        Instantiate(Resources.Load("Prefabs/" + obj), transform.position, Quaternion.identity);
        Tilemap tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        Vector3Int grid_position = tilemap.WorldToCell(transform.position);
        tilemap.SetTile(grid_position, null);
    }
}
