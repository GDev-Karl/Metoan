using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTouch : MonoBehaviour
{
    public Color originalColor;
    public bool colored = false;

    public Grid grid; // reference to the grid
    public int pointX;
    public int pointY;

    // Start is called before the first frame update
    void Start()
    {
        // save the original color of the point
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
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

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                // change the color of pointPrefab
                Renderer rend = GetComponent<Renderer>();
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
                }
            }
        }
    }
}



/*
private bool GetValideIn(int i1, int i2, int i3, int i4){
        List<Sprite> sp = new List<Sprite>();

        if (!IsEmpty(i1)) sp.Add(grid.GetPoint(i1));
        if (!IsEmpty(i2)) sp.Add(grid.GetPoint(i2));
        if (!IsEmpty(i3)) sp.Add(grid.GetPoint(i3));
        if (!IsEmpty(i3)) sp.Add(grid.GetPoint(i4));

        for (int i = 0; i < sp.Count - 1; i++){
            for (int j = i + 1; j < sp.Count; j++){
                if (sp[i] != sp[j]){
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsEmpty(int index){
        return grid.GetPoint(index) == points[0];
    }
    */