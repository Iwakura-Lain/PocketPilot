using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Messenger.Broadcast("OnEnterRing");
            Destroy(gameObject);
        }
    }
}
