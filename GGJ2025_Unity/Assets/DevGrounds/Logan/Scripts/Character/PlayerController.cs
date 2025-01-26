using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    public float baseMovespeed = 5.0f;
    public float sprintMovespeed = 10f;
    public float dashSpeed = 5.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public bool underWater;
    public float waterHeight = 60f;
    [SerializeField] private float dashTimer = 0f;
    [SerializeField] private VisualEffect waterBreathVFX;
    

    private InputManager inputManager;
    private Transform cameraTransform;
    [SerializeField] private Transform rayGroundSender;

    private Camera cam;
    [SerializeField] private LayerMask mask;

    public GameObject interactUI;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        inputManager = InputManager.Instance;
        waterHeight = inputManager.GetWaterHeight();
    }

    private void OnEnable()
    {
        cam = Camera.main;
        //StartCoroutine(OneSecondCR());
    }

    private void OnDisable()
    {
        //StopCoroutine(OneSecondCR());
    }

    public void ChangeWaterEffectState(bool onOff)
    {
        Debug.Log("Setting water breath to " + onOff);
        waterBreathVFX.SendEvent(onOff ? "Start" : "Stop");
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

        DetectInteractable();

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 6f, mask))
        {
            if (hitInfo.collider.CompareTag("Interactable"))
            {
                interactUI.SetActive(true);
                if (inputManager.PlayerInteract())
                {
                    hitInfo.collider.GetComponent<MapPickup>().TriggerItemInteract();
                }
            }
            else
            {
                interactUI.SetActive(false);
            }
        }
    }

    private void PlayerMove()
    {
        float moveSpeed = 0f;
        if (underWater) 
        {
            moveSpeed = baseMovespeed; 
        }
        else
        {
            moveSpeed = inputManager.PlayerSprint() ? sprintMovespeed : baseMovespeed;
        }
        

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = (cameraTransform.forward * move.z) + (cameraTransform.right * move.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * moveSpeed);
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
            //Debug.DrawRay(rayGroundSender.position, rayGroundSender.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            //Debug.DrawRay(rayGroundSender.position, rayGroundSender.TransformDirection(Vector3.down) * hit.distance, Color.white);
            return false;
        }
    }
}