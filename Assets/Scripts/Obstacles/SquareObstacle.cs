using UnityEngine;

public class SquareObstacle : BaseObstacle 
{
    private void OnEnable()
    {
        Rigidbody.gravityScale = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody.gravityScale = 10;
    }
}
