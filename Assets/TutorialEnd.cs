using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    public GameObject endMenu;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            endMenu.SetActive(true);
    }
}
