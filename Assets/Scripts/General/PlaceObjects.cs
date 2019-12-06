using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceObjects : MonoBehaviour
{
	public Tilemap tileMap;
	
    [SerializeField]
    public Tile[] placeableTile;

    private Tile currentPlaceableTile;

    private int currentIndex = -1;

    private void Update()
    {
        HandleHotkey();

        if (currentPlaceableTile != null) // if a new key has been pressed 
        {
            MoveCurrentObjectToMouse();
            ReleaseIfClicked();
        }
    }

    private void HandleHotkey()
    {
        for (int i = 0; i < placeableTile.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (PressedKeyIsCurrent(i)) // press the same key to undo, else a new key is pressed
                {
                    Destroy(currentPlaceableTile);
                    currentIndex = -1;
                } 
                else
                {
                    if (currentPlaceableTile != null)
                    {
                        Destroy(currentPlaceableTile);
                    }

                    currentPlaceableTile = Instantiate(placeableTile[i]);
                    currentIndex = i;
                }

                break;
            }
        }
    }

    private bool PressedKeyIsCurrent(int i)
    {
        return currentPlaceableTile != null && currentIndex == i;
    }

	// lets user place tile on mouse position, if tile is empty
    private void MoveCurrentObjectToMouse()
    {
		Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;		
		Vector3Int currentCell = tileMap.WorldToCell(mousepos);		

		// TODO: Hur ska man göra för att rita ut det man vill placera, så att man kan se vad det är.
		//Tile test = Instantiate(currentPlaceableTile, currentCell, Quaternion.identity);	
		//tileMap.SetTile(currentCell, test);	

		if(Input.GetMouseButtonDown(0))
        {
			if(!tileMap.HasTile(currentCell))
				Check_SetTile(currentCell,currentPlaceableTile);
		}
		//Destroy(test); 

    }
	
	// checks if there is enough resources in civbase from Building.Check_canBuild()
	private void Check_SetTile(Vector3Int cell, Tile thisTile)
	{
		Building script_Building = thisTile.gameObject.GetComponent<Building>();
		if(script_Building != null)
		{
			bool enough_resources = script_Building.Check_canBuild();
			if(enough_resources)
			{	
				tileMap.SetTile(cell, thisTile);
			}
			else
				Debug.Log("Not enough resources for " + thisTile.name);			
		}
		else
			Debug.Log("Something went wrong"); //kommer hit för att scriptet "Building" inte finns i objektet //script som ärver från building fungerar
		
	}
	
	// set currentPlaceableTile = null when mouse released
    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableTile = null;
        }
    }

    public void Pressed()
    { //instansiate with element 0
    //use old functions
        Debug.Log("Hello world");
    }
}