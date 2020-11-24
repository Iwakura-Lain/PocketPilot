using System.Collections;
using System.Collections.Generic;
using MFlight;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private MouseFlightController mouseFlightController;
    [SerializeField] private Statistics statistics;
    [SerializeField] private Chaser[] chasers;
    [SerializeField] private Plane _plane;

    private void Start()
    {
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        statistics.Init();

        foreach (var chaser in chasers) chaser.Init();
        //_plane.Init();
        mouseFlightController.Init();
    }
}