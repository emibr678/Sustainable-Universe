using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CivBaseSim : MonoBehaviour
{
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    public int wood = 0;
	public int stone = 0;
    public int food = 100;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }
    
    void Update()
    {
        
    }
}
