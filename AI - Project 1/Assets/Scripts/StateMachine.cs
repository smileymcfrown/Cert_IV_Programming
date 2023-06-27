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
    [SerializeField] private TurnTimer _turnTimer;

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
        Debug.Log(message:"You be dying.");

        while(_state == State.LowHP)
        {
            if(!_turnTimer.IsNextTurn())
            {
                yield return null;
                continue;
            }

            _enemy.Heal();
            _turnTimer.ResetTimer();
            if(_enemy.CurrentHealth() > 80)
            {
                _state = State.Normal;
            }
            yield return null;

            if(_enemy.CurrentHealth() > 30)
            {
                _state = State.Normal;
            }
            Debug.Log("Exit LowHP State");
            NextState();
        }

        Debug.Log(message:"Exit LowHP State");
    }

     private IEnumerator SleepState()
    {
        Debug.Log(message:"Going to sleep...");
        
        while(_state == State.Sleep)
        {
         
            yield return null;
        }

        Debug.Log(message:"... waking up!");
        NextState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
