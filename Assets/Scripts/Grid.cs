using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int gridSizeX, gridSizeY;
    public Vector2 origine;

    public GameObject tilePrefab;
    public GameObject pointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        for (int i = 0; i < gridSizeX * gridSizeY; i++)
        {
            Vector2 tilePosition = new Vector2();
    
            tilePosition.x = (i % gridSizeX) + origine.x;
            tilePosition.y = (i / gridSizeX) + origine.y;

            GameObject gridTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
            gridTile.transform.SetParent(transform);
            gridTile.transform.localScale = new Vector3(2, 2, 1);

        }

        for (int i = 0; i < (gridSizeX + 1) * (gridSizeY + 1); i++)
        {
            Vector2 pointPosition = new Vector2();
    
            pointPosition.x = (i % (gridSizeX + 1) + origine.x - 0.5f;
            pointPosition.y = (i / (gridSizeX + 1) + origine.y - 0.5f;

            GameObject gridPoint = Instantiate(pointPrefab, pointPosition, Quaternion.identity);
            gridPoint.transform.SetParent(transform);
            gridPoint.transform.localScale = new Vector3(2, 2, 1);
            
            // handler touch options
            gridPoint.AddComponent<PointTouch>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}