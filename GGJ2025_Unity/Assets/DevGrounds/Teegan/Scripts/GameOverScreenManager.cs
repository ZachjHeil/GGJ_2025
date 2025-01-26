using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{

    [SerializeField]
    Canvas deathCanvas;
    [SerializeField]
    PlayerStats playerStats;
    [SerializeField]
    PlayerController playerController;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUI()
    {
        
        InputManager.Instance.ShowCursor();
        deathCanvas.gameObject.SetActive(true);
    }
    
    public void HideUI()
    {
        deathCanvas.gameObject.SetActive(false);
    }
    public void TryAgain()
    {
        Debug.Log("TryAgain");
        playerController.enabled = true;
        playerStats.enabled = true;
        playerController.gameObject.SetActive(true);
        InputManager.Instance.HideCursor();
        SavingLoading.Instance.CheckpointLoad();
        HideUI();
        
    }
    public void BackToMenu()
    {
        
        InputManager.Instance.ShowCursor();
        SceneLoader.Instance.BackToMainMenu();
        
    }
  
    

}
