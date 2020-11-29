using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject plane;
    public Transform[] SpawnPositions;
    void Start()
    {
        Messenger.AddListener("CargoTaken", Spawn);
        plane = GameObject.FindGameObjectWithTag("Player");
    }
    private void Spawn()
    {
        foreach (var spawnPosition in SpawnPositions)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition.position, plane.transform.rotation);
            newEnemy.GetComponent<Chaser>().Init();
        }

    }
}