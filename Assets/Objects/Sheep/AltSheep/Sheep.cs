using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//using System;

public class Sheep : MonoBehaviour
{
    Tilemap tilemap;
    Tilemap ground_tilemap;
    MoveToTile mover;
    FindNearest finder;

     public Vector3 targetPos;
     public bool isMoving = false;
     public float maxRange = 0.1f;
     public float waitTime = 0.1f;
     public float randomSpeed = 0.05f;
    
    public float speed = 1.0f;
    public int chop_speed = 2;
    public int wood_capacity = 10;
    
    int wood_count = 0;
    
    void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        
        mover = gameObject.GetComponent<MoveToTile>();
        finder = gameObject.GetComponent<FindNearest>();
    }
    
    int ChopTree()
    {
        Vector3Int grid_position = tilemap.WorldToCell(transform.position);
        GameObject tile_object = tilemap.GetInstantiatedObject(grid_position);
        if(tile_object != null)
        {
            DeepGrass1 DeepGrass = tile_object.GetComponent<DeepGrass1>();
            if(DeepGrass != null)
            {
                int chopped = DeepGrass.TakeHealth(10);
                return chopped;
            }
        }
        
        return 0;
    }
    
      void DropOffWood()
    {
        Vector3Int grid_position = tilemap.WorldToCell(transform.position);
        GameObject tile_object = tilemap.GetInstantiatedObject(grid_position);
        if(tile_object != null)
        {
            DeepGrass1 civ_base = tile_object.GetComponent<DeepGrass1>();
            if(civ_base != null)
            {
                //civ_base.wood += wood_count;
                wood_count = 0;
            }
        }
    }
    
    void Update()
    {
      

        if(wood_count < wood_capacity)
        {
            if(mover.MovingDone())
            {
                Vector3Int target = finder.Find("DeepGrass");
                Vector3Int grid_position = tilemap.WorldToCell(transform.position);
                if(target == grid_position)
                {
                    wood_count += ChopTree();
                }
                else
                {
                    mover.MoveTo(target, speed);
                }
            }
        }
        else
        {
            if(mover.MovingDone())
            {
                Vector3Int target = finder.Find("DeepGrass");
                Vector3Int grid_position = tilemap.WorldToCell(transform.position);
                 if(target == grid_position)
                {
                    //DropOffWood();
                    wood_count = 0;
                    FindNewTargetPos();
                    //wood_count = 0;
                    mover.MoveTo(target, speed);
                } else {
                mover.MoveTo(target, speed);
                }
                
            }
        }
    
    }

      
     
     private void FindNewTargetPos() {
         Vector3 pos = transform.position;
         targetPos = new Vector3();
         targetPos.x  = Random.Range(pos.x - maxRange, pos.x + maxRange);
         targetPos.y = pos.y;
         targetPos.z = Random.Range(pos.z - maxRange, pos.z + maxRange);
 
         transform.LookAt(targetPos);
         StartCoroutine(Move());
         
     }
     
     IEnumerator Move() {
         isMoving = true;
         
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * randomSpeed) {
 
             transform.position = Vector3.MoveTowards(transform.position, targetPos, t);
             yield return null;
         }
         
         yield return new WaitForSeconds(waitTime);
         isMoving = false;
         yield return null;
     }
     
}
