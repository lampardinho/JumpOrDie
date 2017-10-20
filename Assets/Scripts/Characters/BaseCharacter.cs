using System;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour 
{
    protected const float SpeedLinearIncreaseTime = 10;
    protected const float MaxSpeed = 10;
    private const int JumpForce = 300;
    private const float CollisionTollerance = 0.1f;

    public event Action Die = () => { };

    public bool IsOnGround { get; private set; }
    public float Speed { get; protected set; }

    private bool _shouldJumpOnNextFixedUpdate;
    private GameCharacterView _characterView;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _characterView = GetComponent<GameCharacterView>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void CallUpdate(float dt)
    {
        _characterView.CallUpdate(dt);

        if (InputManager.IsJump())
        {
            _shouldJumpOnNextFixedUpdate = true;
        }
    }

    protected virtual void UpdateSpeed(float dt)
    {
        float incValue = MaxSpeed / SpeedLinearIncreaseTime;
        Speed += incValue * dt;
        Speed = Mathf.Clamp(Speed, 0, MaxSpeed);
    }

    public void CallFixedUpdate(float dt)
    {
        UpdateSpeed(dt);

        if (_shouldJumpOnNextFixedUpdate && IsOnGround)
        {
            _rigidbody.AddForce(transform.up * JumpForce);
            IsOnGround = false;
        }

        _shouldJumpOnNextFixedUpdate = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bool collidedBelow = Mathf.Abs(collision.otherCollider.bounds.min.y - collision.collider.bounds.max.y) < CollisionTollerance;

        if (!collidedBelow)
        {
            IsOnGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;

        bool collideRightSide = contactPoint.x > collision.otherCollider.bounds.center.x;
        bool collidedBelow = Mathf.Abs(collision.otherCollider.bounds.min.y - collision.collider.bounds.max.y) < CollisionTollerance;

        if (!collidedBelow && collideRightSide)
        {
            Die();
        }

        if (collidedBelow)
        {
            IsOnGround = true;
        }
    }
}
