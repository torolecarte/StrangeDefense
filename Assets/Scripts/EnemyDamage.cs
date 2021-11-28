using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum PushBackAxis
{
    Y = 0,
    X = 1
}

public class EnemyDamage : MonoBehaviour
{
    // Constants.
    private string ANIM_IS_ATTACKING = "IsAttacking";

    // Inspector Properties.
    public float Damage;
    public float DamageRateInSeconds;
    public float InactiveTime = 0;
    public float PushBackForce = 35;
    public PushBackAxis PushBackAxis = PushBackAxis.Y;

    // Fields.
    private float _nextDamageTime;
    private Animator _enemyAC;
    private string[] _reactToTags = new string[] { "Player", "Robot" };

    // Life Cycle.
    void Start()
    {
        _nextDamageTime = Time.time;
        _enemyAC = GetComponentInParent<Animator>();
    }

    // Collision Events.
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleAttackingAnimation(other, true);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Robot")
        {
            HandleAttackingAnimation(other, true);
        }

        if (!_reactToTags.Contains(other.tag) || _nextDamageTime > Time.time)
            return;

        _nextDamageTime = Time.time + DamageRateInSeconds;
        Debug.Log(Time.time.ToString());

        PushBack(other.transform);

        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth.AddDamage(Damage, InactiveTime);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        HandleAttackingAnimation(other, false);
    }

    // Private Methods.
    private void HandleAttackingAnimation(Collider2D other, bool setTo)
    {
        if (_enemyAC != null && _reactToTags.Contains(other.tag))
        {
            _enemyAC.SetBool(ANIM_IS_ATTACKING, setTo);
        }
    }
    private void PushBack(Transform pushedObject)
    {
        Vector2 pushDirection;
        switch (PushBackAxis)
        {
            case PushBackAxis.X:
                pushDirection = new Vector2((pushedObject.position.x - transform.position.x), (pushedObject.position.y - transform.position.y)).normalized;
                pushDirection *= PushBackForce;
                break;
            case PushBackAxis.Y:
            default:
                pushDirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized;
                pushDirection *= PushBackForce;
                break;
        }
        Debug.Log($"Pushed back to {pushDirection}");

        Rigidbody2D pushBody = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushBody.velocity = Vector2.zero;

        pushBody.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
