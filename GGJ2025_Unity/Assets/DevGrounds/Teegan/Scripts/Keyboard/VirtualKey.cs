using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VirtualKey : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Image image;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public char letter = ' ';
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
    }
    public void SetKeyText(string text)
    {
        this.text.text = text;
    }

    public string GetKeyText()
    {
        return text.text;
    }
    

   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonDown()
    {
        //buttonPressed.Invoke();
    }
    public void ButtonDownGiveLetter()
    {
        
        Keyboard.Instance.PlayButtonDownAnim(this);
        Puzzle3V2.Instance.UpdateInput(letter);
    }


    

    
    
    
}
