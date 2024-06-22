using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField] float movementSpeed;
    Rigidbody2D rgbd;
    Vector2 movementVector;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        rgbd = GetComponent<Rigidbody2D>();
        movementVector = new Vector2();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        movementVector = playerInput.actions["Move"].ReadValue<Vector2>();
        animator.SetFloat("horizontalValue", movementVector.x);
        animator.SetFloat("verticalValue", movementVector.y);

        rgbd.position = (rgbd.position + movementVector * movementSpeed * Time.fixedDeltaTime);
    }
}
