using UnityEngine;

public class GameCharacterView : MonoBehaviour 
{
    private const float ColorLerpSpeed = 5;

    [SerializeField] private Color _jumpColor = Color.black;

    private BaseCharacter _character;
    private float _colorLerper = 0;
    private Color _defaultColor;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _character = GetComponent<BaseCharacter>();
        _renderer = GetComponent<SpriteRenderer>();
        _defaultColor = _renderer.color;
    }

    public void CallUpdate(float dt)
    {
        if (!_character.IsOnGround)
        {
            _colorLerper += dt * ColorLerpSpeed;
        }
        else
        {
            _colorLerper -= dt * ColorLerpSpeed;
        }

        _colorLerper = Mathf.Clamp01(_colorLerper);
        _renderer.color = Color.Lerp(_defaultColor, _jumpColor, _colorLerper);
    }
}
