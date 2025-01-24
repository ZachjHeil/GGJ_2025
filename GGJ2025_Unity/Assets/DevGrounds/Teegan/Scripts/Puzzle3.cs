//Teegan Tulk
//2025-01-24 
//Global Game Jam 2025
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle3 : MonoBehaviour
{
    [SerializeField]
    string message;

    [Header("This cannot be the input field's TextMeshProGUI directly. \n (Trust me I spent too long trying to troubleshoot until I realized.) \n \n Duplicate the text field's TextMeshProUGUI and assign the duplicate here. \n On the plus side it " +
        "gives the letters a nice outline when they change color!")]
    [SerializeField]
    TextMeshProUGUI duplicatedTextOnTop;

    public UnityEvent onPuzzleComplete;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        onPuzzleComplete = null;
    }
    public void TextboxUpdate(string text)
    {
        
        string msg = "";
        if (text == message)
        { //all green
            msg += "<color=\"green\">";
            msg += message;
            msg += "</color>";
            duplicatedTextOnTop.text = msg;
            PuzzleComplete();
        }
        else
        {

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == message[i])
                {
                    //turn letter green //right spot
                    msg += "<color=\"green\">";
                    msg += text[i];
                    msg += "</color>";
                }
                else
                {
                    int index = message.IndexOf(text[i]);
                    if (index != -1)
                    {
                        //letter is in word
                        //turn letter yellow 
                        msg += "<color=\"yellow\">";
                        msg += text[i];
                        msg += "</color>";
                    }
                    else
                    {
                        //letter is not in word
                        //turn letter gray
                        //#808080
                        msg += "<color=#808080>";
                        msg += text[i];
                        msg += "</color>";
                    }
                }
            }
            duplicatedTextOnTop.text = msg;
        }
    }
    public void PuzzleComplete()
    {
        onPuzzleComplete.Invoke();

        Debug.Log("Puzzle Complete");
    }
    
}
