using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollow2 : MonoBehaviour
{
    public Transform jet; //camera following position (Transform) of player (player linked to actual player)
    public Transform player;
    public Vector3 offset; //Vector3 stores 3 float numbers
    void Update()
    {   //position of camera equal to position of player
        transform.position = jet.position + offset;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}