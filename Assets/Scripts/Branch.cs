using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {

    [SerializeField] float speed;


    // Start is called before the first frame update
    void Start()
    {
        // Start the branches at random heights
        transform.position = GetNewPosition();
        gameObject.tag = "Branch"; //branches have the correct "Branch" tag
    }


    void Update()
    {
        MoveBranch();

        // When the branch is off-screen below the player, move it back to the top.
        if(transform.position.y < -6){
            transform.position = GetNewPosition();
        }
    }

    // Move the branch downwards at a constant rate
    void MoveBranch(){
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    // Returns a random Vector2 for the branch's new transform position
    Vector2 GetNewPosition(){
        float newX = Random.Range(Mathf.Sign(transform.position.x) * 10, Mathf.Sign(transform.position.x) * 15);
        float newY = Mathf.Clamp(Random.Range(9f, 13f), 9f, 13f); // Explicitly clamping

        return new Vector2(newX, newY);
    }
}
