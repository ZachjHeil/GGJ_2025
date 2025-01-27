using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    bool isEnding;
    public GameObject endSceen;

    public Transform flareRB;

    public CanvasGroup fade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isEnding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnding)
        {
            if (InputManager.Instance.PlayerInteract())
            {
                isEnding = true;
                StartCoroutine(EndSequence());
            }
        }
    }

    IEnumerator EndSequence()
    {
        float curTime = 0;
        while (curTime < 0.5f)
        {
            curTime += Time.deltaTime;
            flareRB.Translate(0, .05f, 0);
            yield return null;
        }

        curTime = 0;
        while (curTime < 0.5f)
        {
            curTime += Time.deltaTime;
            flareRB.Translate(0, .1f, 0);
            yield return null;
        }

        curTime = 0;
        while (curTime < 1.5f)
        {
            curTime += Time.deltaTime;
            flareRB.Translate(0, .15f, 0);
            yield return null;
        }

        while (fade.alpha < 1)
        {
            fade.alpha += Time.deltaTime;
            flareRB.Translate(0, .15f, 0);
            yield return null;
        }

        endSceen.SetActive(true);
        fade.alpha = 0;
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadUnloadScene("EndScene", "MainMenu");
    }
}
