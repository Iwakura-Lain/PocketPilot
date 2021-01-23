using System.Collections;
using System.Collections.Generic;
using MFlight;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private MouseFlightController mouseFlightController;
    [SerializeField] private Statistics statistics;

    private void Start()
    {
        mouseFlightController = GameObject.FindObjectOfType<MouseFlightController>();
        statistics.Init();
        Inventory.Full = false;
        mouseFlightController.Init();
    }
}