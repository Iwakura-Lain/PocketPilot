using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Messenger.Broadcast("FirstPieceIsDelivered");//send an event to place 3 other parts
            gameObject.SetActive(false);
        }
    }
}
