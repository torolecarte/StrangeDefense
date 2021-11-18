using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Inspector Properties.
    public float FullHealth = 100F;

    // Fields.
    private float _currentHealth;


    void Start()
    {
        _currentHealth = FullHealth;
    }

    void Update()
    {

    }

    // Public Methods.
    public void AddDamage(float damage)
    {
        if (damage <= 0)
            return;

        _currentHealth -= damage;
        Debug.Log($"Current health: {_currentHealth}");
        if (_currentHealth <= 0)
            MakeDead();
    }


    // Private Methods.
    private void MakeDead()
    {
        Debug.Log("You are dead!");
    }
}
