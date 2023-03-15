using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;

    [SerializeField] private Bar healthBar;

    public void Heal()
    {
        int heal = Random.Range(20,30);

        _health = Mathf.Min(_health + heal, _maxHealth);
    }

    public int CurrentHealth()
    {
        return _health;
    }

    public void UpdateHealthBar()
    {
        healthBar.SetBar((float) _health, (float) _maxHealth);
    }

}
