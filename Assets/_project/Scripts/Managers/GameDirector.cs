using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameState gameState;
    public LevelManager levelManager;
    public FXManager fxManager;
    public Player player;
    public AudioManager audioManager;
    public TimerManager timerManager;
    public UIManager uiManager;

    private void Start()
    {
        uiManager.ShowMainMenu();
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
        Invoke(nameof(ChangeGameStateToGameplay), .1f);
        levelManager.StartLevel();
        timerManager.RestartTimerManager(levelManager.GetCurrentLevelTime());
        player.RestartPlayer();
        audioManager.PlayAmbientAS();
        if (levelManager.levelNo == 1)
        {
            uiManager.messageUI.ShowMessage("WASD TO MOVE!", 0, 2);
            uiManager.messageUI.ShowMessage("FIND THE POTION BEFORE TIME RUNS OUT!", 3, 2);
        }
        uiManager.ShowIngameUI();
    }

    void ChangeGameStateToGameplay()
    {
        gameState = GameState.GamePlay;
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
        uiManager.ShowVictoryUI(2);
        audioManager.PlayVictoryAS();
        audioManager.StopAmbientAS();
    }

    public void LevelFailed(float uiDelay)
    {
        gameState = GameState.Lose;
        uiManager.ShowFailUI(uiDelay);
        audioManager.PlayFailAS();
        audioManager.StopAmbientAS();
    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    Win,
    Lose,
}