using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action GameEnded = () => { };

    [SerializeField] private BaseWorld _world;
    [SerializeField] private Transform _characterStartPosition;

    private GameState _state = GameState.GameRunning;
    private BaseCharacter _character;
    
    private void Start()
	{
	    _character = GetComponent<CharacterFactory>().CreateCharacter(AppManager.SelectedCharacter);
        _character.transform.position = _characterStartPosition.position;
	    _character.Die += CharacterOnDie;

	    _state = GameState.GameRunning;
	}

    private void Update()
	{
	    switch (_state)
	    {
	        case GameState.GameRunning:
	            _character.CallUpdate(Time.deltaTime);
	            _world.CallUpdate(Time.deltaTime);
	            break;
	        case GameState.GameEnded:
	            if (InputManager.IsContinue())
	            {
	                AppManager.ExitGame();
	            }
	            break;
	    }
	}

    private void FixedUpdate()
    {
        if (_state == GameState.GameRunning)
        {
            _character.CallFixedUpdate(Time.fixedDeltaTime);
            _world.Move(_character.Speed * Time.fixedDeltaTime);
        }
    }

    private void CharacterOnDie()
    {
        GameEnded();
        _state = GameState.GameEnded;
    }
}

public enum GameState
{
    GameRunning,
    GameEnded
}
