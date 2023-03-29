using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    //Str
    //Dex
    //Con
    //Int
    //Wis
    //Char
    #region Struct

    [Serializable]

    public struct StatBlock
    {
        //name of our stat
        public string name;
        //base value
        public int statValue;
        //buff or debuff value being applied
        public int tempStatValue;
        //temp value for levelling
        public int levelTempStatValue;
    }
    #endregion
    #region Variables
    #endregion
}

