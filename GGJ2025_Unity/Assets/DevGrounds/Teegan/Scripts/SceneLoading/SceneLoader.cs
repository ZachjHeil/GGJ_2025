using UnityEngine;
using UnityEngine.Rendering;
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

        if(!string.IsNullOrEmpty(mainMenuSceneName))
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
        if (!string.IsNullOrEmpty(mainMenuSceneName))
            SceneManager.UnloadSceneAsync(mainMenuSceneName);
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
    }

    public void LoadUnloadScene(string unload, string load)
    {
        SceneManager.UnloadSceneAsync(unload);
        SceneManager.LoadScene(load, LoadSceneMode.Additive);
    }
    

}
