using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Normal,
        LowHP,
        Sleep
    }
    [SerializeField] private State _state;
    [SerializeField] private Enemy _enemy;

    //Keep track of which state we are in
    //Start
    //-----------> NextState()
    //----------------> NormalState()
    //----------------> CombatState()


    // Start is called before the first frame update
    void Start()
    {
        _state = State.Normal;
        NextState();

    }

    private void NextState()
    {
        switch(_state)
        {
            case State.Normal:
                NormalState();
                break;

            case State.LowHP:
                LowHPState();
                break;

            case State.Sleep:
                SleepState();
                break;
        }
    }

    private IEnumerator NormalState()
    {
        Debug.Log(message:"Enter Normal State");

        while(_state == State.Normal)
        {
            if(_enemy.CurrentHealth() < 30)
            {
                _state = State.LowHP;
            }

            yield return null;
        }

        Debug.Log(message:"Exit Normal State");
        NextState();
    }

     private IEnumerator LowHPState()
    {
        Debug.Log(message:"Enter LowHP State");

        while(_state == State.LowHP)
        {
            if(_enemy.CurrentHealth() > 30)
            {
                _state = State.Normal;
            }

            yield return null;
        }

        Debug.Log(message:"Exit LowHP State");
    }

     private IEnumerator SleepState()
    {
        Debug.Log(message:"Enter Sleep State");
        
        while(_state == State.Sleep)
        {
         
            yield return null;
        }

        Debug.Log(message:"Exit Sleep State");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
