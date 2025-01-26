using UnityEngine;

public class Puzzle_FirstPuzzle : MonoBehaviour
{
    InputManager input;
    public MapPickup mapHandler;

    private void Start()
    {
        input = InputManager.Instance;
    }

    private void Update()
    {
        if(input.PlayerCancel() || input.PlayerDashed())
        {
            mapHandler.curStep++;
            mapHandler.TriggerItemInteract();
            input.isInPuzzle = false;
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
