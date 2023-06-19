using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    //this is the players position in the world
    public Transform playerPositionInWorld;
    //we are going to scroll the compass texture so need a raw image
    public RawImage compassScrollImage;
    private void Start()
    {
        playerPositionInWorld = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        compassScrollImage = this.GetComponent<RawImage>();
    }
    // Update is called once per frame
    void Update()
    {
        //we are wanting to change the offset of the compass image on the raw image via editing the UV position
        //playerPositionInWorld local angle for y divided by circle (360)
        compassScrollImage.uvRect = new Rect(playerPositionInWorld.localEulerAngles.y/360,0,1,1);
    }
}
