//Teegan Tulk
//2025-01-24
//Global Game Jam 2025
using UnityEngine;

public class MapPickup : MonoBehaviour
{
    public Enums.InventoryItems inventoryItem;
    public GameObject PuzzleCanvas;
    public GameObject ClosedObject;
    public GameObject OpenObject;
    public Collider firstInteractCollider;
    public int curStep;

    private void Awake()
    {
        curStep = 0;
    }

    private void Start()
    {
        bool alreadyDone = Inventory.instance.CheckIfObtained(inventoryItem); // change this to if the map piece is obtained
        if (alreadyDone)
        {
            curStep = 2;
            TriggerItemInteract();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TriggerItemInteract()
    {

        switch (curStep)
        {
            case 0:

                if (PuzzleCanvas != null)
                {
                    PuzzleCanvas.SetActive(true);

                    InputManager.Instance.isInPuzzle = true;
                }

                if (PuzzleCanvas == null)
                {
                    curStep++;
                    TriggerItemInteract();
                }
                break;

            case 1:
                if(ClosedObject != null) ClosedObject.SetActive(false);
                if (firstInteractCollider != null) firstInteractCollider.enabled = false;
                if(OpenObject != null) OpenObject.SetActive(true); 

                if(ClosedObject == null || OpenObject == null)
                {
                    curStep++;
                    TriggerItemInteract();
                }
                break;
            case 2:
                if(!Inventory.Instance.CheckIfObtained(inventoryItem)) Inventory.Instance.ObtainItem(inventoryItem);
                gameObject.SetActive(false);
                break;

        }
    }
}
