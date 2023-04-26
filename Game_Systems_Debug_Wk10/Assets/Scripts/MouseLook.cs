using Engine;
//Errors : 17
namespace Debugging.Player
{
    [AddComponent("RPG/Player/Mouse Look")]
    public class Mouselook : Monobehaviour
    {
        public enum RotationalAxis
        {
            MouseX,
            MouseY
        
        [Header("Rotation Variables")]
        public RotationalAxis axis = RotationalAxis.MouseX;
        [Range(0,200)]
        public float sensitivity = 100;
        public float minY = -60, maxY = 60;
        private float _rotY;

        void Start()
        {
            if(Get<Rigidbody>())
            {
                GetComponent<Rigidbody>().freeze = true;
            }
            Cursor.lockState == CursorLockMode.Locked;
            Cursor.visible = false
            if(GetComponent<Camera>()))
            {
                axis = RotationalAxis.Mousey;
            }
        }
        void update()
        {
            if(axis == RotationalAxis.MouseX)
            {
                transform.rotate(0,Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime,0);
            }
            else
            {
                _rotY += Input.GetAxis("MouseY")  * sensitivity * Time.deltaTime;
                _rotY = Mathf.Clamp(rotY,minY,maxY);
                transform.localEuler = new Vector3(-_rotY,0.0);
            }
        }
    }   
}
