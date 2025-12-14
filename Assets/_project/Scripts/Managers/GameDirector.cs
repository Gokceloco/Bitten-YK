using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameState gameState;
    public LevelManager levelManager;
    public FXManager fxManager;
    public Player player;

    private void Start()
    {
        RestartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            LoadPreviousLevel();
        }
    }

    public void RestartLevel() 
    {
        gameState = GameState.GamePlay;
        levelManager.StartLevel();
        player.RestartPlayer();
    }
    public void LoadNextLevel()
    {
        levelManager.levelNo++;
        RestartLevel();
    }
    public void LoadPreviousLevel()
    {
        levelManager.levelNo--;
        RestartLevel();
    }

    public void LevelCompleted()
    {
        gameState = GameState.Win;
        Invoke(nameof(LoadNextLevel), 2f);
    }

    public void LevelFailed()
    {
        gameState = GameState.Lose;
        Invoke(nameof(RestartLevel), 2f);
    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    Win,
    Lose,
}