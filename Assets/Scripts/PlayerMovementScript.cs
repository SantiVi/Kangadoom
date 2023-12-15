using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float jumpHeight = 3f;
    public float momentumDamping = 5f;
    private CharacterController movementController;
    public Animator camAnimator;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;

    public float gravity = -25f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float yVelocity;
    private bool isGrounded;
    private bool doubleJump;
   
    void Start()
    {
        movementController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        GetInput();
        MovePlayer();
        
        camAnimator.SetBool("isWalking", isWalking);
    }

    void GetInput() 
    {   
        if(isGrounded && movementVector.y < 0)
        {
            movementVector.y = -2f;
        }

        //if holding down w,a,s,d, then give us -1,0,1 
        //TODO: change with ground check and move speed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);
            if (isGrounded)
            {
                isWalking = true;
            }
        }
        else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);

            isWalking = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            doubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded && doubleJump)
        {
            Jump();
            doubleJump = false;
        }

        //Movement
        if (isGrounded)
        {
            movementVector = new Vector3(inputVector.x * playerSpeed, movementVector.y, inputVector.z * playerSpeed);
        }
        else
        {
            movementVector = new Vector3(inputVector.x * playerSpeed * 0.7f, movementVector.y, inputVector.z * playerSpeed * 0.7f);
        }
        
        movementVector.y += gravity * Time.deltaTime;
        

        
    } 
    void MovePlayer()
    {
        movementController.Move(movementVector * Time.deltaTime);

    }

    void Jump()
    {
        //Jumping method; animate camera when jumping up and down
        movementVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
