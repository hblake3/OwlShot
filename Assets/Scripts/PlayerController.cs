using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerView view;
    private PlayerModel model;

    public int lives = 3;
    public GameObject[] hearts;

    [SerializeField] GameStateController gameStateController;
    [SerializeField] Text ScoreText; //refer to ScoreText UI

    private int score = 0;
     public bool hasCollidedWithBranch = false;  //flag collision with branch
    public bool hasPassedBranch = false; //track if branch passes without collision

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PlayerView>();
        model = new PlayerModel();
        

    }

    // Update is called once per frame
    void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(moveInput, 0, 0);
        view.Move(moveDirection, model.speed);
    }

    //detect collision w/ branches

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Branch")) //branches have "Branch" tag
        {
            Debug.Log("Branch hit! Losing a life.");
            hasCollidedWithBranch = true;

            LostLife();
            UpdateHearts();


        }else if(other.CompareTag("Star"))
        {
            Debug.Log("Star detected, attempting to collect.");
            Debug.Log("Star collected!");
            AddScore(10);
            //disable the star instead of destroying it
            other.gameObject.SetActive(false);
            UpdateScoreText();
            
        }
    }

    void LostLife()
    {
        if(lives > 0)
        {
            lives--;
            UpdateHearts();
        }
        if(lives <= 0)
        {
            Debug.Log("Game Over!");
            //gameStateController.SetPausedState(true); //pause the gameplay
            gameStateController.DisplayGameOver();
            // Open UI to prompt for replay
        }
        //reset collision state after handling the penalty
        hasCollidedWithBranch = false;
    }
    void UpdateHearts()
    {
        for(int i=0; i < hearts.Length; i++ )
        {
            hearts[i].SetActive(i < lives); //set hearts based on lives left
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score added: " + points + ". Total score: " + score); // Log when score is added

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        Debug.Log("Updating score text: " + score); //ensure this method is called

        ScoreText.text = score.ToString() + " PTS";
    }

    //reset branch state when the branch goes off-screen or when necessary
    void ResetBranchState()
    {
        hasPassedBranch = false; //reset the passed flag when a branch is no longer in play
        hasCollidedWithBranch = false; //reset collision only when a new branch is spawned

    }
    
}
