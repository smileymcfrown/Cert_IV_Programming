using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
	public enum CameraMovmentTypes
	{
		LockedNorth,
		LockedNorthOffset,
		FreeRotation,
		FreeRotationOffset,
	}
	
	public Transform playerPos;
	public CameraMovmentTypes camMovement;
	
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		camMovement = CameraMovmentTypes.LockedNorth;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
		if (camMovement == CameraMovmentTypes.LockedNorth)
		{
			this.transform.position = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
		}
		else if (camMovement == CameraMovmentTypes.LockedNorthOffset)
		{
			transform.position = new Vector3(playerPos.position.x, transform.position.y, playerPos.positon.z -5f);
		}
		
		
			
    }
}
