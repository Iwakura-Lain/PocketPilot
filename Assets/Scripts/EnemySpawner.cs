using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float currentDestroyedCount = 0;
    float totalCount = 0;
    public GameObject Enemy;
    void Start()
    {
        Chaser.OnDestroyed += UpdateStatistics;
        Chaser.OnInit += AddToTotalCount;
    }

    void AddToTotalCount()
    {
        totalCount++;
    }

    void UpdateStatistics()
    {
        currentDestroyedCount++;
    }

    void Spawn()
    {
        if(totalCount < currentDestroyedCount)
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
        }
    }
}
