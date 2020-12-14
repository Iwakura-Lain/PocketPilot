using System;
using System.Collections;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    private Transform player;

    private Rigidbody rigid;
    private float thrust = 1950;
    private float forceMult = 10;

    public void Start()
    {
        Messenger.Broadcast("OnInitChaser");
        rigid = GetComponent<Rigidbody>();
        /*rigid.mass = 700;
        rigid.drag = 5;
        rigid.angularDrag = 500;*/ //rigidbody setup replaces delayed update. later i would add roll

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(player)
            transform.LookAt(player.position);
    }

    private void FixedUpdate()
    {

        if(rigid)
            rigid.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Colapse") 
            BlowMe();

        if (collision.gameObject.tag == "Player")
            Destroy(collision.gameObject);
    }
    
    private void BlowMe()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Messenger.Broadcast("OnDestroyedChaser");
        Destroy(gameObject);
    }
}