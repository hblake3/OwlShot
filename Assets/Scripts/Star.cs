using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] Branch branch; // The branch belonging to a star
    private List<float> edgesList = new List<float>();
    private float leftBoundaryX;
    private float rightBoundaryX;
    private float randX; // holds a randomly calculated x position for the star

    public void Start(){
        leftBoundaryX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        rightBoundaryX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane)).x;
    }

    public void SetPosition(){
        // Get the branch edges in world space
        edgesList = branch.GetBranchEdgesX();
        
        // Get screen bounds again to ensure they're up-to-date
        leftBoundaryX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        rightBoundaryX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane)).x;
        
        if(Random.Range(0f, 1f) < 0.4f){
            float minX, maxX;
            
            if(branch.name.Contains("Branch_L")){
                // For left branch: position star between left screen edge and right branch edge
                minX = leftBoundaryX;
                maxX = edgesList[1]; // Right edge of branch
            }
            else{
                // For right branch: position star between left branch edge and right screen edge
                minX = edgesList[0]; // Left edge of branch
                maxX = rightBoundaryX;
            }
            
            // Check if range is valid (min should be less than max)
            if(minX >= maxX) {
                Debug.LogError("Invalid range: minX (" + minX + ") >= maxX (" + maxX + ")");
                gameObject.SetActive(false);
                return;
            }
            
            // Star needs to de-parent from the branch to set accurate world space position.
            Transform originalParent = transform.parent;
            transform.SetParent(null);
            
            randX = Random.Range(minX, maxX);
            transform.position = new Vector3(randX, transform.position.y, transform.position.z);
                       
            // Re-attach to parent
            transform.SetParent(originalParent);            
            gameObject.SetActive(true);
        }
        else{
            gameObject.SetActive(false);
        }
    }
}
