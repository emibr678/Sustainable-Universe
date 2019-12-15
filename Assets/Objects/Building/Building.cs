using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    Tilemap tilemap;
    Tilemap ground_tilemap;
    
    protected int cost_wood = 0;
    protected int cost_stone = 0;
    
    protected virtual void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        Instantiate(Resources.Load("Prefabs/IdleHuman"), transform.position + new Vector3(0, -0.25f, 0), Quaternion.identity);
    }
    
    protected virtual void Update()
    {
        
    }
	
	public virtual bool Check_canBuild()
	{	
		CivBaseSim civ_base = GameObject.FindWithTag("CivBase").GetComponent<CivBaseSim>();
		if(civ_base != null)
		{
			if(civ_base.wood >= cost_wood && civ_base.stone >= cost_stone)
			{
				civ_base.wood -= cost_wood;
				civ_base.stone -= cost_stone;
				return true;
			}
		}
		return false;		
	}
    
	public virtual int GetWoodCost()
	{
		return cost_wood;
	}
	public virtual int GetStoneCost()
	{
		return cost_stone;
	}
}
