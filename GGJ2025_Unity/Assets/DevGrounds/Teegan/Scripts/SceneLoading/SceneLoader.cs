using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string mainMenuSceneName;
    public string gameSceneName;

    public static SceneLoader Instance { get { return instance; } } 
    static SceneLoader instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Additive);
    }
    private void OnDestroy()
    {
        instance = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToMainMenu()
    {
        SceneManager.UnloadSceneAsync(gameSceneName);
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Additive);
    }
  

    public void ToGameScene()
    {
        SceneManager.UnloadSceneAsync(mainMenuSceneName);
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
    }
    

}
