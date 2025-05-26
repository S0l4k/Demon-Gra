using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCount : MonoBehaviour
{
    public static DeathCount Instance { get; private set; }

    public int enemiesDefeated = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log("Zabito przeciwników: " + enemiesDefeated);
    }

    public int GetDefeatedEnemies()
    {
        return enemiesDefeated;
    }
}
