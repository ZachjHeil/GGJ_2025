//Teegan Tulk
//2025-01-24
//Global Game Jam 2025
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    public InventoryClass inventory = new InventoryClass();
    public bool underwaterState = false;
    public Vector3 position = new Vector3();
    public bool newSave = true;
   
}
public class SavingLoading : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    public string location;




    public static SavingLoading Instance { get { return instance; } }
    public static SavingLoading instance;

    public PlayerController playerController;



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
            saveData.newSave = true;
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
        if (playerController == null) { playerController = GetComponent<PlayerController>(); }
        if (playerController != null)
        {
            saveData.position = playerController.transform.position;
            saveData.underwaterState = playerController.underWater;
        }
        Debug.Log("Checkpoint saving");
        Inventory.instance.CheckPointTriggered();

        SaveGame();
    }
    public void CheckpointLoad()
    {

        Inventory.Instance.inventory = saveData.inventory;
        Inventory.instance.ParseInventory();

        if (playerController == null) { playerController = GetComponent<PlayerController>(); }
        if (playerController != null)
        {
            playerController.underWater = saveData.underwaterState;
            playerController.transform.position = saveData.position;
            playerController.GetComponent<PlayerStats>().PlayerRevived();
        }

    }
    public Vector3 GetSavedStartingPos()
    {
        return saveData.position;
    }
    public bool GetSavedWaterState()
    {
        return saveData.underwaterState;
    }




    public InventoryClass SupplySavedInventory()
    {
        return saveData.inventory;
    }

    public void ResetSaveFile()
    {
        saveData = new SaveData();
        saveData.newSave = true;
        if (Inventory.instance != null)
        {
            Inventory.instance.inventory = saveData.inventory;
            Inventory.instance.ParseInventory();
        }
        SaveGame();


    }

    public void SavePosIfNew(Transform pos)
    {
        if (saveData.newSave)
        {
            saveData.position = pos.position;
            SaveGame();
        }
    }
    public Vector3 GetSavedPos()
    {
        return saveData.position;
    }
    public void SetSavedPos(Vector3 pos)
    {
        saveData.position = pos;
        SaveGame();
    }
    public void SetUnderwaterState(bool state)
    {
        saveData.underwaterState = state;
        SaveGame();
    }
    public bool GetUnderwaterState()
    {
        return saveData.underwaterState;
    }

    
}
    




