using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("Game System RPG/Managers/Game Manager")]
public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Alive;
    private static GameManager _instance;
    public static GameManager Instance
    {
        //Read
        get 
        {
            return _instance;
        }
        //Write
        private set 
        {
            _instance = value;
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
public enum GameState
{
    Alive,
    Dead,
    Pause,
    AnyOtherScreen
}