using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public AudioClip ding;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Messenger.Broadcast("OnEnterRing");
            AudioSource.PlayClipAtPoint(ding, transform.position);
            Destroy(gameObject);
        }
    }
}
