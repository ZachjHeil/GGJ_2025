using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float dashSpeed = 5.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public bool underWater;
    public float waterHeight = 60f;
    [SerializeField] private float dashTimer = 0f;
    

    private InputManager inputManager;
    private Transform cameraTransform;
    [SerializeField]private Transform rayGroundSender;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        inputManager = InputManager.Instance;
        waterHeight = inputManager.GetWaterHeight();
    }

    private void OnEnable()
    {
        //StartCoroutine(OneSecondCR());
    }

    private void OnDisable()
    {
        //StopCoroutine(OneSecondCR());
    }

    //IEnumerator OneSecondCR()
    //{
    //    while (true) { 

    //        dashTimer = Mathf.Clamp(dashTimer--, 0, 5);

    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    void Update()
    {
        dashTimer = Mathf.Clamp(dashTimer - Time.deltaTime, 0, 5);

        //Check the player's current y level, for underwater
        underWater = this.transform.position.y >= waterHeight ? false:true;


        if (underWater)
        {
            gravityValue = -3f;
            DetermineVelocity();
            PlayerMove();
            PlayerSwim();
            PlayerSwimDash();
        }
        else
        {
            gravityValue = -9.81f;
            DetermineVelocity();
            PlayerMove();
            PlayerJump();

        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    private void PlayerMove()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = (cameraTransform.forward * move.z) + (cameraTransform.right * move.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void DetermineVelocity()
    {
        groundedPlayer = DetectGround();
        if (groundedPlayer && controller.isGrounded)
        {
            playerVelocity.y = 0f;
        }
    }

    private void PlayerJump()
    {
        // Makes the player jump
        if (inputManager.PlayerJumped() && DetectGround())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
    }

    private void PlayerSwim()
    {
        // Makes the player jump
        if (inputManager.PlayerJumped())
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        } 
        else if (inputManager.PlayerJumpHeld()) // can only trigger after the initial press of the jump button
        {
            playerVelocity.y += Mathf.Sqrt((jumpHeight/5) * -2.0f * gravityValue);
            
        }
        playerVelocity.y = Mathf.Clamp(playerVelocity.y, -10f, 3f);
    }

    private void PlayerSwimDash()
    {
        if (inputManager.PlayerDashed() && !controller.isGrounded && dashTimer == 0)
        {
           StartCoroutine(DashMovement());
           dashTimer = 5f;
        }
    }

    private IEnumerator DashMovement()
    {
        float startingTime = Time.time;
        Vector2 direction = inputManager.GetPlayerMovement();

        while (Time.time < startingTime + 1f)
        {
            Vector3 move = new Vector3(direction.x, 0f, direction.y);
            move = (cameraTransform.forward * move.z) + (cameraTransform.right * move.x);
            move.y = 0f;
            controller.Move(move * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    bool DetectGround()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rayGroundSender.position, rayGroundSender.TransformDirection(Vector3.down), out hit, .75f))

        {
            Debug.DrawRay(rayGroundSender.position, rayGroundSender.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(rayGroundSender.position, rayGroundSender.TransformDirection(Vector3.down) * hit.distance, Color.white);
            return false;
        }
    }
}