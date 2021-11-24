using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Inspector Properties.
    public float MaxSpeed = 5;
    public float JumpPower = 10;
    public LayerMask GroundLayer;
    public LayerMask DangerGroundLayer;
    public Transform GroundCheck;

    // Constants.
    private const string ANIM_IS_GROUNDED = "IsGrounded";
    private const string ANIM_VERTICAL_VELOCITY = "VerticalVelocity";
    private const string ANIM_MOVE_SPEED = "MoveSpeed";
    private const float _groundCheckRadius = 0.1f;

    // Components.
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;

    // Flags.
    private bool _facingRight = true;
    private bool _isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    // Movements.
    public void Move()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        //if (_isGrounded)
        //{
        //    _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        //    _animator.SetFloat(ANIM_MOVE_SPEED, 0);
        //    return;
        //}

        if (moveDirection > 0 && !_facingRight) FlipAnimation();
        else if (moveDirection < 0 && _facingRight) FlipAnimation();

        _rigidBody.velocity = new Vector2(moveDirection * MaxSpeed, _rigidBody.velocity.y);
        _animator.SetFloat(ANIM_MOVE_SPEED, Mathf.Abs(moveDirection));
    }
    public void Jump()
    {
        if (_isGrounded && Input.GetAxis("Jump") > 0)
        {
            _animator.SetBool(ANIM_IS_GROUNDED, false);
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            _rigidBody.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
            _isGrounded = false;
            return;
        }

        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundCheckRadius, GroundLayer);
        bool isInDangerGround = Physics2D.OverlapCircle(GroundCheck.position, _groundCheckRadius, DangerGroundLayer);
        bool shouldAnimateGrounded = _isGrounded || isInDangerGround;
        _animator.SetBool(ANIM_IS_GROUNDED, shouldAnimateGrounded);
        _animator.SetFloat(ANIM_VERTICAL_VELOCITY, _rigidBody.velocity.y);
    }

    // Helpers.
    private void FlipAnimation()
    {
        _facingRight = !_facingRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
