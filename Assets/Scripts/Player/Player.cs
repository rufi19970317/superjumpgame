using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 5f;
    float jumpForce = 5f;

    Transform groundCheck;
    float groundCheckRadius = 0.1f;
    LayerMask groundLayer;

    Rigidbody2D _rb;

    IPlayerState _state;
    int _currentMoveDir;

    public int CurrentMoveDir => _currentMoveDir;
    public float VerticalVelocity => _rb.velocity.y;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        InputManager.OnMoveInput += OnMoveInput;
        InputManager.OnJumpInput += OnJumpInput;
    }

    void OnDisable()
    {
        InputManager.OnMoveInput -= OnMoveInput;
        InputManager.OnJumpInput -= OnJumpInput;
    }

    void Start()
    {
        ChangeState(new PlayerIdleState(this));
    }

    void Update()
    {
        _state?.Update();
    }

    void FixedUpdate()
    {
        ApplyHorizontalMove();
    }

    void OnMoveInput(int dir)
    {
        _state?.HandleMoveInput(dir);
    }

    void OnJumpInput()
    {
        _state?.HandleJumpInput();
    }

    public void ChangeState(IPlayerState newState)
    {
        _state?.Exit();
        _state = newState;
        _state.Enter();
    }

    public void SetMoveDir(int dir)
    {
        _currentMoveDir = dir;
    }

    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool IsGrounded()
    {
        if (groundCheck == null) return false;

        Collider2D col = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
        return col != null;
    }

    void ApplyHorizontalMove()
    {
        float targetVX = _currentMoveDir * moveSpeed;
        _rb.velocity = new Vector2(targetVX, _rb.velocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if(groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
