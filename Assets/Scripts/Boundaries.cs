using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Establish the screen boundaries for the player object
public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;

    
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = GetComponent<BoxCollider>().bounds.size.x / 2;
    }


    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        transform.position = viewPos;
    }
}
