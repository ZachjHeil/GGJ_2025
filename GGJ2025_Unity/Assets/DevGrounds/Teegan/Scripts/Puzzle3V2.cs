//Teegan Tulk
//2025-01-26
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static TeeganLibrary;

[System.Serializable]
public class Puzzle3InputBox
{
    public Image puzzleInputBackground;
    public TextMeshProUGUI text;
    public Animator animator;


}

public class Puzzle3V2 : MonoBehaviour
{
    public string message;

    int currentIndex = 0;
    public List<Puzzle3InputBox> inputBoxes = new List<Puzzle3InputBox>();
    public Puzzle3InputBox selectedBox = new Puzzle3InputBox();

    [Header("Color")]

    public Color rightSpotColor = Color.green;
    public Color letterPartOfMessage = Color.yellow;
    public Color notPartOfMessage = Color.gray;
    public Color typingColor = Color.black;

    [Header("Animation")]
    public AnimationClip sweepIn;
    public AnimationClip reveal;

    public bool enterPressed = false;
    public bool backInProgress = false;
    [SerializeField]
    Animator backSpaceBtnAnimator;

    UnityEvent puzzleComplete;    
        

    public static Puzzle3V2 Instance { get { return instance; } }
    private static Puzzle3V2 instance;

    PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        instance = this;
        message = message.ToUpper();
     
    }
    private void OnDestroy()
    {
        instance = null;
        puzzleComplete = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if (playerController == null) { playerController = FindAnyObjectByType<PlayerController>(); }
        if (InputManager.Instance != null) { InputManager.Instance.ShowCursor(); }
        if (playerController != null) { playerController.gameObject.SetActive(false); }
        


    }
    private void OnDisable()
    {
        if (playerController == null) { playerController = FindAnyObjectByType<PlayerController>(); }
        if (InputManager.Instance != null) { InputManager.Instance.HideCursor(); }
        if (playerController != null) { playerController.gameObject.SetActive(true); }
    }
    public void UpdateInput(char letter)
    {
        if (inputBoxes[currentIndex].text.text == "") { inputBoxes[currentIndex].text.text = letter.ToString(); }
        else
        {
            if (currentIndex != inputBoxes.Count - 1)
            {
                currentIndex++;
                inputBoxes[currentIndex].text.text = letter.ToString();
            }
        }
    }
    public void Backspace()
    {
        Keyboard.Instance.PlayButtonDownAnim(backSpaceBtnAnimator);
        if (enterPressed && !backInProgress) { backInProgress = true; StartCoroutine(TriggerAnimation(sweepIn, EventShortcut(ReturnBack))); } //trigger animation? 

        if (!enterPressed && !backInProgress)
        {
            if (inputBoxes[currentIndex].text.text != "") { inputBoxes[currentIndex].text.text = ""; }
            else
            {
                if (currentIndex != 0) { currentIndex--; }
                inputBoxes[currentIndex].text.text = "";
            }
        }
    }

    public void ReturnBack()
    {
        foreach (Puzzle3InputBox box in inputBoxes) { box.puzzleInputBackground.color = typingColor; }
        StartCoroutine(TriggerAnimation(reveal, EventShortcut(ReturnBackComplete)));
    }
    public void ReturnBackComplete()
    {
        backInProgress = false;
        enterPressed = false;
    }
    public void BackSpaceIdle()
    {
        WaitThenRunFunction(0.2f, EventShortcut(ReturnBack));
    }

    public void EnterPressed()
    {
        if (enterPressed) { return; }
        //make keyboard not interactable

        //trigger animation
        if (currentIndex == inputBoxes.Count - 1)
        {
            enterPressed = true;
            //make keyboard not interactable
            //play animation
            StartCoroutine(TriggerAnimation(sweepIn, EventShortcut(TriggerAnimation)));
        }
        else
        {
            //little shake animation to indicate NO?
        }
    }
    public void AllowInputAgain()
    {
        //make keyboard usable again

    }
    
    public void PuzzleComplete()
    {
        puzzleComplete.Invoke();
    }
    public void CheckInput()
    {

        string msg = GetTypedMsg();
        if (msg == message)
        {
            foreach (Puzzle3InputBox box in inputBoxes) { box.puzzleInputBackground.color = rightSpotColor; }
            //All green
            StartCoroutine(TriggerAnimation(reveal, EventShortcut(PuzzleComplete)));
        }
        else
        {
            for (int i = 0; i < inputBoxes.Count; i++)
            {
                if (message[i].ToString() == inputBoxes[i].text.text)
                {
                    //make green
                    inputBoxes[i].puzzleInputBackground.color = rightSpotColor;
                }
                else if (message.Contains(inputBoxes[i].text.text))
                {
                    int numIndexOfMessage = FindIndexes_String(inputBoxes[i].text.text[0], message).Count;
                    int numIndexOfPiecedMsg = FindIndexes_String(inputBoxes[i].text.text[0], msg).Count;
                    if (numIndexOfMessage == numIndexOfPiecedMsg || numIndexOfMessage > numIndexOfPiecedMsg)
                    {
                        inputBoxes[i].puzzleInputBackground.color = letterPartOfMessage;
                    }
                    else if (numIndexOfPiecedMsg > numIndexOfMessage)
                    {
                        int difference = numIndexOfPiecedMsg - numIndexOfMessage;
                        int count = 0;
                        for (int j = 0; j < i; j++)
                        {
                            if (inputBoxes[j].text.text[0] == inputBoxes[i].text.text[0])
                            {
                                count++;
                            }
                        }
                        if (count == difference)
                        {
                            inputBoxes[i].puzzleInputBackground.color = notPartOfMessage;
                        }
                        else
                        {
                            inputBoxes[i].puzzleInputBackground.color = letterPartOfMessage;
                        }
                    }
                }
                else
                {

                    inputBoxes[i].puzzleInputBackground.color = notPartOfMessage;
                    //call animation?

                }
            }

            StartCoroutine(TriggerAnimation(reveal, EventShortcut(AllowInputAgain)));
        }

    }
    public void TriggerAnimation()
    {
        StartCoroutine(WaitThenRunFunction(0.2f, EventShortcut(CheckInput)));
    }

    public string GetTypedMsg()
    {
        string msg = "";
        foreach(Puzzle3InputBox box in inputBoxes) { msg += box.text.text; }
        return msg;

    }

    public IEnumerator TriggerAnimation(AnimationClip clip, UnityEvent unityEvent)
    {
        
        for (int i = 0; i < inputBoxes.Count; i++)
        {
            inputBoxes[i].animator.Play(clip.name.ToString());
            if (i != inputBoxes.Count - 1) { yield return new WaitForSeconds(0.1f); continue; }
            else
            {
                yield return new WaitForSeconds(clip.length);
                unityEvent.Invoke();
                
            }
        }
      

    }
}
