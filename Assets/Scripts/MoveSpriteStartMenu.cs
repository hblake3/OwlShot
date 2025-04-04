using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSpriteStartMenu : MonoBehaviour
{
    public RawImage rawImage;
    public float speed = 100f;
    private RectTransform rectTransform;

    private bool movingRight = true;

    void Start()
    {
        //get the RectTransform of the RawImage
        rectTransform = rawImage.GetComponent<RectTransform>();
    }

    void Update()
    {
        //get the current position of the RawImage
        float currentX = rectTransform.anchoredPosition.x;

        if (movingRight)
        {
            //move the RawImage to the right
            rectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);

            //check if it hits the right edge of the screen
            if (currentX >= Screen.width / 2)
            {
                //change direction to left
                movingRight = false;
            }
        }
        else
        {
            //move the RawImage to the left
            rectTransform.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);

            //check if it hits the left edge of the screen
            if (currentX <= -Screen.width / 2)
            {
                //change direction to right
                movingRight = true;
            }
        }
    }
}
