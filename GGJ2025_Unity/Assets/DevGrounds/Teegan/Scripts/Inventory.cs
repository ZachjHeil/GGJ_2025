//Teegan Tulk
//2025-01-24
//Global Game Jam 2025
using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
[Serializable]

public class InventoryClass
{
   //Items
   public bool obtainedMapPiece1 = false;
   public bool obtainedMapPiece2 = false;
   public bool obtainedMapPiece3 = false;
   public bool obtainedMapPiece4 = false;
}
public class Inventory : MonoBehaviour
{

    public Image mapPiece1Image;
    public Sprite mapPiece1CollectedSprite;
    public Sprite mapPiece1NotCollectedSprite;


    public Image mapPiece2Image;
    public Sprite mapPiece2CollectedSprite;
    public Sprite mapPiece2NotCollectedSprite;


    public Image mapPiece3Image;
    public Sprite mapPiece3CollectedSprite;
    public Sprite mapPiece3NotCollectedSprite;

    public Image mapPiece4Image;
    public Sprite mapPiece4CollectedSprite;
    public Sprite mapPiece4NotCollectedSprite;



    public static Inventory Instance { get { return instance; } }
    public static Inventory instance;
    public InventoryClass inventory = new InventoryClass();


    List<MapPickup> maps = new List<MapPickup>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SavingLoading.Instance != null) { inventory = SavingLoading.Instance.SupplySavedInventory(); } else { Debug.LogError("Load persistent scene first!"); }
        ParseInventory();

        maps = FindObjectsByType<MapPickup>(sortMode:FindObjectsSortMode.None).ToList();
    }

    public void ParseInventory()
    {
        //Handle UI
        UpdateInventoryUI(inventory.obtainedMapPiece1, mapPiece1CollectedSprite, mapPiece1NotCollectedSprite, mapPiece1Image);
        UpdateInventoryUI(inventory.obtainedMapPiece2, mapPiece2CollectedSprite, mapPiece2NotCollectedSprite, mapPiece2Image);
        UpdateInventoryUI(inventory.obtainedMapPiece3, mapPiece3CollectedSprite, mapPiece3NotCollectedSprite, mapPiece3Image);
        UpdateInventoryUI(inventory.obtainedMapPiece4, mapPiece4CollectedSprite, mapPiece4NotCollectedSprite, mapPiece4Image);

        //Handle GameObject
        List<MapPickup> pickups = FindObjectsByType<MapPickup>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        foreach (MapPickup p in pickups)
        {
            switch (p.inventoryItem)
            {
                case Enums.InventoryItems.FirstMapPiece:
                    p.gameObject.SetActive(!inventory.obtainedMapPiece1);
                    break;
                case Enums.InventoryItems.SecondMapPiece:
                    p.gameObject.SetActive(!inventory.obtainedMapPiece2);
                    break;
                case Enums.InventoryItems.ThirdMapPiece:
                    p.gameObject.SetActive(!inventory.obtainedMapPiece3);
                    break;
                case Enums.InventoryItems.FourthMapPiece:
                    p.gameObject.SetActive(!inventory.obtainedMapPiece4);
                    break;
            }
           
        }


    }
    
    
    public void UpdateInventoryUI(bool ownedState, Sprite obtainedSprite, Sprite notObtained, Image imageIcon)
    {
        if (ownedState) { imageIcon.sprite = obtainedSprite; } else { imageIcon.sprite = notObtained; }
    }
    
    public void ObtainItem(Enums.InventoryItems item)
    {
        switch(item)
        {
            case Enums.InventoryItems.FirstMapPiece:
                inventory.obtainedMapPiece1 = true;
                mapPiece1Image.sprite = mapPiece1CollectedSprite;
                break;
            case Enums.InventoryItems.SecondMapPiece:
                inventory.obtainedMapPiece2 = true;
                mapPiece2Image.sprite = mapPiece2CollectedSprite;
                break;
            case Enums.InventoryItems.ThirdMapPiece:
                inventory.obtainedMapPiece3 = true;
                mapPiece3Image.sprite = mapPiece3CollectedSprite;
                break;
            case Enums.InventoryItems.FourthMapPiece:
                inventory.obtainedMapPiece4 = true;
                mapPiece4Image.sprite = mapPiece4CollectedSprite;
                break;
        }

        foreach(MapPickup map in maps)
        {
            if(map.inventoryItem == item)
            {
                map.TriggerItemInteract();
                break;
            }
        }
    }

    public void CheckPointTriggered()
    {
        SavingLoading.Instance.SaveInventory(inventory);
    }
    public bool CheckIfObtained(Enums.InventoryItems items)
    {
        switch(items)
        {
            case Enums.InventoryItems.FirstMapPiece:
                return inventory.obtainedMapPiece1;
            case Enums.InventoryItems.SecondMapPiece:
                return inventory.obtainedMapPiece2;
            case Enums.InventoryItems.ThirdMapPiece:
                return inventory.obtainedMapPiece3;
            case Enums.InventoryItems.FourthMapPiece:
                return inventory.obtainedMapPiece4;
        }
        return false;
    }

    



    
   
}
