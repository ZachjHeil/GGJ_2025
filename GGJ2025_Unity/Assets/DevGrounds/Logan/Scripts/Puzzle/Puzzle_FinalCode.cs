
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_FinalCode : MonoBehaviour
{
    InputSystem_Actions controls;

    public bool[] desiredPuzzleInputs;
    public bool[] currentPuzzleInputs;
    public Toggle[] toggles;

    public TextMeshProUGUI wrongTxt;
    public TextMeshProUGUI correctTxt;

    public CanvasGroup canvasGroup;
    public AudioSource audioSource;
    public AudioClip wrongAudio;
    public AudioClip correctAudio;
    public MapPickup mapHandler;

    InputManager input;

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void Start()
    {
        input = InputManager.Instance;
        currentPuzzleInputs = new bool[desiredPuzzleInputs.Length];
    }

    private void OnEnable()
    {
        InputManager.Instance.ShowCursor();
        controls.Enable();
    }

    private void OnDisable()
    {
        InputManager.Instance.HideCursor();
        controls.Disable();
    }

    private void Update()
    {
        if (input.PlayerCancel() || input.PlayerDashed())
        {
            this.transform.parent.gameObject.SetActive(false);
            input.isInPuzzle = false;
        }
    }

    public void EnterAttempt()
    {
        for(int i = 0; i < desiredPuzzleInputs.Length; i++)
        {
            currentPuzzleInputs[i] = toggles[i].isOn;

        }

        if (!IsPuzzleCorrect())
        {
            StartCoroutine(WrongInput());
        }
        else
        {
            StartCoroutine(CorrectInput());
        }
    }

    public bool IsPuzzleCorrect()
    {
        for(int i = 0; i < desiredPuzzleInputs.Length; i++)
        {
            if (desiredPuzzleInputs[i] != currentPuzzleInputs[i])
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator WrongInput()
    {
        canvasGroup.interactable = false;
        GetComponent<AudioSource>().clip = wrongAudio;
        GetComponent<AudioSource>().Play();

        wrongTxt.enabled = true;
        yield return new WaitForSeconds(.5f);
        wrongTxt.enabled = false;
        yield return new WaitForSeconds(.5f);
        wrongTxt.enabled = true;
        yield return new WaitForSeconds(.5f);
        wrongTxt.enabled = false;
        yield return new WaitForSeconds(.5f);
        wrongTxt.enabled = true;
        yield return new WaitForSeconds(.5f);
        wrongTxt.enabled = false;
        yield return new WaitForSeconds(.5f);

        canvasGroup.interactable = true;
    }

    IEnumerator CorrectInput()
    {
        canvasGroup.interactable = false;
        audioSource.clip = correctAudio;
        audioSource.Play();

        correctTxt.enabled = true;
        yield return new WaitForSeconds(.5f);
        correctTxt.enabled = false;
        yield return new WaitForSeconds(.5f);
        correctTxt.enabled = true;
        yield return new WaitForSeconds(.5f);
        correctTxt.enabled = false;
        yield return new WaitForSeconds(.5f);
        correctTxt.enabled = true;
        yield return new WaitForSeconds(.5f);
        correctTxt.enabled = false;
        yield return new WaitForSeconds(.5f);


        //CALL CODE TO EXIT VIEW AND ENABLE FINAL INTERACT PROMPT

        mapHandler.curStep++;
        mapHandler.TriggerItemInteract();


        input.isInPuzzle = false;
        this.transform.parent.gameObject.SetActive(false);
        canvasGroup.interactable = true;
    }
}
