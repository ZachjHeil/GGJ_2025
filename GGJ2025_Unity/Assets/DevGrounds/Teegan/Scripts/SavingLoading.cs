using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    public InventoryClass inventory = new InventoryClass();
    public Vector3 position;
   
}
public class SavingLoading : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    public string location;

    public static SavingLoading Instance { get { return Instance; } }
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

    public void SaveInventory(InventoryClass inventory)
    {
        saveData.inventory = inventory;
    }
    public InventoryClass SupplyInventory()
    {
        return saveData.inventory;
    }
  
    
}
