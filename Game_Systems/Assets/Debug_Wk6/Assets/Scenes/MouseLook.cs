﻿using UnityEngine;
//Errors : 17
namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Mouse Look")]

    public class Mouselook : MonoBehaviour
    {
        public enum RotationalAxis
        {
            MouseX,
            MouseY
        }
        
        [Header("Rotation Variables")]
        public RotationalAxis axis = RotationalAxis.MouseX;

        [Range(0,200)]
        public float sensitivity = 100;
        public float minY = -60, maxY = 60;
        private float _rotY;

        void Start()
        { /*
            if(GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freeze = true;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if(GetComponent<Camera>())
            {
                axis = RotationalAxis.MouseY;
            } */
        }
        void update()
        {
            /*
            if(axis == RotationalAxis.MouseX)
            {
                transform.Rotate(0,Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime,0);

            }
            else
            {
                _rotY += Input.GetAxis("MouseY")  * sensitivity * Time.deltaTime;
                _rotY = Mathf.Clamp(_rotY,minY,maxY);
                transform.localEuler = new Vector3(-_rotY,0.0f);
            }
            */
        }
    }   
}
