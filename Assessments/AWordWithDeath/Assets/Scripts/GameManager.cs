using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string word;
    private LoadWords loadWords;
    private bool letterFound = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void LoadWord()
    {
        word = loadWords.SelectWord();

        
        foreach(char c in word)
        {
            // Instantiate a space prefab in to fit within the panel
        }
    }

    public void CheckLetter(char letter)
    {
        foreach (char c in word)
        {
            if(letter == c)
            {
                // Put the letter into the corect space
                letterFound = true;
            }
        }

        if (letterFound)
        {
            // Fade the letter and disable the button
        }
    }


}
