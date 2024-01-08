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
    private int currentPlayer;

    private AudioSource gameAudio;

    public List<Sprite> redSprites;
    public List<Sprite> blueSprites;

    private GameObject blue, red;

    public Text timerText;
    private float timer;

    public AudioClip pointClip;
    private AudioSource audioPoint;

    private int emptyPoints;

    public GameObject screenEnd, playerIcon;
    public Text scoreBlueTect, scoreRedText;
    public List<Sprite> winners;

    // Start is called before the first frame update
    void Start()
    {
        screenEnd.SetActive(false);

        audioPoint = GetComponents<AudioSource>()[1];
        gameAudio = GetComponent<AudioSource>();
        gameAudio.Play();

        Debug.Log("Player index " + playersIndex.Count);

        for (int i = 0; i < playersIndex.Count; i++)
        {
            int spriteIndex = playersIndex[i];

            Debug.Log("sprite index " + spriteIndex);
            Player player =  new Player();
            player.score = 0;
            player.box = boxes[spriteIndex];
            player.point = points[spriteIndex];

            players.Add(player);
        }

        blue = GameObject.Find("Player_1");
        red = GameObject.Find("Player_2");
        currentPlayer = 0;
        timer = 11.0f;

        indicatePlayer();
        emptyPoints = (grid.GetColumns() + 1) * (grid.GetLines() + 1);
    }

    private void PrintScore(){
        //scoreUI[currentPlayer].text = "Score Player" + playersIndex[currentPlayer] + " : " + players[currentPlayer].score
        scoreUI[currentPlayer].text = "" + players[currentPlayer].score;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(emptyPoints == 0){
            Debug.Log("Game Over");
            GameOver();
        }

        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        if(timer <= 0){
            currentPlayer = (currentPlayer + 1) % players.Count;
            timer = 11.0f;

            indicatePlayer();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player pressed " + (currentPlayer + 1));
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // check if the point is touched
            Collider2D hitCollider = Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y));

            if (hitCollider == null)
                Debug.Log("hitCollider est null");

            if (hitCollider != null)
            {
                // change the color of pointPrefab

                SpriteRenderer renderer = hitCollider.gameObject.GetComponent<SpriteRenderer>();
                if (renderer != null && renderer.sprite == points[0]){
                    renderer.sprite = players[currentPlayer].point;

                    audioPoint.Play();
                    emptyPoints -= 1;
                    Debug.Log("Current player " + currentPlayer);

                    int i = grid.PointIndexOf(hitCollider.gameObject);

                    int iligne = i / (grid.GetColumns() + 1);
                    int icolone = i % (grid.GetColumns() + 1);

                    int index = icolone + iligne * grid.GetColumns();
                    int index1 = icolone - 1 + (iligne - 1) * grid.GetColumns();
                    int index2 = icolone + (iligne - 1) * grid.GetColumns();
                    int index3 = icolone - 1 + (iligne) * grid.GetColumns();

                    Sprite box = players[currentPlayer].box;
                    int player_index = playersIndex[currentPlayer];

                    bool continuePlay = false;

                    if (IsValide(icolone, iligne, grid.GetColumns())){
                        grid.SetBox(index, box, player_index);
                        continuePlay = true;
                        players[currentPlayer].score += 1;
                        PrintScore();
                    }

                    if (IsValide(icolone - 1, iligne - 1, grid.GetColumns())){
                        grid.SetBox(index1, box, player_index);
                        continuePlay = true;
                        players[currentPlayer].score += 1;
                        PrintScore();
                    }

                    if (IsValide(icolone - 1, iligne, grid.GetColumns())){
                        grid.SetBox(index3, box, player_index);
                        continuePlay = true;
                        players[currentPlayer].score += 1;
                        PrintScore();
                    }

                    if (IsValide(icolone, iligne - 1, grid.GetColumns())){
                        grid.SetBox(index2, box, player_index);
                        continuePlay = true;
                        players[currentPlayer].score += 1;
                        PrintScore();
                    }
                    
                    if (!continuePlay){
                        currentPlayer = (currentPlayer + 1) % players.Count;
                        timer = 11.0f;

                        indicatePlayer();
                    }
                }
            }
        }
    }

    /*
    public bool isGameOver(Grid grid)
    {
        for (int i = 0; i < numberLines * numberColumns; i++)
        {

        }
    }*/

    private bool IsValide(int column, int line, int columns){
        int i1 = column + line * (columns + 1);
        int i2 = (column + 1) + line * (columns + 1);
        int i3 = (column + 1) + (line + 1) * (columns + 1);
        int i4 = column + (line + 1) * (columns + 1);


        return canFormTile(grid.GetPoint(i1)) && canFormTile(grid.GetPoint(i2)) 
        && canFormTile(grid.GetPoint(i4)) && canFormTile(grid.GetPoint(i3));

    }

    public void indicatePlayer()
    {
        if (currentPlayer == 0)
        {

            blue.GetComponent<SpriteRenderer>().sprite = blueSprites[0];
            red.GetComponent<SpriteRenderer>().sprite = redSprites[1];
        }
        else if (currentPlayer == 1)
        {
            blue.GetComponent<SpriteRenderer>().sprite = blueSprites[1];
            red.GetComponent<SpriteRenderer>().sprite = redSprites[0];
        }
    }

    public bool canFormTile(Sprite point)
    {
        return point == players[currentPlayer].point;
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
        return players[0].score;
    }

    public int getPlayerScore2()
    {
        return players[1].score;
    }

    public void GameOver()
    {
        int winner;

        screenEnd.SetActive(true);
        
        if (getPlayerScore1() > getPlayerScore2()){
            winner = 0;
        } 
        else if (getPlayerScore1() < getPlayerScore2()){
            winner = 1;
        }
        else{
            winner = 2;
        }
        playerIcon.GetComponent<Image>().sprite = winners[winner];
    
        Debug.Log("score 1 : " + getPlayerScore1());
        Debug.Log("score 2 : " + getPlayerScore2());

        scoreBlueTect.text = "" + getPlayerScore1();
        scoreRedText.text = "" + getPlayerScore2();
    }

}
