using UnityEngine;

public class MapPickup : MonoBehaviour
{
    public Enums.InventoryItems inventoryItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Inventory.Instance.ObtainItem(inventoryItem);
            Destroy(gameObject);
           
        }
    }


}
