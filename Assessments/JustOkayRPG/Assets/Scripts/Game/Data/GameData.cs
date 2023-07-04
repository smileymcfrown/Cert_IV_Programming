using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public static GameData gameData;
    public int health;
    public int mana;
    public int stamina;

    //public Vector3 playerPosition;
    //public Vector3 playerRotation;
    
    public float[] lastCheckpoint = new float[3];
    public float[] playerPosition = new float[3];
    public float[] playerRotation = new float[3];


    public GameData()
    {
        this.health = 100;
        this.mana = 100;
        this.stamina = 100;
        
        //this.playerPosition = new Vector3(360f, 9.77f, 385f);
        //this.playerRotation = new Vector3(0, 0, 0);
        
        this.lastCheckpoint[0] = 0; // Do you have to do three lines like this?
        this.lastCheckpoint[1] = 0; // Can you add all values in one line?
        this.lastCheckpoint[2] = 0;
        this.playerPosition[0] = 360f;
        this.playerPosition[1] = 10f;
        this.playerPosition[2] = 385f;
        this.playerRotation[0] = 0;
        this.playerRotation[1] = 0;
        this.playerRotation[2] = 0;
    }

}
