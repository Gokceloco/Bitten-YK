using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public TimerUI timerUI;

    private float _totalLevelTime;
    private float _remainingTime;

    private bool _started;

    public void RestartTimerManager(float levelTime)
    {
        print(levelTime);
        _totalLevelTime = levelTime;
        _remainingTime = levelTime;
        _started = true;
    }

    private void Update()
    {
        if (gameDirector.gameState != GameState.GamePlay || !_started)
        {
            return;
        }
        _remainingTime -= Time.deltaTime;

        timerUI.SetFillBar(_remainingTime / _totalLevelTime, Mathf.CeilToInt(_remainingTime));

        if (_remainingTime < 0) 
        {
            gameDirector.LevelFailed(2);
            gameDirector.player.DieWithTimer();
            gameDirector.uiManager.messageUI.ShowMessage(
                "<color=\"red\">TIME <color=#005500><color=\"yellow\">RAN OUT! <color=#005500>", 0, 3);
        }
    }
}
