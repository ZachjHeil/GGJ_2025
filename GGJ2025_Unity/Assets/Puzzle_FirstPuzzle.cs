using UnityEngine;

public class Puzzle_FirstPuzzle : MonoBehaviour
{
    InputManager input;

    private void Start()
    {
        input = InputManager.Instance;
    }

    private void Update()
    {
        if(input.PlayerCancel() || input.PlayerDashed())
        {
            this.gameObject.SetActive(false);
            input.isInPuzzle = false;
        }
    }
}
