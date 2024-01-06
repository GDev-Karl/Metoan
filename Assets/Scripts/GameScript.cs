using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private int player1_score = 0;
    private int player2_score = 0;

    public List<Sprite> boxes;
    public List<Sprite> points;
    public List<int> playersIndex;
    public List<Text> scoreUI;

    public Grid grid;

    private List<Player> players = new List<Player>();

    private int curentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player inex " + playersIndex.Count);

        for (int i = 0; i < playersIndex.Count; i++)
        {
            int spriteIndex = playersIndex[i];

            Debug.Log("sprite idex " + spriteIndex);
            Player player =  new Player();
            player.score = 0;
            player.box = boxes[spriteIndex];
            player.point = points[spriteIndex];

            players.Add(player);
        }

        curentPlayer = 0;
    }

    private void PrintScore(){
        scoreUI[curentPlayer].text = "Score Player" + playersIndex[curentPlayer] + " : " + players[curentPlayer].score;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player pressed " + (curentPlayer + 1));
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // check if the point is touched
            Collider2D hitCollider = Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y));

            if (hitCollider != null)
            {
                // change the color of pointPrefab
                SpriteRenderer renderer = hitCollider.gameObject.GetComponent<SpriteRenderer>();
                if (renderer != null && renderer.sprite == points[0]){
                    renderer.sprite = players[curentPlayer].point;

                    int i = grid.PointIndexOf(hitCollider.gameObject);

                    int iligne = i / (grid.GetColumns() + 1);
                    int icolone = i % (grid.GetColumns() + 1);

                    int index = icolone + iligne * grid.GetColumns();
                    int index1 = icolone - 1 + (iligne - 1) * grid.GetColumns();
                    int index2 = icolone + (iligne - 1) * grid.GetColumns();
                    int index3 = icolone - 1 + (iligne) * grid.GetColumns();

                    Sprite box = players[curentPlayer].box;
                    int player_index = playersIndex[curentPlayer];

                    bool continuePlay = false;

                    if (IsValide(icolone, iligne, grid.GetColumns())){
                        grid.SetBox(index, box, player_index);
                        continuePlay = true;
                        players[curentPlayer].score += 1;
                        PrintScore();
                    }

                    if (IsValide(icolone - 1, iligne - 1, grid.GetColumns())){
                        grid.SetBox(index1, box, player_index);
                        continuePlay = true;
                        players[curentPlayer].score += 1;
                        PrintScore();
                    }

                    if (IsValide(icolone - 1, iligne, grid.GetColumns())){
                        grid.SetBox(index3, box, player_index);
                        continuePlay = true;
                        players[curentPlayer].score += 1;
                        PrintScore();
                    }

                    if (IsValide(icolone, iligne - 1, grid.GetColumns())){
                        grid.SetBox(index2, box, player_index);
                        continuePlay = true;
                        players[curentPlayer].score += 1;
                        PrintScore();
                    }
                    
                    if (!continuePlay){
                        curentPlayer = (curentPlayer + 1) % players.Count;
                    }
                }
            }
        }
    }

    private bool IsValide(int column, int line, int columns){
        int i1 = column + line * (columns + 1);
        int i2 = (column + 1) + line * (columns + 1);
        int i3 = (column + 1) + (line + 1) * (columns + 1);
        int i4 = column + (line + 1) * (columns + 1);


        return canFormTile(grid.GetPoint(i1)) && canFormTile(grid.GetPoint(i2)) 
        && canFormTile(grid.GetPoint(i4)) && canFormTile(grid.GetPoint(i3));

    }

    public bool canFormTile(Sprite point)
    {
        return point == players[curentPlayer].point;
    }

    public void AddScore(int player)
    {
        if (player == 1)
        {
            player1_score += 1;
        }
        else if (player == 2)
        {
            player2_score += 1;
        }
    }

    public void AddScore1()
    {
        player1_score++;
    }

    public void AddScore2()
    {
        player2_score++;
    }

    public int getScore(int player)
    {
        if (player == 1)
            return player1_score;

        return player2_score;
    }

    public int getPlayerScore1()
    {
        return player1_score;
    }

    public int getPlayerScore2()
    {
        return player2_score;
    }


}
