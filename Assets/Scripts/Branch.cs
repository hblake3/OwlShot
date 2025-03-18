using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {

    [SerializeField] float baseSpeed = 5; // Speed at which the branch moves, will be increased as game progresses
    [SerializeField] BranchManager branchManager;
    [SerializeField] Star star; // The star belonging to this branch
    private float branchLeftEdgeX;
    private float branchRightEdgeX;
    private Renderer branchRenderer;



    // Start is called before the first frame update
    void Start()
    {
        // Initiate randomization for branch heights, positions, star positions on start
        transform.position = GetNewPosition();
        gameObject.tag = "Branch"; //branches have the correct "Branch" tag
        branchManager.CheckBranchPositions();
        star.SetPosition();
    }


    void Update()
    {
        MoveBranch();

        // When the branch is off-screen below the player, move it back to the top.
        if(transform.position.y < -6){
            transform.position = GetNewPosition();
            branchManager.CheckBranchPositions();
            star.SetPosition();
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

}
