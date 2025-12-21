using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public MainMenu mainMenu;
    public VictoryUI victoryUI;
    public FailUI failUI;
    public MessageUI messageUI;
    public TimerUI timerUI;

    //ShowHideMenu
    public void ShowMainMenu()
    {
        mainMenu.Show();
        victoryUI.Hide();
        failUI.Hide();        
        HideIngameUI();
        messageUI.Hide();
    }
    public void ShowVictoryUI(float delay)
    {
        victoryUI.Show(delay);
        HideIngameUI();
    }
    public void ShowFailUI(float delay)
    {
        failUI.Show(delay);
        HideIngameUI();
    }

    public void ShowIngameUI()
    {
        timerUI.Show();
    }
    public void HideIngameUI()
    {
        timerUI.Hide();        
    }


    //ButtonPressed
    public void StartGameButtonPressed()
    {
        mainMenu.Hide();
        gameDirector.RestartLevel();
    }
    public void LoadNextLevelButtonPressed()
    {
        victoryUI.Hide();
        gameDirector.LoadNextLevel();
    }
    public void RetryButtonPressed()
    {
        failUI.Hide();
        gameDirector.RestartLevel();
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
