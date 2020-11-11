using System.Collections;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public delegate void DestroyedAction();
    public delegate void InitialAction();

    public GameObject ExplosionPrefab;

    private Transform player;
    
    private Rigidbody rigid;
    public float thrust = 55;
    public float forceMult = 1000f;
    private void Start()
    {
        if (OnInit != null)
            OnInit();
        rigid = GetComponent<Rigidbody>();
        rigid.mass = 900;
        rigid.drag = 1;
        rigid.angularDrag = 5; //rigidbody setup replaces delayed update. later i would add roll
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.LookAt(player.position);
    }

    private void FixedUpdate()
    {
        rigid.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Obsticle") //TODO XD
            BlowMe();

        if (collision.gameObject.tag == "Player")
            Destroy(collision.gameObject);
    }

    public static event DestroyedAction OnDestroyed; 
    public static event InitialAction OnInit; //sends to statistics for calculating total amount of enemies

    private void BlowMe()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        if (OnDestroyed != null)
            OnDestroyed();
        Destroy(gameObject);
    }
}