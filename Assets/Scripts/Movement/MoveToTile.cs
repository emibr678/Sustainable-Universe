using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveToTile : MonoBehaviour
{
    Vector3Int target;
    float speed;
    bool moving = false;
    
    public void MoveTo(Vector3Int new_target, float new_speed = 1.0f)
    {
        target = new_target;
        speed  = new_speed;
        moving = true;
    }
    
    public bool MovingDone()
    {
        return !moving;
    }
    
    public Vector3Int GetTarget()
    {
        return target;
    }
    
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }
    
    void Update()
    {
        if(moving)
        {
            Vector3 world_target = tilemap.CellToWorld(target);
            if((transform.position - world_target).magnitude < 0.00001f)
            {
                moving = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, world_target, speed * Time.deltaTime);
            }
        }
    }
}
