using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Normal,
        Angry,
        LowHP,
        Death,
        Sleep
    }

    [SerializeField] private State state;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TurnTimer turnTimer;
    public Animator animPlayer;
    public Animator animEnemy;
    public SpriteRenderer mushroomSprite;
    [SerializeField] private Text stateText; 
    
    private void Start()
    {
        NextState();
    }

    private void NextState()
    {
        switch(state)
        {
            case State.Normal:
                stateText.text = "in a bad mood";
                StartCoroutine(NormalState());
                break;
            case State.Angry:
                stateText.text = "super angry, bro";
                StartCoroutine(AngryState());
                break;
            case State.LowHP:
                animEnemy.Play("idle");
                stateText.text = "takin' a breather";
                 StartCoroutine(LowHPState());
                break;
            case State.Death:
                stateText.text = "dead as, bro";
                 StartCoroutine(DeathState());
                break;
            case State.Sleep:
                stateText.text = "sleeping?!";
                StartCoroutine(SleepState());
                break;
            
        }
    }

    private IEnumerator NormalState()
    {
        Debug.Log("I'm fine. I'm always like this.");
        
        while(state == State.Normal)
        {
            if(enemyHealth.CurrentHealth() > 50 && enemyHealth.CurrentHealth() <= 70)
            {
                state = State.Angry;
                yield return null;
                continue;
            }
            else if(enemyHealth.CurrentHealth() < 30)
            {
                state = State.LowHP;
                yield return null;
                continue;
            }
            
            if(!turnTimer.IsNextTurn())
            {
                yield return null;
                continue;
            }
            
            animEnemy.SetTrigger("melee");
            if(Random.Range(0f,1f) < .95f)
            {
                int damage = Random.Range(1, 6);
                playerHealth.DealDamage(damage);
                animPlayer.SetTrigger("hurt");
            }

            turnTimer.ResetTimer();
            yield return null;//wait a single frame
        }
        Debug.Log("I'm getting moody!");
        NextState();
    }
    
    private IEnumerator AngryState()
    {
        Debug.Log("George is getting angry!");
        
        while(state == State.Angry)
        {
            if((enemyHealth.CurrentHealth() > 30 && enemyHealth.CurrentHealth() < 40) || enemyHealth.CurrentHealth() > 70)
            {
                state = State.Normal;
                yield return null;
                continue;
            }
            else if(enemyHealth.CurrentHealth() < 30)
            {
                state = State.LowHP;
                yield return null;
                continue;
            }
            
            if(!turnTimer.IsNextTurn())
            {
                yield return null;
                continue;
            }
            
            animEnemy.SetTrigger("range");
            int damage = Random.Range(0, 26);
            playerHealth.DealDamage(damage);
            
            if (damage > 0)
            {
                animPlayer.SetTrigger("hurt");
            }
            
            turnTimer.ResetTimer();
            
            yield return null;
        }
        
        Debug.Log("The angry is being channeled into...");
        NextState();
    }

    private IEnumerator LowHPState()
    { 
        Debug.Log("Is that your leg over there?");

        while(state == State.LowHP)
        {
            if(enemyHealth.CurrentHealth() > 75)
            {
                mushroomSprite.color = Color.white;
                state = State.Angry;
                yield return null;
                continue;
            }
            else if (enemyHealth.CurrentHealth() < 1)
            {
                mushroomSprite.color = Color.red;
                state = State.Death;
                yield return null;
                continue;
            }
            
            if(!turnTimer.IsNextTurn())
            {
                yield return null;
                continue;
            }

            mushroomSprite.color = new Color(204, 170, 256);
            
            enemyHealth.Heal();
            turnTimer.ResetTimer();

            yield return null;
        }
        Debug.Log("T'was just a flesh wound!");
        NextState();
    }
    
    private IEnumerator DeathState()
    {
        Debug.Log("Enemy start dying");
        while(state == State.Death)
        {
            yield return null;
        }
        Debug.Log("Enemy dead.");
        NextState();
    }

    private IEnumerator SleepState()
    {
        Debug.Log("Sleepy Monster");
        while(state == State.Sleep)
        {


            yield return null;
        }
        Debug.Log("Waking Up!");
        NextState();
    }
}
