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

    public Sprite blueTile;

    private GameObject[,] gridPoints;
    private List<GameObject> gridTiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        origine = new Vector2(-8, -3);
        CreateGrid();
    }

    public void CreateGrid()
    {
        gridPoints = new GameObject[gridSizeX, gridSizeY];

        // draw tiles
        for (int i = 0; i < gridSizeX * gridSizeY; i++)
        {
            Vector2 tilePosition = new Vector2();
    
            tilePosition.x = (i % gridSizeX) + origine.x;
            tilePosition.y = (i / gridSizeX) + origine.y;

            GameObject gridTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
            gridTile.transform.SetParent(transform);
            gridTile.transform.localScale = new Vector3(2, 2, 1);

            gridTiles.Add(gridTile);

        }

        // draw points
        for (int i = 0; i < (gridSizeX + 1) * (gridSizeY + 1); i++)
        {
            Vector2 pointPosition = new Vector2();
    
            pointPosition.x = (i % (gridSizeX + 1)) + origine.x - 0.5f;
            pointPosition.y = (i / (gridSizeX + 1)) + origine.y - 0.5f;

            GameObject gridPoint = Instantiate(pointPrefab, pointPosition, Quaternion.identity);
            gridPoint.transform.SetParent(transform);
            gridPoint.transform.localScale = new Vector3(2, 2, 1);
            
            // handler touch options
            //PointTouch touchHandler = gridPoint.AddComponent<PointTouch>();
            //touchHandler.grid = this;

            //touchHandler.pointX = (int)pointPosition.x;
            //touchHandler.pointY = (int)pointPosition.y;

            //touchHandler.pointX = i % gridSizeX;
            //touchHandler.pointY = i / gridSizeX;

        }
    }

    public void UpdateTileSprite(int x, int y)
    {
        int index = (y + (int)origine.y) * gridSizeX + (x + (int)origine.x) + 100;
        Debug.Log("Index " + index);

        if (CheckAdjacentPoints(x, y))
        {
            Debug.Log("all");
            //int index = (y + (int)origine.y) * gridSizeX + (x + (int)origine.x);
            if (index >= 0 && index < gridTiles.Count)
            {
                GameObject gridTile = gridTiles[index];
                if (gridTile != null)
                {
                    SpriteRenderer tileRenderer = gridTile.GetComponent<SpriteRenderer>();
                    if (tileRenderer != null)
                    {
                        tileRenderer.sprite = blueTile;
                    }
                }
            }
        }
    }

    // Check if adjacents points are all blue
    private bool CheckAdjacentPoints(int x, int y)
    {
        return IsBluePoint(x - 1, y) && IsBluePoint(x + 1, y) 
        && IsBluePoint(x, y) && IsBluePoint(x-1, y - 1);
    }

    // Vérifiez si un point est bleu
    private bool IsBluePoint(int x, int y)
    {
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            GameObject point = gridPoints[x, y];
            if (point != null)
            {
                Renderer pointRenderer = point.GetComponent<Renderer>();
                if (pointRenderer != null && pointRenderer.material.color == Color.blue)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}