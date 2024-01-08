using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int numberColumns, numberLines;
    public Vector2 origine;

    public GameObject tilePrefab;
    public GameObject pointPrefab;

    public AudioClip boxClip;
    public AudioClip pointClip;
    private AudioSource audio;
/*
    
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
    private Sprite actualBox;*/

    //private GameObject[,] gridPoints;
    private List<GameObject> gridPoints = new List<GameObject>();
    private List<GameObject> gridTiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // actualPoint = pointBlue;
        // actualBox = gridBoxBlue;
        //origine = new Vector2(-8, -3);
        CreateGrid();
        audio = GetComponent<AudioSource>();
    }

    public void CreateGrid()
    {
        // draw points
        for (int i = 0; i < (numberColumns + 1) * (numberLines + 1); i++)
        {
            Vector2 pointPosition = new Vector2();
    
            pointPosition.x = (i % (numberColumns + 1)) + origine.x - 0.5f;
            pointPosition.y = (i / (numberColumns + 1)) + origine.y - 0.5f;

            GameObject gridPoint = Instantiate(pointPrefab, pointPosition, Quaternion.identity);
            gridPoint.transform.SetParent(transform);
            gridPoint.transform.localScale = new Vector3(2, 2, 1);

            gridPoints.Add(gridPoint);

        }

        // draw tiles
        for (int i = 0; i < numberColumns * numberLines; i++)
        {
            Vector2 tilePosition = new Vector2();
    
            tilePosition.x = (i % numberColumns) + origine.x;
            tilePosition.y = (i / numberColumns) + origine.y;

            GameObject gridTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
            gridTile.transform.SetParent(transform);
            gridTile.transform.localScale = new Vector3(2, 2, 1);

            gridTiles.Add(gridTile);
        }

    }

    public Sprite GetPoint(int index){
        return (index >= 0 && index < gridPoints.Count) ? gridPoints[index].GetComponent<SpriteRenderer>().sprite : null;
    }

    public void SetPoint(int index, Sprite sprite){
        if (index >= 0 && index < gridPoints.Count) {
            gridPoints[index].GetComponent<SpriteRenderer>().sprite = sprite;

            audio.clip = pointClip;
            audio.Play();
        }
    }

    public Sprite GetBox(int index){
        return (index >= 0 && index < gridTiles.Count) ? gridTiles[index].GetComponent<SpriteRenderer>().sprite : null;
    }

    public void SetBox(int index, Sprite box, int player){
        if (index >= 0 && index < gridTiles.Count) {
            //gridTiles[index].GetComponent<SpriteRenderer>().sprite = box;
            Animator animator = gridTiles[index].GetComponent<Animator>();
            Debug.Log("Player : " + player);
            animator.SetInteger("player", player);

            audio.clip = boxClip;
            audio.Play();
        }

    }

    public int GetColumns(){
        return numberColumns;
    }

    public int GetLines(){
        return numberLines;
    }

    public int PointIndexOf(GameObject gameObject){
        return gridPoints.IndexOf(gameObject);
    }

    public int BoxIndexOf(GameObject gameObject){
        return gridTiles.IndexOf(gameObject);
    }
/*
    // Update is called once per frame
    void Barby()
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

                    int iligne = i / (numberColumns + 1);
                    int icolone = i % (numberColumns + 1);

                    bool continuePlay = false;

                    if (IsValide(icolone, iligne, actualPoint)){
                        gridTiles[icolone + iligne * numberColumns].GetComponent<SpriteRenderer>().sprite = actualBox;
                        continuePlay = true;
                    }

                    if (IsValide(icolone - 1, iligne - 1, actualPoint)){
                        gridTiles[icolone - 1 + (iligne - 1) * numberColumns].GetComponent<SpriteRenderer>().sprite = actualBox;
                        continuePlay = true;
                    }

                    if (IsValide(icolone - 1, iligne, actualPoint)){
                        gridTiles[icolone - 1 + iligne * numberColumns].GetComponent<SpriteRenderer>().sprite = actualBox;
                        continuePlay = true;
                    }

                    if (IsValide(icolone, iligne - 1, actualPoint)){
                        gridTiles[icolone + (iligne - 1) * numberColumns].GetComponent<SpriteRenderer>().sprite = actualBox;
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
        int i1 = x + y * (numberColumns + 1);
        int i2 = (x + 1) + y * (numberColumns + 1);
        int i3 = (x + 1) + (y + 1) * (numberColumns + 1);
        int i4 = x + (y + 1) * (numberColumns + 1);


        return canFormTile(i1, actual) && canFormTile(i2, actual) 
        && canFormTile(i3, actual) && canFormTile(i4, actual);

    }

    public bool canFormTile(int pointNumber, Sprite actual)
    {
        // check if the point is has the good color
        if (pointNumber < 0 || pointNumber > (numberColumns + 1) * (numberLines + 1)){
            return false;
        }

        // check if the point is for the actual player
        if (gridPoints[pointNumber].GetComponent<SpriteRenderer>().sprite != actual){
            return false;
        }

        return true;
    }
*/
}