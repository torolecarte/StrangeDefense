using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Constants.
    private string ANIM_IS_DEAD = "IsDead";

    // Inspector Properties.
    public float FullHealth = 1;
    public float DamageTakeOnWeakSpot = 1;
    public float NextDamageRate = 1f;
    public float DelayBeforeDestroy = 1f;
    public GameObject DeathFX;

    // Fields.
    private float _currentHealth;
    private Animator _enemyAC;
    private Rigidbody2D _enemyBody;
    private float _nextDamageTime;


    // Life Cycle.
    void Start()
    {
        _nextDamageTime = Time.time;
        _currentHealth = FullHealth;
        _enemyBody = GetComponentInParent<Rigidbody2D>();
        _enemyAC = GetComponentInParent<Animator>();
    }
    void Update()
    {

    }

    // Collision Events.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;

        if (Time.time > _nextDamageTime)
            AddDamage();
    }


    // Private Methods.
    private void AddDamage()
    {
        _currentHealth -= DamageTakeOnWeakSpot;
        _nextDamageTime = Time.time + NextDamageRate;
        if (_currentHealth <= 0)
            MakeDead();
    }

    private void MakeDead()
    {
        GetComponentInParent<EnemyMovement>().SetIsDying();
        _enemyAC.SetBool(ANIM_IS_DEAD, true);
        Instantiate(DeathFX, transform.root.transform);
        Destroy(transform.root.gameObject, DelayBeforeDestroy);
    }
}
