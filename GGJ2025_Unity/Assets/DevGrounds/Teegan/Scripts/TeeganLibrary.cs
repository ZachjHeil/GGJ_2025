//Teegan Tulk
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TeeganLibrary
{
    //2025-01-25
    public static UnityEvent EventShortcut(UnityAction action)
    {
        UnityEvent unityEvent = new UnityEvent();
        unityEvent.AddListener(action);
        return unityEvent;
    }
    public static IEnumerator WaitThenRunFunction(float waitTime, UnityEvent unityEvent)
    {
        yield return new WaitForSeconds(waitTime);
        unityEvent.Invoke();
    }

    public static List<int> FindIndexes_String(char val, string fullString)
    {
        List<int> indexesFound = new List<int>();
        for (int i = 0; i < fullString.Length; i++)
        {
            if (fullString[i] == val) { indexesFound.Add(i); }

        }
        return indexesFound;
    }
   
    


    

}
