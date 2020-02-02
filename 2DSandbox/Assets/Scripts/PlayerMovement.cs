using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private float speed = 40f;

    private float move_x = 0f;
    private bool jump = false;

    void Start()
    {
        
    }

    void Update()
    {
        // input for horizontal movement
        move_x = Input.GetAxis("Horizontal") * speed;

        // input for jump movement
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(move_x * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
