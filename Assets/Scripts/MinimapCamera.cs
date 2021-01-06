using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapCamera : MonoBehaviour
{
    public Transform player; //camera following position (Transform) of player (player linked to actual player)
    public Vector3 offset; //Vector3 stores 3 float numbers

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        if (player)
        {
            transform.position = player.position + offset;
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}


