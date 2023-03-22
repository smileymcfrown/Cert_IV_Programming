using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Alive;
    // Private instance to prevent tampering
    private static GameManager _instance;
    // Public instance to be accessed
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    // On Awake, check for other gamemanager instance and make only one instance.
    void Awake()
    {
        // Check for an instance that is NOT this instance
        if (Instance != null && Instance != this)
        {
            // Am I not the only one?! Destroy me!
            Destroy(this);
        }
        else
        {
            // I am the ONLY ONE!
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum GameState
{
    Alive, Dead, Pause, Menus
}