using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
	// increase population
	// private int incPopulation = 5;
	
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();   
    }

    // Update is called once per frame
   /* protected override void Update()
    {
        base.Update();		
    }*/
	
	public override bool Check_canBuild()
    {		
		cost_wood = 500;
		cost_stone = 0;  
        return base.Check_canBuild();	
    }
}
