using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{

    [SerializeField]
    Canvas deathCanvas;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUI()
    {
        deathCanvas.gameObject.SetActive(true);
    }
    
    public void HideUI()
    {
        deathCanvas.gameObject.SetActive(false);
    }
    public void TryAgain()
    {
        SavingLoading.Instance.CheckpointLoad();
        HideUI();
        
    }
    public void BackToMenu()
    {
        SceneLoader.Instance.BackToMainMenu();
        
    }

}
