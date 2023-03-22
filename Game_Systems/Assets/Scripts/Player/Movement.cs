using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [AddComponentMenu("Game System RPG/Player/Movement")] // Change the way the script is referenced in the "Add Component" menu
    [RequireComponent(typeof(CharacterController))]  // Automatically adds this component and won't allow it to be removed

    public class Movement : MonoBehaviour
    {
        private CharacterController _charC;
        
        [SerializeField] private Vector3 _moveDir = Vector3.zero;
        
        [Space(25), Header("Speeds")]
        public float speed = 5f;
        public float gravity = 20f, jumpSpeed = 8;

        // Start is called before the first frame update
        void Start()
        {
            _charC = GetComponent<CharacterController>(); // Assigning the Character Controller to a variable
        }

        // Update is called once per frame
        void Update()
        {
            if(GameManager.Instance.gameState == GameState.Alive) // Is the player alive?
            {
                if (_charC.isGrounded) // Is the player standing on the ground?
                {
                    // Move the player in a direction relative to the player (not worldspace) at 'speed'
                    _moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed);
                    if (Input.GetButton("Jump"))
                    {
                        _moveDir.y = jumpSpeed;
                    }
                                        
                }
                _moveDir.y -= gravity * Time.deltaTime;
                _charC.Move(_moveDir * Time.deltaTime);
            }
        }
    }
}

