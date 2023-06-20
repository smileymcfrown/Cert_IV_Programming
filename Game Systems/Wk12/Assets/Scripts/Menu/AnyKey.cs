using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnyKey : MonoBehaviour
{
    [SerializeField] private GameObject anyKeyPnl, mainPnl;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            mainPnl.SetActive(true);
            anyKeyPnl.SetActive(false);
        }
    }
}
