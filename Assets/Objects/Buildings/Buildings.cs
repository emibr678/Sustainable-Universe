﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Buildings : MonoBehaviour
{
    Tilemap tilemap;
    Tilemap ground_tilemap;
	StateMachine machine;
    
    protected int cost_wood = 0;
    protected int cost_stone = 0;
    
    protected virtual void Start()
    {
        tilemap = GameObject.Find("World").GetComponent<Tilemap>();
        ground_tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
		machine = gameObject.GetComponent<StateMachine>();
    }
    
    protected virtual void Update()
    {
        
    }
	
	public virtual bool Check_canBuild()
	{			
		try {
            GameObject civ = GameObject.Find("CivBase");             
			if(civ != null)
			{
				//hur får man en ref till CivBase för att kunna läsa/skriva resurser
						CivBaseSim civ_base = civ.GetComponent<CivBaseSim>();
						if(civ_base != null)
						{
							if(civ_base.wood >= cost_wood /*&& civ_base.stone > cost_stone*/)
							{
								civ_base.wood -= cost_wood;
								//civ_base.stone -= cost_stone;
								return true;
							}
						}
					
				
			}
			return false;
			
		}       
        catch (NullReferenceException ex) {
            Debug.Log(ex);
        }
		return false;
		
	}
}
