using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float playerSpeed = 20f;
    private CharacterController movementController;
    public Animator camAnimator;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float gravity = -10f;
   
    void Start()
    {
        movementController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        CheckForHeadBob();

        camAnimator.SetBool("isWalking", isWalking);
    }

    void GetInput() 
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);

        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    } 
    void MovePlayer()
    {
        movementController.Move(movementVector * Time.deltaTime);
    }

    void CheckForHeadBob()
    {
        if(movementController.velocity.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
