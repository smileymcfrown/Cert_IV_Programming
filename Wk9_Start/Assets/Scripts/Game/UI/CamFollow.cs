using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum CameraMovementTypes
    { 
        LockedNorth,
        LockedNorthOffset,
        FreeRotation,
        FreeRotationOffset
    }
    public Transform playerPos;
    public CameraMovementTypes camMovement;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (camMovement == CameraMovementTypes.LockedNorth)
        {
            this.transform.position = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
        }
        else if (camMovement == CameraMovementTypes.LockedNorthOffset)
        {
            this.transform.position = new Vector3(playerPos.position.x+offset.x, transform.position.y + offset.y, playerPos.position.z + offset.z);
        }
        else if (camMovement == CameraMovementTypes.FreeRotation)
        {
            this.transform.position = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
            this.transform.rotation = Quaternion.Euler(90, playerPos.eulerAngles.y, 0);
        }
        else if (camMovement == CameraMovementTypes.FreeRotationOffset)
        {
            this.transform.position = new Vector3(playerPos.position.x + offset.x, transform.position.y + offset.y, playerPos.position.z + offset.z);
            this.transform.rotation = Quaternion.Euler(90, playerPos.eulerAngles.y, 0);
        }
    }
}
