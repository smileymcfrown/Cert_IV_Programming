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
    public Vector3 lastCheckpoint; // How best to store this?
    public Vector3 playerPosition;
    public Vector3 playerRotation;


    public GameData()
    {
        this.health = 100;
        this.mana = 100;
        this.stamina = 100;
        this.lastCheckpoint = Vector3.zero; // See above
        this.playerPosition = new Vector3(360f,9.77f, 385);
        this.playerRotation = new Vector3(0, 0, 0);
    }

}
