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
    public bool isWaypointNotNeedeAmynore;
    void Start()
    {
        Messenger.AddListener("CargoTaken", ChangeWaypoint);
        Messenger.AddListener("EnemiesAreDestroyed", EnableWaypoint);
        Messenger.AddListener("FirstPieceIsDelivered", DisableWaypoint);
    }

    private void EnableWaypoint()
    {
        if(!isWaypointNotNeedeAmynore)
            waypoint.enabled = text1.enabled = text2.enabled = true;//here bug
    }
    private void ChangeWaypoint()
    {
        text2.text = "Deliver the cargo";
        waypoint.enabled = text1.enabled = text2.enabled = false;
    }
    private void DisableWaypoint()
    {
        waypoint.enabled = text1.enabled = text2.enabled = false;
        isWaypointNotNeedeAmynore = true;
    }

    private void OnTriggerEnter(Collider other) //for doors
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider>().enabled = false;
            EnableWaypoint();
        }
    }
}
