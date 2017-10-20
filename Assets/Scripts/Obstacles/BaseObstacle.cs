using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour
{
    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Rigidbody.velocity = Vector2.zero;
    }

    public virtual void Move(float value)
    {
        Vector2 direction = Vector2.left * value;
        Rigidbody.MovePosition(Rigidbody.position + direction);
    }
}
