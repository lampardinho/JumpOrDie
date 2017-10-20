using UnityEngine;
using UnityEngine.SceneManagement;

public static class AppManager
{
    private const int TargetFPSValue = 30;

    public static CharacterType SelectedCharacter { get; set; }
    public static SceneType SelectedWorld { get; set; }

    static AppManager()
    {
        Application.targetFrameRate = TargetFPSValue;
    }

    public static void CreateGame()
    {
        SceneManager.LoadScene((int) SelectedWorld);
    }

    public static void ExitGame()
    {
        SceneManager.LoadScene((int)SceneType.Menu);
    }
}

public enum CharacterType
{
    Circle,
    Square,
    Triangle
}

public enum SceneType
{
    Menu = 0,
    Woods = 1,
    Mountains = 2
}
