using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//This Will Auto Add Character Controller To Gameobject If It's Not Already Applied:
[RequireComponent(typeof(CharacterController))]

public class SlenderPlayerController : MonoBehaviour
{

    //Camera:

    public Camera playerCam;
    public string death;


    //Movement Settings:

    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpPower = 3f;
    public float gravity = 10f;


    //Camera Settings:

    public float lookSpeed = 2f;
    public float lookXLimit = 75f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    //Camera Zoom Settings

    public int ZoomFOV = 35;
    public int initialFOV;
    public float cameraZoomSmooth = 1;

    private bool isZoomed = false;


    //Can The Player Move?:

    private bool canMove = true;

    CharacterController characterController;

    //Sound Effects:

    public AudioSource cameraZoomSound;

    void Start()
    {
        //Ensure We Are Using The Character Controller Component:

        characterController = GetComponent<CharacterController>();


        //Lock And Hide Cursor:

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    void Update()
    {

        //Walking/Running In Action:

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        //Jumping In Action:

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);


        //Camera Movement In Action:

        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        //Zooming In Action:

        if(Input.GetButtonDown("Fire2"))
        {
            isZoomed = true;
            cameraZoomSound.Play();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            isZoomed = false;
            cameraZoomSound.Play();
        }

        if (isZoomed)
        {
            playerCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCam.fieldOfView, ZoomFOV, Time.deltaTime * cameraZoomSmooth);
        }

        else if (!isZoomed)
        {
            playerCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCam.fieldOfView, initialFOV, Time.deltaTime * cameraZoomSmooth);
        }


    }

     void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Slender"))
        {
            SceneManager.LoadScene(death);
        }

    }
}
