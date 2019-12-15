using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceObjects : MonoBehaviour
{
	public Tilemap tileMap;
    public Tilemap groundTileMap;
    public Tilemap tileUI;
	
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
                HandleInput(i);
                break;
            }
        }
    }
	
	private void HandleInput(int i)
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
	}

    private bool PressedKeyIsCurrent(int i)
    {
        return currentPlaceableTile != null && currentIndex == i;
    }

	// lets user place objecttile on mouse position, if tileposition is empty
    private void MoveCurrentObjectToMouse()
    {
		Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;		
		Vector3Int currentCell = tileMap.WorldToCell(mousepos);		

		// TODO: Hur ska man göra så att man kan se vad som ska sättas ut, så att objektet följer muspekaren.

		if(Input.GetMouseButtonDown(0))
        {
            GameObject ground_object = groundTileMap.GetInstantiatedObject(currentCell);
			if(!tileMap.HasTile(currentCell) && ground_object != null && ground_object.tag == "Grass")
				Check_SetTile(currentCell,currentPlaceableTile);
		}

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
		else if(thisTile.gameObject.GetComponent<Placer>() != null)
		{
			tileMap.SetTile(cell, thisTile);
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

    public void UIHousePressed()
    { //instansiate with element 0 == House tile
        tileUI.ClearAllTiles();
        HandleInput(0);
    }
	
	public void UISheepPressed() 
    { //instansiate with element 1 == Sheep tile
        tileUI.ClearAllTiles();
        HandleInput(1);
    }
}