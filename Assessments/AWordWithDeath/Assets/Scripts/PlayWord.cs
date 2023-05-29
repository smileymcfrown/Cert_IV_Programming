using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Linq;
using TMPro;

public class PlayWord : MonoBehaviour
{
    private string word;
    //private LoadWords loadWords;
    
    [SerializeField] private GameObject letterSpace;
    private List<TMP_Text> wordSpaces = new List<TMP_Text>();
    
    // Load a new word when the panel is enabled
    void OnEnable()
    {
        LoadWord();
    }
    
    // Clear the current word when the panel is disabled
    /*
    private void OnDisable()
    {
        for (int i = transform.childCount; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }*/

    // Update will likely not be used
    void Update()
    {
        //Debug.Log("Fuck Rider");
    }

    // Load a new word
    public void LoadWord()
    {
        //Clear last word from wordSpaces list
        wordSpaces.Clear();
        //Setting up things to loop through until finding an unused word and stopping loop from going infinite
        bool wordCheck = false;
        int currentCount = 0;//GameManager.Instance.usedWords.Count;
        

        //Do a loop until an unused word is found
        while (!wordCheck)
        {
            word = GameManager.Instance.wordList[Random.Range(0, GameManager.Instance.wordList.Length - 1)].ToUpper();
            if (!GameManager.Instance.usedWords.Contains(word))
            {
                Debug.Log("Word not used yet.");
                //To check if words contain characters like ' or - this can be removed if
                //support for those characters is added.
                if (word.All(char.IsLetterOrDigit))
                {
                    GameManager.Instance.usedWords.Add(word);
                    wordCheck = true;
                    word = "CHARACTERISTICALLY"; // Test word
                    Debug.Log("The word of the day is " + word);
                }
                else
                {
                    GameManager.Instance.usedWords.Add(word);
                    Debug.Log("NOT A GOOD WORD: " + word);
                }
            }
            
            //Break loop if it's not finding a word to stop it going infinite
            if (GameManager.Instance.usedWords.Count > currentCount + 100)
            {
                Debug.Log("Just cycled through 100 used or poor words. Something's wrong!");
                wordCheck = true;
                Debug.Log("Loop Failed!");
            }
        }

        
        // Instantiate a letter space prefabs in to fit within the panel
        float spacing = (transform.GetComponent<RectTransform>().rect.width - 40 - (word.Length * 50)) / (word.Length + 2);
        Debug.Log("Initial Spacing: " + spacing);
        if (spacing > 60) { spacing = 60; } 
        
        float startPos = 25 - (((spacing * (word.Length-1)) + (word.Length*50)) /2);
        
        Debug.Log("SPACING!! "+spacing);

        
        for (int i = 1; i <= word.Length; i++)
        {
            GameObject gO = Instantiate(letterSpace,transform);//,transform.position+new Vector3(startPos + (spacing * i), 0, 0),letterSpace.transform.rotation,transform);
            if(i == 1){gO.transform.localPosition = new Vector3(startPos, 0, 0);}
            else{gO.transform.localPosition = new Vector3(startPos + ((spacing + 50)* (i-1)), 0, 0);}
            wordSpaces.Add(gO.GetComponent<TMP_Text>());
            Debug.Log(i-1 + " : " + wordSpaces[i-1].text);
        }
    }

    //Check if the chosen letter is in the current word
    public void CheckLetter(string letter)
    {
        bool letterFound = false;
        Button button = GameObject.Find("Button_" + letter).GetComponent<Button>();
        
        Debug.Log("Check Letter! - " + letter);
        
        int i = 0;
        foreach (char c in word)
        {
            if(letter.ToCharArray()[0] == c || (letter == "E" && c == 'É'))
            {

                wordSpaces[i].text = letter;
                letterFound = true;
            }

            i++;
        }

        if (!letterFound)
        {
            Debug.Log("You Fucked Up!");
        }
        
        // Fade the letter and disable the button
        button.interactable = false;
        ColorBlock colours = button.colors;
        colours.normalColor = Color.grey;
        button.colors = colours;

    }

}
