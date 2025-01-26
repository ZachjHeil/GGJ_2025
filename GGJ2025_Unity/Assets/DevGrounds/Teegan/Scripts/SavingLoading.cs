//Teegan Tulk
//2025-01-24
//Global Game Jam 2025
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    public InventoryClass inventory = new InventoryClass();
    public bool newSave = true;
   
}
public class SavingLoading : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    public string location;

    public static SavingLoading Instance { get { return instance; } }
    public static SavingLoading instance;

    

    private void Awake()
    {
        instance = this;
        location = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "TeamA_BubblesSaveData.json";
        LookForSaveData();
        LoadGame();
    }
    private void OnDestroy()
    {
        instance = null;
    }

    public void LookForSaveData()
    {
        if (File.Exists(location))
        {

        }
        else
        {
            SaveGame();
        }
    }

    public void LoadGame()
    {
        string data = "";

        using (StreamReader read = new StreamReader(location))
        {
            data = read.ReadToEnd();
            read.Close();
           
        }
        saveData = JsonUtility.FromJson<SaveData>(data);
        
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData);

        using (StreamWriter write = new StreamWriter(location))
        {
            write.Write(json);
            write.Close();
        }
    }

    public bool GetIfNewSave()
    {
        return saveData.newSave;
    }
    public void SetNewSave(bool newSave)
    {
        saveData.newSave = newSave;
    }
    public void SaveInventory(InventoryClass inventory)
    {
        saveData.inventory = inventory;
    }
    public void CheckpointSave()
    {
        saveData.newSave = false;
        Debug.Log("Checkpoint saving");
        Inventory.instance.CheckPointTriggered();
        SaveGame();
    }
    public void CheckpointLoad()
    {
        Inventory.Instance.inventory = saveData.inventory;
        Inventory.instance.ParseInventory();
        
    }
    public InventoryClass SupplySavedInventory()
    {
        return saveData.inventory;
    }

    public void ResetSaveFile()
    {
        saveData = new SaveData();
        if (Inventory.instance != null)
        { 
            Inventory.instance.inventory = saveData.inventory;
            Inventory.instance.ParseInventory();
        }
        SaveGame();
        

    }

    
  
    
}
