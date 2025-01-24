using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    [SerializeField] private Transform WaterSurface;
    private PlayerControls playerControls;

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
        playerControls = new PlayerControls();
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
        return playerControls.Player.Movement.ReadValue<Vector2>();
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
        return playerControls.Player.Dash.triggered;
    }

    public bool PlayerSprint()
    {
        return playerControls.Player.Dash.IsPressed();
    }

    public float GetWaterHeight()
    {
        return WaterSurface.transform.position.y;
    }
}
