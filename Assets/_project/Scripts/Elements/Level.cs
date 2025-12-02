using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Level : MonoBehaviour
{
    private LevelManager _levelManager;
    public void StartLevel(LevelManager levelManager)
    {
        _levelManager = levelManager;
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy(this, _levelManager.player);
        }
    }
}
