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
    private RotationalAxis _axis = RotationalAxis.MouseX;
    static public float sensitivity = 15f;
    static public bool invertMouseY = false;
    private Vector2 _clamp = new Vector2(-60f, 60f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
