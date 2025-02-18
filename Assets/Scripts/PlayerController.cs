using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerView view;
    private PlayerModel model;
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
}
