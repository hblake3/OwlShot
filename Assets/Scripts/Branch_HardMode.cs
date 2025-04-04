using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Branch_HardMode : MonoBehaviour
{
    [SerializeField] float baseSpeed = 10f; // Faster speed for hard mode
    [SerializeField] BranchManager_HardMode branchManager;
    [SerializeField] Star_HardMode star;
    [SerializeField] PlayerController_HardMode playerController_Hard;

    private float branchLeftEdgeX;
    private float branchRightEdgeX;
    private bool hasPassedPlayer = false;

    void Start()
    {
        transform.position = GetNewPosition();
        gameObject.tag = "Branch";
        branchManager.CheckBranchPositions();
        star.SetPosition();
        hasPassedPlayer = false;
    }

    void Update()
    {
        MoveBranch();
        CheckIfBranchPassedPlayer();

        if (transform.position.y < -6)
        {
            transform.position = GetNewPosition();
            branchManager.CheckBranchPositions();
            star.SetPosition();
            hasPassedPlayer = false;
        }
    }

    void MoveBranch()
    {
        // transform.Translate(baseSpeed * Time.deltaTime * Vector2.down); // Faster fall speed
        transform.Translate(Vector2.down * baseSpeed * Time.deltaTime);

    }

    Vector2 GetNewPosition()
    {
        float newX = Random.Range(Mathf.Sign(transform.position.x) * 5, Mathf.Sign(transform.position.x) * 12);
        if (newX > -9)
        {
            if (transform.position.x > 0)
            {
                newX += 5;
            }
        }
        float newY = Mathf.Clamp(Random.Range(9f, 13f), 9f, 13f);

        return new Vector2(newX, newY);
    }

    public List<float> GetBranchEdgesX()
    {
        branchRightEdgeX = transform.position.x + (GetComponent<Renderer>().bounds.size.x / 2);
        branchLeftEdgeX = transform.position.x - (GetComponent<Renderer>().bounds.size.x / 2);

        return new List<float> { branchLeftEdgeX, branchRightEdgeX };
    }

    void CheckIfBranchPassedPlayer()
    {
        if (playerController_Hard == null)
        {
            return;
        }

        float owlPositionX = playerController_Hard.transform.position.x;
        List<float> branchEdgesX = GetBranchEdgesX();
        float branchLeftEdgeX = branchEdgesX[0];
        float branchRightEdgeX = branchEdgesX[1];

        if (hasPassedPlayer || playerController_Hard.hasCollidedWithBranch)
        {
            return;
        }

        if ((branchRightEdgeX < owlPositionX || branchLeftEdgeX > owlPositionX) && transform.position.y < playerController_Hard.transform.position.y)
        {
            playerController_Hard.AddScore(2);
            hasPassedPlayer = true;
        }
    }
}