using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Inspector Properties.
    public float Damage;
    public float DamageRateInSeconds;
    public float PushBackForce;

    // Fields.
    private float _nextDamage;

    void Start()
    {
        _nextDamage = Time.time;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Player" || _nextDamage > Time.time)
            return;

        _nextDamage = Time.time + DamageRateInSeconds;
        Debug.Log(Time.time.ToString());

        PushBack(other.transform);

        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth.AddDamage(Damage);
    }

    private void PushBack(Transform pushedObject)
    {
        Vector2 pushDirection = new Vector2(0, (pushedObject.position.y - transform.position.y));
        pushDirection *= PushBackForce;

        Rigidbody2D pushBody = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushBody.velocity = Vector2.zero;

        pushBody.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
