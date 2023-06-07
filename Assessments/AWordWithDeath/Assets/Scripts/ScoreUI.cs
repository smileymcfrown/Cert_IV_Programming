using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreValue;
    public TMP_Text roundValue;  

    // Reset Score UI to zero for new game
    public void ResetScore()
    {
        if (GameManager.Instance.score == 0)
        {
            scoreValue.text = "00000";
        }
    }


    // Update the score UI when correct letter guessed.
    public void UpdateScore(char letter)
    {
        int letterScore = 0;

        switch (letter)
        {
            case 'A':
            case 'E':
            case 'I':
            case 'O':
            case 'U':
            case 'L':
            case 'N':
            case 'S':
            case 'T':
            case 'R':
                letterScore = 1;
                break;
            case 'D':
            case 'G':
                letterScore = 2;
                break;
            case 'B':
            case 'C': 
            case 'M': 
            case 'P':
                letterScore = 3;
                break;

            case 'F':
            case 'H':
            case 'V':
            case 'W':
            case 'Y':
                letterScore = 4;
                break;
            case 'K':
                letterScore = 5;
                break;
            case 'J':
            case 'X':
                letterScore = 8;
                break;
            case 'Q':
            case 'Z':
                letterScore = 10;
                break;
            default:
                Debug.Log("This letter: \'" + letter + "\' is not matching a score for some reason!");
                break;
        }

        //Testing! Remove!
        Debug.Log("Letter: " + letter + ", Letter Score: " + letterScore);

        //Update the total score
        GameManager.Instance.score += letterScore;

        //Testing Remove!
        Debug.Log("New score: " + GameManager.Instance.score);
        scoreValue.text = GameManager.Instance.score.ToString();

        //GAVIN! - Add code from Cert IV art game for updating the score because you put a nice animation on it and it has a nice pixel art font you made!
    }

    // Update the number of rounds when a new round or game starts
    public void UpdateRound()
    {
        roundValue.text = GameManager.Instance.round.ToString();
    }
}
