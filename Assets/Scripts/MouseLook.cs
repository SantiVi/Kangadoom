using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    float yRotation = 0f;
    public float minAngle = -85f;
    public float maxAngle = 89f;
    

    public Transform playerBody;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    void GetInput()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    }

    void ModifyInput()
    {
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, minAngle, maxAngle);

    }

    void MovePlayer()
    {
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }

}
