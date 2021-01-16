using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsParty : MonoBehaviour
{
    public void TurnColliderOn()
        {
            GetComponent<CapsuleCollider>().enabled = true;
        }
    
    // Update is called once per frame
    void Update()
    {
        
    } 
            
    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
                {
                    TurnColliderOn();
                }
        }
}