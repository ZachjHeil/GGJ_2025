using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    [SerializeField] private Transform WaterSurface;
    private InputSystem_Actions playerControls;

    public bool isLefty = false;

    public bool isInPuzzle = false;

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

        isInPuzzle = false;
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
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public bool PlayerShoot()
    {
        return playerControls.Player.Attack.triggered;
    }

    public bool PlayerInteract()
    {
        return playerControls.Player.Interact.IsPressed();
    }

    public float GetWaterHeight()
    {
        return WaterSurface.transform.position.y;
    }

    public bool PlayerCancel()
    {
        return playerControls.UI.Cancel.triggered;
    }
}
