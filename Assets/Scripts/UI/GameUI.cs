using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour 
{
    [SerializeField] private GameManager _manager;
    [SerializeField] private Text _gameEndedText;

    private void Start()
    {
        _manager.GameEnded += () => _gameEndedText.gameObject.SetActive(true);
    }
}
