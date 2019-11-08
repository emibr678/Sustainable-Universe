using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FindNearest : MonoBehaviour
{
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }
    
    public Vector3Int Find(string tag)
    {
        Vector3Int result = new Vector3Int(0, 0, 0);
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        if(objects.Length > 0)
        {
            Vector3 nearest = objects[0].transform.position;
            float nearest_distance = (nearest - transform.position).magnitude;
            
            for(int i = 1; i < objects.Length; i++)
            {
                float distance = (objects[i].transform.position - transform.position).magnitude;
                if(distance < nearest_distance)
                {
                    nearest = objects[i].transform.position;
                    nearest_distance = distance;
                }
            }
            
            result = tilemap.WorldToCell(nearest);
        }
        
        return result;
    }
}
