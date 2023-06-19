using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{
    #region Structs
    [Serializable]
    public struct Attribute
    {
        //Name of the attribute
        public string name;
        public float currentValue;
        public float maxValue;
        //the regen value (eg heal over time or regen from spell or potion)
        public float regenValue;
        //the bar that displays the fill amount (eg health bar)
        public Image displayImage;
    }
    #endregion
    
    #region Variables
    public Attribute[] attributes = new Attribute[0];
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
