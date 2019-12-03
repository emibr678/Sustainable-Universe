using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Buildings
{
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		cost_wood = 500;
		cost_stone = 0;      
    }

    // Update is called once per frame
   /* protected override void Update()
    {
        base.Update();		
    }*/
	
	public override bool Check_canBuild()
    {
        return base.Check_canBuild();	
    }
}
