using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceObjects : MonoBehaviour
{
	public Tilemap tileMap;
	
    [SerializeField]
    public Tile[] placeableObject;

    private Tile currentPlaceableObject;

    private int currentIndex = -1;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey()
    {
        for (int i = 0; i < placeableObject.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (PressedKeyOfCurrent(i))
                {
                    Destroy(currentPlaceableObject);
                    currentIndex = -1;
                } 
                else
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                    }

                    currentPlaceableObject = Instantiate(placeableObject[i]);
                    currentIndex = i;
                }

                break;
            }
        }
    }

    private bool PressedKeyOfCurrent(int i)
    {
        return currentPlaceableObject != null && currentIndex == i;
    }

    private void MoveCurrentObjectToMouse()
    {
		Sprite sprite = currentPlaceableObject.sprite;
		Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;		
		Vector3Int currentCell = tileMap.WorldToCell(mousepos);
		
		
		if(Input.GetMouseButtonDown(0))
        {
			if(!tileMap.HasTile(currentCell))
				tileMap.SetTile(currentCell, currentPlaceableObject);
		}

    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }
}