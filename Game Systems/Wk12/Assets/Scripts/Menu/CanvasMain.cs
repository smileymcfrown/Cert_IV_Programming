using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMain : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel, playPanel, optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        playPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void OptionsMenu()
    {
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
}
