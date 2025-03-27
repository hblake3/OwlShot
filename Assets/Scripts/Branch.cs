using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {

    [SerializeField] float baseSpeed = 5; // Speed at which the branch moves, will be increased as game progresses
    [SerializeField] BranchManager branchManager;
    [SerializeField] Star star; // The star belonging to this branch
    [SerializeField] PlayerController playerController;

    private float branchLeftEdgeX;
    private float branchRightEdgeX;
    private Renderer branchRenderer;
    private bool hasPassedPlayer = false; //make sure the score is only added once


    // Start is called before the first frame update
    void Start()
    {
        // Initiate randomization for branch heights, positions, star positions on start
        transform.position = GetNewPosition();
        gameObject.tag = "Branch"; //branches have the correct "Branch" tag
        branchManager.CheckBranchPositions();
        star.SetPosition();

        hasPassedPlayer = false; //reset passed player flag

    

    }


    void Update()
    {
        MoveBranch();
        CheckIfBranchPassedPlayer(); // Add this line to check for passing

        // When the branch is off-screen below the player, move it back to the top.
        if(transform.position.y < -6){
            transform.position = GetNewPosition();
            branchManager.CheckBranchPositions();
            star.SetPosition();

            hasPassedPlayer = false; //reset only when the branch resets

        }
        
    }

    // Move the branch downwards at a constant rate
    void MoveBranch(){
        transform.Translate(baseSpeed * Time.deltaTime * Vector2.down);
    }

    // Returns a random Vector2 for the branch's new transform position
    Vector2 GetNewPosition(){

        float newX = Random.Range(Mathf.Sign(transform.position.x) * 5, Mathf.Sign(transform.position.x) * 12);
        if(newX > -9){
            if(transform.position.x > 0){
                newX += 5;
            }
        }
        float newY = Mathf.Clamp(Random.Range(9f, 13f), 9f, 13f); // Explicitly clamping

        return new Vector2(newX, newY);
    }

    // Returns a list of the branch's [ left edge x-pos, right edge x-pos ]
    public List<float> GetBranchEdgesX() {
        branchRightEdgeX = transform.position.x + (GetComponent<Renderer>().bounds.size.x / 2);
        branchLeftEdgeX = transform.position.x - (GetComponent<Renderer>().bounds.size.x / 2);

        return new List<float> { branchLeftEdgeX, branchRightEdgeX };
    }

    //this method is to add points when branches pass the owl
    void CheckIfBranchPassedPlayer()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController is not assigned!");
            return;
        }

        //get the owl’s X position
        float owlPositionX = playerController.transform.position.x;

        //get the branch's left and right edge positions
        List<float> branchEdgesX = GetBranchEdgesX();
        float branchLeftEdgeX = branchEdgesX[0];
        float branchRightEdgeX = branchEdgesX[1];

        Debug.Log($"Branch Left X: {branchLeftEdgeX}, Branch Right X: {branchRightEdgeX}, Player X: {owlPositionX}");

        //prevent scoring if the owl has already collided with a branch
        if (hasPassedPlayer || playerController.hasCollidedWithBranch)
        {
            Debug.Log("Branch passed, but no points added due to collision.");
            return;
        }

        //make sure the branch passes completely before adding points
        if ((branchRightEdgeX < owlPositionX || branchLeftEdgeX > owlPositionX) && transform.position.y < playerController.transform.position.y)
        {
            playerController.AddScore(2);
            hasPassedPlayer = true; //prevent multiple score additions for the same branch
            Debug.Log("Branch passed player, score added.");
        }
    }
}
