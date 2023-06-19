using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private enum RotationalAxis
    {
        MouseX,
        MouseY
    }
    [SerializeField] private RotationalAxis _axis = RotationalAxis.MouseX;
    static public float sensitivity = 15f;
    static public bool invertMouseY = false;
    private Vector2 _clamp = new Vector2(-60f, 60f);
    private float _rotationY;


    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "Player")
        {
            _axis = RotationalAxis.MouseX;
        }
        else
        {
            _axis = RotationalAxis.MouseY;
        }

        //If there's a RigidBody then freeze the rotation so there is no conflict in movement
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
