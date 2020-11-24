using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNewRoom : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPositions;
    public Animator CloseDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var spawnPosition in spawnPositions)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);
                newEnemy.GetComponent<Chaser>().Init();
            }

            CloseDoor.enabled = true;
        }
    }
}
