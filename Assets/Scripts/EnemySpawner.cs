using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable] 
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject plane;
    
    [Serializable]
    public class SpawnPositions
    {
        public Transform[] spawnPositions;
    }
    public SpawnPositions[] spawnPositions;
    
    void Start()
    {
        Messenger.AddListener<int>("CargoTaken", Spawn);
        plane = GameObject.FindGameObjectWithTag("Player");
        if (SceneManager.GetActiveScene().name == "Playground")
        {
            Spawn();
            Messenger.AddListener<int>("EnemiesAreDestroyed", Spawn);
        }
    }
    private void Spawn(int setOfPositions = 0)
    {
        var array = spawnPositions[setOfPositions];
         // foreach (Transform sp in array)
         // {
         //     Instantiate(enemyPrefab, sp.position, plane.transform.rotation);
         // }
    }
}