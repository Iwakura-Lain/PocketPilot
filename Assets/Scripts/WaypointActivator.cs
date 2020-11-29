using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointActivator : MonoBehaviour
{
    public Image waypoint;
    public Text text1;
    public Text text2;

    void Start()
    {
        Messenger.AddListener("CargoTaken", DisableWaypoint);
        Messenger.AddListener("EnemiesAreDestroyed", EnableWaypoint);
    }

    private void EnableWaypoint()
    {
        waypoint.enabled = text1.enabled = text2.enabled = true;
    }
    private void DisableWaypoint()
    {
        text2.text = "Deliver the cargo";
        waypoint.enabled = text1.enabled = text2.enabled = false;

    }

    private void OnTriggerEnter(Collider other) //for doors
    {
        if (other.gameObject.tag == "Player")
            EnableWaypoint();
    }
}
