using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public MainMenu mainMenu;
    public VictoryUI victoryUI;
    public FailUI failUI;


    //ShowHideMenu
    public void ShowMainMenu()
    {
        mainMenu.Show();
        victoryUI.Hide();
        failUI.Hide();
    }
    public void ShowVictoryUI(float delay)
    {
        victoryUI.Show(delay);
    }
    public void ShowFailUI(float delay)
    {
        failUI.Show(delay);
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
