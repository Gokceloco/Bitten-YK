using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public Player player;
    public int levelNo;
    public List<Level> levels;

    private Level _currentLevel;

    public void StartLevel()
    {
        DeletePreviousLevel();
        CreateNewLevel();
    }

    public float GetCurrentLevelTime()
    {
        return _currentLevel.levelTime;
    }

    private void CreateNewLevel()
    {
        levelNo = Mathf.Clamp(levelNo, 1, levels.Count);

        _currentLevel = Instantiate(levels[levelNo-1]);
        _currentLevel.StartLevel(this);
    }

    private void DeletePreviousLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }
    }
}
 