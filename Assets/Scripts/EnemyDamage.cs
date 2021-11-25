using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PushBackAxis
{
    Y = 0,
    X = 1
}

public class EnemyDamage : MonoBehaviour
{
    // Inspector Properties.
    public float Damage;
    public float DamageRateInSeconds;
    public float InactiveTime = 0;
    public float PushBackForce = 35;
    public PushBackAxis PushBackAxis = PushBackAxis.Y;

    // Fields.
    private float _nextDamageTime;

    void Start()
    {
        _nextDamageTime = Time.time;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Player" || _nextDamageTime > Time.time)
            return;

        _nextDamageTime = Time.time + DamageRateInSeconds;
        Debug.Log(Time.time.ToString());

        PushBack(other.transform);

        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth.AddDamage(Damage, InactiveTime);
    }

    private void PushBack(Transform pushedObject)
    {
        Vector2 pushDirection;
        //switch (PushBackAxis)
        //{
        //    case PushBackAxis.X:
        //        pushDirection = new Vector2((pushedObject.position.x - transform.position.x), 0).normalized;
        //        break;
        //    case PushBackAxis.Y:
        //    default:
        //        pushDirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized;
        //        break;
        //}
        pushDirection = new Vector2((pushedObject.position.x - transform.position.x), (pushedObject.position.y - transform.position.y)).normalized;
        pushDirection *= PushBackForce;
        Debug.Log($"Pushed back to {pushDirection}");

        Rigidbody2D pushBody = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushBody.velocity = Vector2.zero;

        pushBody.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
