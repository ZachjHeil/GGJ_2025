using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    [SerializeField] private Transform WaterSurface;
    private PlayerControls playerControls;

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

    public float GetWaterHeight()
    {
        return WaterSurface.transform.position.y;
    }
}
