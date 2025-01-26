//Teegan Tulk
//2025-01-24
//Global Game Jam 2025
using UnityEngine;

public class MapPickup : MonoBehaviour
{
    public Enums.InventoryItems inventoryItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void TriggerItemInteract()
    {
        Inventory.Instance.ObtainItem(inventoryItem);
        gameObject.SetActive(false);
    }


}
