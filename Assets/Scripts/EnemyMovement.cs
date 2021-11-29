using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    // Constants.
    private string ANIM_IS_WALKING = "IsWalking";

    // Inspector Properties.
    public float WalkSpeed = 10f;
    public float ChargeInitDelayTime = 0.25f;
    public float ChargeEndDelayTime = 0.5f;
    public float ChargeAcceleration = 25f;
    public float ChargeSlowdownTime = 10f;
    public float MaxChargeSpeed = 22f;
    public GameObject ObjectToChase;

    // Fields.
    private Rigidbody2D _enemyBody;
    private Transform _enemyTransform;
    private Animator _enemyAC;

    private bool _isCharging = false;
    private float _startChargeTime = 0f;
    private float _endChargeTime = 0f;

    private bool _isAttacking = false;
    private bool _isAttackingRobot = false;
    private bool _isWalking = false;
    private bool _isDiyng = false;

    private bool _canFlip = true;
    private bool _isFacingRight = true;
    private float _flipTimeOffset = 1f;
    private float _nextFlipChance = 0f;
    private string[] _reactToTags = new string[] { "Player", "Robot" };

    // LifeCycle.
    void Start()
    {
        _enemyTransform = this.transform.GetChild(0).transform;
        _enemyBody = GetComponent<Rigidbody2D>();
        _enemyAC = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        //if (Time.time > _nextFlipChance)
        //{
        //    _nextFlipChance = Time.time + _flipTimeOffset;
        //    FlipFacing();
        //}
        if (_isDiyng)
            return;

        if (!_isCharging)
        {
            if (ObjectToChase != null)
            {
                HandleFlippingInColliderDirection(ObjectToChase.transform.GetComponent<Collider2D>());
                SetWalkInTheFacingDirection();
            }
        }

        if (_enemyBody.velocity.x < MaxChargeSpeed
            && (
                (_isCharging && Time.time > _startChargeTime)
                || (!_isCharging && Time.time < _endChargeTime)))
        {
            ChargeInTheFacingDirection();
        }
        else if (!_isCharging && !_isWalking && Time.time > _endChargeTime
            && (_enemyBody.velocity.x > 0 || _enemyBody.velocity.x < 0))
        {
            _enemyBody.velocity = Vector2.Lerp(_enemyBody.velocity, Vector2.zero, ChargeSlowdownTime * Time.deltaTime);
        }

        if (_enemyBody.velocity.x > 0f || _enemyBody.velocity.x < 0f)
            _enemyAC.SetBool(ANIM_IS_WALKING, true);
        else
            _enemyAC.SetBool(ANIM_IS_WALKING, false);
    }


    // Collision Events.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDiyng)
            return;

        if (_reactToTags.Contains(other.tag))
        {
            HandleFlippingInColliderDirection(other);

            _canFlip = false;
            _isCharging = true;
            _startChargeTime = Time.time + ChargeInitDelayTime;

            if (other.tag == "Robot")
                _isAttackingRobot = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_isDiyng)
            return;

        if (_reactToTags.Contains(other.tag))
        {
            HandleFlippingInColliderDirection(other);
            //_isAttacking = true;
            _isCharging = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isDiyng)
            return;

        if (_reactToTags.Contains(other.tag))
        {
            _isCharging = false;
            _isAttacking = false;
            _endChargeTime = Time.time + ChargeEndDelayTime;

            //_enemyBody.velocity = Vector2.zero;

            //if (ObjectToChase != null && _enemyBody)
            //{
            //    HandleFlippingInColliderDirection(ObjectToChase.transform.GetComponent<Collider2D>());
            //    SetWalkInTheFacingDirection();
            //}
        }

        if (other.tag == "Player" && !_isAttackingRobot)
        {
            _canFlip = true;
        }
    }


    // Public Methods.
    public void SetIsDying()
    {
        _isDiyng = true;
        _isWalking = false;
        _isAttacking = false;
        _isCharging = false;
        _enemyAC.SetBool(ANIM_IS_WALKING, false);
        _enemyBody.velocity = Vector2.zero;
    }

    // Private Methods.
    private void HandleFlippingInColliderDirection(Collider2D other)
    {
        if (_isFacingRight && _enemyTransform.position.x > other.transform.position.x)
            FlipFacing();
        else if (!_isFacingRight && _enemyTransform.position.x < other.transform.position.x)
            FlipFacing();
    }
    private void FlipFacing()
    {
        if (!_canFlip)
            return;

        this.transform.localScale = new Vector3(
            (this.transform.localScale.x * -1),
            this.transform.localScale.y,
            this.transform.localScale.z);

        _isFacingRight = !_isFacingRight;
    }
    private void ChargeInTheFacingDirection()
    {
        _isWalking = false;
        if (!_isFacingRight)
            //_enemyBody.AddForce(new Vector2(-1f, 0) * ChargeAcceleration);
            _enemyBody.velocity = new Vector2(-1f, 0) * ChargeAcceleration;
        else if (_isFacingRight)
            //_enemyBody.AddForce(new Vector2(1f, 0) * ChargeAcceleration);
            _enemyBody.velocity = new Vector2(1f, 0) * ChargeAcceleration;
    }
    private void SetWalkInTheFacingDirection()
    {
        _isWalking = true;
        if (!_isFacingRight)
            _enemyBody.velocity = new Vector2(-WalkSpeed, _enemyBody.velocity.y);
        else if (_isFacingRight)
            _enemyBody.velocity = new Vector2(WalkSpeed, _enemyBody.velocity.y);
    }
}
