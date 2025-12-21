using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Level : MonoBehaviour
{
    public float levelTime;

    private LevelManager _levelManager;
    public void StartLevel(LevelManager levelManager)
    {
        _levelManager = levelManager;
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy(this, _levelManager.player);
        }
    }

    public void ShowMessage(string msg, float duration)
    {
        _levelManager.gameDirector.uiManager.messageUI.ShowMessage(msg, 0, duration);
    }
}
