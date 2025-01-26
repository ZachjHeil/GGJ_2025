
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class VirtualKeyboardRow
{

    public string rowLetters = "";
    public Transform layoutGroup;
    [HideInInspector]
    public List<VirtualKey> keys = new List<VirtualKey>();

}

public class Keyboard : MonoBehaviour
{
    string row1 = "qwertyuiop";
    string row2 = "asdfghjkl";
    string row3 = "zxcvbnm";

    static public Keyboard Instance { get { return instance; } }
    static Keyboard instance;

    public List<VirtualKeyboardRow> rows = new List<VirtualKeyboardRow>();
    public GameObject buttonPrefab;

    public UnityEvent buttonPressed;
    public UnityEvent enterPressed;
    public UnityEvent backspacePressed;
    public UnityEvent shiftPressed;

    public AnimationClip buttonDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        for (int row = 0; row < rows.Count; row++)
        {
            for (int i = 0; i < rows[row].rowLetters.Length; i++)
            {
                VirtualKey key = GameObject.Instantiate(buttonPrefab, rows[row].layoutGroup).GetComponent<VirtualKey>();
                key.SetKeyText(rows[row].rowLetters[i].ToString());
                key.letter = rows[row].rowLetters[i];
                rows[row].keys.Add(key);
            }

        }
    }
    private void OnDestroy()
    {
        buttonPressed = null;
        enterPressed = null;
        backspacePressed = null;
        shiftPressed = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonDownAnim(Animator animator)
    {
        animator.Play(buttonDown.name.ToString());
    }
    public void PlayButtonDownAnim(VirtualKey key)
    {
        
        key.animator.Play(buttonDown.name.ToString());
    }
   

}
