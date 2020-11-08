using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private float currentDestroyedCount;
    private float totalCount;

    private void Start()
    {
        Chaser.OnDestroyed += UpdateStatistics;
        Chaser.OnInit += AddToTotalCount;
    }

    private void AddToTotalCount()
    {
        totalCount++;
    }

    private void UpdateStatistics()
    {
        currentDestroyedCount++;
    }

    private void Spawn()
    {
        if (totalCount < currentDestroyedCount) Instantiate(Enemy, transform.position, Quaternion.identity);
    }
}