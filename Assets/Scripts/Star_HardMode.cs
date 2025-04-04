using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_HardMode : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] private Branch_HardMode branch;
    private List<float> edgesList = new List<float>();
    private float leftBoundaryX;
    private float rightBoundaryX;

    public void Start()
    {
        // Calculate the screen boundaries once on start
        leftBoundaryX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        rightBoundaryX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane)).x;

        // Set the tag for the star
        gameObject.tag = "Star_HardMode"; 
    }

    public void SetPosition()
    {
        // Get the branch edges in world space
        edgesList = branch.GetBranchEdgesX();

        // Calculate the valid min and max positions based on the branch's position
        float minX = leftBoundaryX;
        float maxX = rightBoundaryX;

        // Adjust the min/max range based on whether the branch is left or right
        if (branch.name.Contains("Branch_L"))
        {
            maxX = edgesList[1]; // Right edge of branch
        }
        else
        {
            minX = edgesList[0]; // Left edge of branch
        }

        // Validate the range to ensure min < max
        if (minX >= maxX)
        {
            Debug.LogError("Invalid range: minX (" + minX + ") >= maxX (" + maxX + ")");
            gameObject.SetActive(false);
            return;
        }

        // Only spawn the star if the random condition is met (40% chance)
        if (Random.Range(0f, 1f) < 0.6f)
        {
            // De-parent the star to set an accurate world space position
            Transform originalParent = transform.parent;
            transform.SetParent(null);

            // Randomly set the X position within the valid range and maintain the Y/Z position
            float randX = Random.Range(minX, maxX);
            transform.position = new Vector3(randX, transform.position.y, transform.position.z);

            // Re-attach to the original parent
            transform.SetParent(originalParent);
            gameObject.SetActive(true);
        }
        else
        {
            // Deactivate the star if the condition is not met
            gameObject.SetActive(false);
        }
    }

    public void RespawnStar()
    {
        // Delay respawn logic here
        StartCoroutine(RespawnAfterDelay(2f));
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Re-enable the star and reset its position
        gameObject.SetActive(true);
        SetPosition(); // Reset the position of the star or implement any other respawn logic
    }
}
