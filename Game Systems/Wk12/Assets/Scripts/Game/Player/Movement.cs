using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
namespace Player
{
    [AddComponentMenu("Game System RPG/Player/Movement")]
    //telling the object that the script needs the CharacterController
    //Assigns the CharacterController to the object this script is on
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        private CharacterController _charC;
        [SerializeField] private Vector3 _moveDir = Vector3.zero;
        [Space(25),Header("Speeds")]
        public float speed = 5f;
        public float  gravity = 20f, jumpSpeed = 8f;

        [SerializeField] private Transform player;

        void Start()
        {
            Debug.Log("In Game - Loading player into current position");
            player.position = new Vector3(GameData.gameData.playerPosition[0], GameData.gameData.playerPosition[1],
                GameData.gameData.playerPosition[2]);
            player.eulerAngles = new Vector3(GameData.gameData.playerRotation[0], GameData.gameData.playerRotation[1],
                GameData.gameData.playerRotation[2]);
            
            Debug.Log("RealPos: " + player.position + " RealRot: " + player.eulerAngles);
            
            string posRotArray = "SavePos: (";
            for (int x = 0; x < GameData.gameData.playerPosition.Length; ++x)
            {
                posRotArray += GameData.gameData.playerPosition[x];
                if(x < GameData.gameData.playerPosition.Length -1){posRotArray += ", ";}
            }
            posRotArray += ")  Rotation: (";
            for (int x = 0; x < GameData.gameData.playerRotation.Length; ++x)
            {
                posRotArray += GameData.gameData.playerRotation[x];
                if(x < GameData.gameData.playerRotation.Length -1){posRotArray += ", ";}
            }
            posRotArray += ")";
            Debug.Log(posRotArray);
            
            
            _charC = this.GetComponent<CharacterController>();
            Debug.Log("_CharC: " + _charC.transform.position + " , " + _charC.transform.rotation);
        }
        void Update()
        {
            if (GameManager.Instance.gameState == GameState.Alive)
            {
                if (_charC.isGrounded)
                {
                    //_moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))* speed);
                    _moveDir = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
                    _moveDir = transform.TransformDirection(_moveDir);
                    _moveDir *= speed;

                    // Tried to check for a 'jump' input from the key bind array
                    // by changing from GetButton("Jump") to GetKeyDown(KeyBinder.keys["Jump"])
                    // to demonstrate keybinding works. But GetKeyDown only worked on every 10-20 presses of space
                    if (Input.GetButton("Jump")) 
                    {
                        //Debug.Log("Apparently getting keycode: " + KeyBinder.keys["Jump"]);
                        _moveDir.y = jumpSpeed;
                    }
                }
                _moveDir.y -= gravity * Time.deltaTime;
                _charC.Move(_moveDir*Time.deltaTime);
            }
        }
    }
}
