using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to adjust behaviors of branches together, such as not overlapping.
public class BranchManager : MonoBehaviour
{
    [SerializeField] Branch branchLeft;
    [SerializeField] Branch branchRight;



    public void CheckBranchPositions(){
        // Calculate the absolute distance between branches
        float branchDistance = Mathf.Abs(branchLeft.transform.position.y - branchRight.transform.position.y);
        
        // The branches are overlapping too closely and player cannot get through
        if(branchDistance <= 2){
            // Determine which branch to move based on their current positions
            if(branchLeft.transform.position.y < branchRight.transform.position.y){
                // Left branch is lower, so move it up further
                branchLeft.transform.position = new Vector2(branchLeft.transform.position.x, branchLeft.transform.position.y + 6);
            }
            else{
                // Right branch is lower, so move it up further
                branchRight.transform.position = new Vector2(branchRight.transform.position.x, branchRight.transform.position.y + 6);
            }
        }
    }
}
