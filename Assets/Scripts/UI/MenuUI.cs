using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _gameStartButton;
    [SerializeField] private ToggleGroup _characterSelectionToggleGroup;
    [SerializeField] private ToggleGroup _worldSelectionToggleGroup;
    
	private void Start () 
    {
        _gameStartButton.onClick.AddListener(AppManager.CreateGame);

        AppManager.SelectedCharacter = _characterSelectionToggleGroup.GetActive().GetComponent<MenuCharacterView>().Type;
        foreach (MenuCharacterView toggle in _characterSelectionToggleGroup.GetComponentsInChildren<MenuCharacterView>())
        {
            toggle.Selected += type => AppManager.SelectedCharacter = type;
        }

        AppManager.SelectedWorld = _worldSelectionToggleGroup.GetActive().GetComponent<MenuWorldView>().Type;
        foreach (MenuWorldView toggle in _worldSelectionToggleGroup.GetComponentsInChildren<MenuWorldView>())
        {
            toggle.Selected += type => AppManager.SelectedWorld = type;
        }
	}
}
