using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Mark : MonoBehaviour
{
    Tilemap world;
    Tilemap ground;
    Tilemap tile_ui;
    Vector3Int grid_position;
    
    GameObject ground_information;
    GameObject object_information;
    
    void Start()
    {
        world   = GameObject.Find("World").GetComponent<Tilemap>();
        ground  = GameObject.Find("Ground").GetComponent<Tilemap>();
        tile_ui = GameObject.Find("TileUI").GetComponent<Tilemap>();
        
        ground_information = transform.Find("Ground Information").gameObject;
        object_information = transform.Find("Object Information").gameObject;
        
        grid_position = tile_ui.WorldToCell(transform.parent.position);
    }

    void Update()
    {
        Tile tile = (Tile)world.GetTile(grid_position);
        if(tile != null)
        {
            GameObject marked_object = tile.gameObject;
            
            switch(marked_object.tag)
            {
                case "Tree":
                {
                    object_information.GetComponent<Text>().text = "Object: Tree";
                } break;
                
                default:
                {
                    object_information.GetComponent<Text>().text = "Uknown object type";
                } break;
            }
        }
        else
        {
            object_information.GetComponent<Text>().text = "";
        }
        
        tile = (Tile)ground.GetTile(grid_position);
        if(tile != null)
        {
            GameObject marked_ground = tile.gameObject;
            
            switch(marked_ground.tag)
            {
                case "Grass":
                {
                    ground_information.GetComponent<Text>().text = "Ground: Grass";
                } break;
                
                default:
                {
                    ground_information.GetComponent<Text>().text = "Uknown ground type";
                } break;
            }
        }
        else
        {
            ground_information.GetComponent<Text>().text = "";
        }
    }
}
