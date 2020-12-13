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
        Time.timeScale = 1;
        mouseFlightController = GameObject.FindObjectOfType<MouseFlightController>();
        Cursor.lockState = CursorLockMode.Locked;
        statistics.Init();

        mouseFlightController.Init();
    }
}