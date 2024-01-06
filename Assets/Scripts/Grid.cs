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

    public RuntimeAnimatorController blueAController;
    public RuntimeAnimatorController redAController;

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

        }

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
            }
        }
    }

    private bool IsValide(int x, int y, Sprite actual){
        int i1 = x + y * (gridSizeX + 1);
        int i2 = (x + 1) + y * (gridSizeX + 1);
        int i3 = (x + 1) + (y + 1) * (gridSizeX + 1);
        int i4 = x + (y + 1) * (gridSizeX + 1);


        return canFormTile(i1, actual) && canFormTile(i2, actual) 
        && canFormTile(i3, actual) && canFormTile(i4, actual);

    }

    public bool canFormTile(int pointNumber, Sprite actual)
    {
        // check if the point is has the good color
        if (pointNumber < 0 || pointNumber > (gridSizeX + 1) * (gridSizeY + 1)){
            return false;
        }

        // check if the point is for the actual player
        if (gridPoints[pointNumber].GetComponent<SpriteRenderer>().sprite != actual){
            return false;
        }

        return true;
    }

}