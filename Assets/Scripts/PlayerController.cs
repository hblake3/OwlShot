using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerView view;
    private PlayerModel model;

    public int lives = 3;
    public GameObject[] hearts;
    
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

            LostLife();
            UpdateHearts();

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
            //we will add game over logic later
        }
    }
    void UpdateHearts()
    {
        for(int i=0; i < hearts.Length; i++ )
        {
            hearts[i].SetActive(i < lives); //set hearts based on lives left
        }
    }
}
