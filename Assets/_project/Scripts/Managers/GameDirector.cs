using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameState gameState;
    public LevelManager levelManager;
    public Player player;

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartLevel();
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

    public void StartLevel() 
    {
        gameState = GameState.GamePlay;
        levelManager.StartLevel();
        player.RestartPlayer();
    }
    public void LoadNextLevel()
    {
        levelManager.levelNo++;
        StartLevel();
    }
    public void LoadPreviousLevel()
    {
        levelManager.levelNo--;
        StartLevel();
    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    Win,
    Lose,
}