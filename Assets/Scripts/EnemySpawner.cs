using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable] 
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject plane;
    
    [Serializable]
    public class SpawnPositions: IEnumerator,IEnumerable
    {
        public Transform[] spawnPositions;
        private int position = -1;

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return this;
        }
        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < spawnPositions.Length);
        }
        //IEnumerable
        public void Reset()
        {
            position = 0;
        }
        //IEnumerable
        public object Current
        {
            get { return spawnPositions[position];}
        }
    }
    public SpawnPositions[] spawnPositions;
    
    void Start()
    {
        Messenger.AddListener<int>("SpawnEnemies", Spawn);
        plane = GameObject.FindGameObjectWithTag("Player");
    }
    private void Spawn(int setOfPositions)
    {
        print("spawn for " + setOfPositions + " set");
        var array = spawnPositions[setOfPositions];
          foreach (Transform sp in array)
          {
              Instantiate(enemyPrefab, sp.position, plane.transform.rotation);
          }
    }
    
   
}