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
    private int lettersCorrect;
    private int incorrectGuesses;
    

    [SerializeField] private GameObject letterSpace;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject censoredPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Transform bladeTransform;
    [SerializeField] private TMP_Text wordText;
    [SerializeField] private float moveTime = .7f;
        
    private List<TMP_Text> wordSpaces = new List<TMP_Text>();
    

    //Event.Current  - Keybinding gives pressed key as char

    
    // Load a new word when the panel is enabled
    void OnEnable()
    {
        Debug.Log("Word Panel Enabled");
        //Load a new random word
        LoadWord();

        //Reset guesses
        incorrectGuesses = 0;
        lettersCorrect = 0;
        
        //Update the score and round number when a new word is given (or new game)
        scoreUI.ResetScore();
        scoreUI.UpdateRound();
    }
    
    // Clear the current word when the panel is disabled
    
    private void OnDisable()
    {
        Debug.Log("Word Panel Disabled");
        /*GameObject gO = GameObject.Find("Keyboard");

        for (int i = gO.transform.childCount - 1; i >= 0; i--)
        {
            Button button = gO.transform.GetChild(i).GetComponent<Button>();
            button.interactable = true;
            ColorBlock colours = button.colors;
            colours.normalColor = Color.white;
            button.colors = colours;
        }

        for (int i = transform.childCount -1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }*/
    }
    
    #region Load A New Random Word

    public void LoadWord()
    {
        //Clear last word from wordSpaces list
        wordSpaces.Clear();
        //Setting up things to loop through until finding an unused word and stopping loop from going infinite
        bool wordCheck = false;
        int currentCount = 0;  //GameManager.Instance.usedWords.Count;

        //Do a loop until an unused word is found
        while (!wordCheck)
        {
            Debug.Log("Trying to get word from list");
            word = GameManager.Instance.wordList[Random.Range(0, GameManager.Instance.wordList.Length - 1)].ToUpper();
            Debug.Log("Word found: " + word);
            if (!GameManager.Instance.usedWords.Contains(word))
            {
                Debug.Log("Word not used yet.");
                
                //To check if words contain characters like ' or - this can be removed if
                //support for those characters is added.
                if (word.All(char.IsLetterOrDigit))
                {
                    GameManager.Instance.usedWords.Add(word);
                    wordCheck = true;
                    //word = "cliché".ToUpper();     // Test word - Use "cliché" or "characteristically"
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
        
        if (spacing > 40) { spacing = 40; } 
        float startPos = 25 - (((spacing * (word.Length-1)) + (word.Length*50)) /2);
        
        for (int i = 1; i <= word.Length; i++)
        {
            GameObject gO = Instantiate(letterSpace,transform);//,transform.position+new Vector3(startPos + (spacing * i), 0, 0),letterSpace.transform.rotation,transform);
            if(i == 1){gO.transform.localPosition = new Vector3(startPos, 0, 0);}
            else{gO.transform.localPosition = new Vector3(startPos + ((spacing + 50)* (i-1)), 0, 0);}
            wordSpaces.Add(gO.GetComponent<TMP_Text>());
        }
    }
    #endregion

    #region Check if the chosen letter is in the current word
    public void CheckLetter(string letter)
    {
        bool letterFound = false;
        Button button = GameObject.Find("Button_" + letter).GetComponent<Button>();
        
        // Testing! Remove!
        Debug.Log("Check Letter! - " + letter);
        
        //Loop through word and check each letter (also checking E against É for words like CLICHÉ)
        int i = 0;
        foreach (char c in word)
        {
            if(letter.ToCharArray()[0] == c || (letter == "E" && c == 'É'))
            {
                //Update the word space with the correct letter of É if it's there
                if (c == 'É') { wordSpaces[i].text = "É"; } 
                else { wordSpaces[i].text = letter; }

                //Increase number of correct letters and flag guess as correct
                lettersCorrect++;
                letterFound = true;

                //Update the score with the points for the letter
                scoreUI.UpdateScore(letter.ToCharArray()[0]);
            }

            i++;
        }

        // Finish and show the Win Game panel if they complete the word
        if(lettersCorrect >= word.Length)
        {
            //Give 10 bonus points if they guess the word without mistakes
            if (incorrectGuesses == 0)
            {
                scoreUI.UpdateScore('Z');
            }
            
            //Show Win Panel and end game
            winPanel.SetActive(true);
        }

        // Play the 'hangman' animation and increase incorrect guess if the wrong letter
        if (!letterFound)
        {
            Debug.Log("You messed up!");
            
            //Increase incorrect guesses by one.
            incorrectGuesses++;
            
            //Call a coroutine to do the next hangman animation
            StartCoroutine(BladeMove());
        }
        
        // Fade the letter and disable the button
        button.interactable = false;
        button.GetComponentInChildren<TMP_Text>().color = Color.grey;
    }
    #endregion

    IEnumerator BladeMove()
    {
        Vector3 startingPos  = bladeTransform.position;
        Vector3 finalPos = new Vector3();
        
        //Move blade into view for first mistake, then left for each mistake after
        if (incorrectGuesses == 1)
        {
            finalPos = bladeTransform.position + new Vector3(0, 285, 0);
        }
        else
        {
            finalPos = bladeTransform.position + new Vector3(-77, 0, 0);
        }

        float elapsedTime = 0;
        
        //Move the blade towards finalPos
        while (elapsedTime < moveTime)
        {
            bladeTransform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Check of the number of guesses and end game at 8
        if (incorrectGuesses >= 8)
        {
            Debug.Log("You lost!");
            //Load Lose Panel
            wordText.text = word;
            losePanel.SetActive(true);
            censoredPanel.SetActive(true);
        }

    }
}
