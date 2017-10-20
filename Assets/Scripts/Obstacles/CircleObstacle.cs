using UnityEngine;

public class CircleObstacle : BaseObstacle 
{
    public override void Move(float value)
    {
        Vector2 direction = Vector2.left * value * 2 + Physics2D.gravity * Time.fixedDeltaTime;
        Rigidbody.MovePosition(Rigidbody.position + direction);
    }
}
