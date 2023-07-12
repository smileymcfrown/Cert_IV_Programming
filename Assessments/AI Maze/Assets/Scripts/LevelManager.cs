using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3[] highCameraPos;
    public Vector3[] highCameraRot;
    public Vector3[] lowCameraPos;
    public Vector3[] lowCameraRot;
    public TMP_Text buttonText; 
    
    private bool highLow = true;
    private int index = 0;

    /* Removing as cannot reliably reset all variables and collections
    public void ResetScene()
    {
        Goals.goalList.Clear();
        Goals.keyList.Clear();
        SceneManager.LoadScene(0);
    }
    */
    
    public void CloseScene()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void CameraAngle()
    {
        highLow = !highLow;
        if (highLow)
        {
            buttonText.text = "HIGH";
            cameraTransform.position = highCameraPos[index];
            cameraTransform.eulerAngles = highCameraRot[index];
        }
        else
        {
            buttonText.text = "LOW";
            cameraTransform.position = lowCameraPos[index];
            cameraTransform.eulerAngles = lowCameraRot[index];
        }
    }
    
    public void MoveCamera(bool direction)
    {
        if (direction)
        {
            index++;
            if (index >= highCameraPos.Length)
            {
                index = 0;
            }
        }
        else
        {
            index--;
            if (index < 0)
            {
                index = 3;
            }
        }

        if (highLow)
        {
            cameraTransform.position = highCameraPos[index];
            cameraTransform.eulerAngles = highCameraRot[index];
        }
        else
        {
            cameraTransform.position = lowCameraPos[index];
            cameraTransform.eulerAngles = lowCameraRot[index];
        }
    }
    
}
