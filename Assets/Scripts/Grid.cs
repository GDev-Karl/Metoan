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
    
    public Sprite gridBoxBlue;
    public Sprite gridBoxRed;
    public Sprite gridBoxEmpty;
    
    public Sprite pointBlue;
    public Sprite pointRed;
    public Sprite pointEmpty;

    private Sprite actualPoint;
    private Sprite actualBox;

    //private GameObject[,] gridPoints;
    private List<GameObject> gridPoints = new List<GameObject>();
    private List<GameObject> gridTiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        actualPoint = pointBlue;
        actualBox = gridBoxBlue;
        origine = new Vector2(-8, -3);
        CreateGrid();
    }

    public void CreateGrid()
    {
        // draw points
        for (int i = 0; i < (gridSizeX + 1) * (gridSizeY + 1); i++)
        {
            Vector2 pointPosition = new Vector2();
    
            pointPosition.x = (i % (gridSizeX + 1)) + origine.x - 0.5f;
            pointPosition.y = (i / (gridSizeX + 1)) + origine.y - 0.5f;

            GameObject gridPoint = Instantiate(pointPrefab, pointPosition, Quaternion.identity);
            gridPoint.transform.SetParent(transform);
            gridPoint.transform.localScale = new Vector3(2, 2, 1);

            gridPoints.Add(gridPoint);
            // handler touch options
            //PointTouch touchHandler = gridPoint.AddComponent<PointTouch>();
            //touchHandler.grid = this;

            //touchHandler.pointX = (int)pointPosition.x;
            //touchHandler.pointY = (int)pointPosition.y;

            //touchHandler.pointX = i % gridSizeX;
            //touchHandler.pointY = i / gridSizeX;

        }
        //gridPoints = new GameObject[gridSizeX, gridSizeY];

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
        /*if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
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
        }*/
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // check if the point is touched
            Collider2D hitCollider = Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y));

            if (hitCollider != null)
            {
                // change the color of pointPrefab
                SpriteRenderer renderer = hitCollider.gameObject.GetComponent<SpriteRenderer>();
                if (renderer != null && renderer.sprite == pointEmpty){
                    renderer.sprite = actualPoint;

                    int i = gridPoints.IndexOf(hitCollider.gameObject);

                    int iligne = i / (gridSizeX + 1);
                    int icolone = i % (gridSizeX + 1);

                    bool continuePlay = false;

                    if (IsValide(icolone, iligne, actualPoint)){
                        gridTiles[icolone + iligne * gridSizeX].GetComponent<SpriteRenderer>().sprite = actualBox;
                        continuePlay = true;
                    }

                    if (IsValide(icolone - 1, iligne - 1, actualPoint)){
                        gridTiles[icolone - 1 + (iligne - 1) * gridSizeX].GetComponent<SpriteRenderer>().sprite = actualBox;
                        continuePlay = true;
                    }

                    if (IsValide(icolone - 1, iligne, actualPoint)){
                        gridTiles[icolone - 1 + iligne * gridSizeX].GetComponent<SpriteRenderer>().sprite = actualBox;
                        continuePlay = true;
                    }

                    if (IsValide(icolone, iligne - 1, actualPoint)){
                        gridTiles[icolone + (iligne - 1) * gridSizeX].GetComponent<SpriteRenderer>().sprite = actualBox;
                        continuePlay = true;
                    }
                    
                    if (!continuePlay){
                        actualPoint = (actualPoint == pointBlue) ? pointRed : pointBlue;
                        actualBox = (actualBox == gridBoxBlue) ? gridBoxRed : gridBoxBlue;
                    }
                }
                /*Renderer rend = GetComponent<Renderer>();
                if (rend != null)
                {
                    if (colored)
                    {
                        rend.material.color = originalColor;
                        colored = false;
                    }
                    else
                    {
                        rend.material.color = Color.blue;
                        colored = true;
                    }

                    // update the sprite of the tile at the position (PointX, PointY)
                    grid.UpdateTileSprite(pointX, pointY);
                }*/
            }
        }
    }

    private bool IsValide(int x, int y, Sprite actual){
        int i1 = x + y * (gridSizeX + 1);
        int i2 = (x + 1) + y * (gridSizeX + 1);
        int i3 = (x + 1) + (y + 1) * (gridSizeX + 1);
        int i4 = x + (y + 1) * (gridSizeX + 1);

        if (i1 < 0 || i1 > (gridSizeX + 1) * (gridSizeY + 1)){
            return false;
        }

        if (i2 < 0 || i2 > (gridSizeX + 1) * (gridSizeY + 1)){
            return false;
        }

        if (i3 < 0 || i3 > (gridSizeX + 1) * (gridSizeY + 1)){
            return false;
        }

        if (i4 < 0 || i4 > (gridSizeX + 1) * (gridSizeY + 1)){
            return false;
        }

        if (gridPoints[i1].GetComponent<SpriteRenderer>().sprite != actual){
            return false;
        }

        if (gridPoints[i2].GetComponent<SpriteRenderer>().sprite != actual){
            return false;
        }

        if (gridPoints[i3].GetComponent<SpriteRenderer>().sprite != actual){
            return false;
        }

        if (gridPoints[i4].GetComponent<SpriteRenderer>().sprite != actual){
            return false;
        }
        return true;
    }
}