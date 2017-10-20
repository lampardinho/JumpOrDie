using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    private float _groundHorizontalLength;

    private void Start()
    {
        _groundHorizontalLength = transform.position.x;
    }

    public void Move(float value)
    {
        transform.position += Vector3.left * value;
        if (transform.position.x < -_groundHorizontalLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffSet = new Vector2(_groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffSet;
    }
}
