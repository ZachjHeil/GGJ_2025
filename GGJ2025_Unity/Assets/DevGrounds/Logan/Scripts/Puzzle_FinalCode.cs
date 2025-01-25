using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_FinalCode : MonoBehaviour
{
    public bool[] desiredPuzzleInputs;
    public bool[] currentPuzzleInputs;
    public Toggle[] toggles;

    public TextMeshProUGUI wrongTxt;
    public TextMeshProUGUI correctTxt;

    public CanvasGroup canvasGroup;
    public AudioSource audio;
    public AudioClip wrongAudio;
    public AudioClip correctAudio;


    private void Start()
    {
        currentPuzzleInputs = new bool[desiredPuzzleInputs.Length];
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
        audio.clip = wrongAudio;
        audio.Play();

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
        audio.clip = correctAudio;
        audio.Play();

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

        canvasGroup.interactable = true;
    }
}
