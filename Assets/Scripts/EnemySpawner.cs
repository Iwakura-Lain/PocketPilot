using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject plane;
    public Transform[] SpawnPositions;
    void Start()
    {
        Messenger.AddListener("CargoTaken", Spawn);
        plane = GameObject.FindGameObjectWithTag("Player");
        if (SceneManager.GetActiveScene().name == "Playground")
        {
            Spawn();
            Messenger.AddListener("EnemiesAreDestroyed", Spawn);
        }
    }
    private void Spawn()
    {
        foreach (var spawnPosition in SpawnPositions)
        {
           Instantiate(enemyPrefab, spawnPosition.position, plane.transform.rotation);
        }
    }
}