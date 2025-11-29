using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private void Start()
    {
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy(this);
        }
    }
}
