using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    [SerializeField] private Transform WaterSurface;
    private InputSystem_Actions playerControls;

    public bool isLefty = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMouseMovement()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerJumpHeld() 
    {
        return playerControls.Player.Jump.IsPressed(); 
    }

    public bool PlayerDashed()
    {
        return playerControls.Player.Sprint.triggered;
    }

    public bool PlayerSprint()
    {
        return playerControls.Player.Sprint.IsPressed();
    }

    public float GetWaterHeight()
    {
        return WaterSurface.transform.position.y;
    }
}
