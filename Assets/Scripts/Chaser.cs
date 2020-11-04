using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public float Delay;
    public float speed;

    public GameObject ExplosionPrefab;

    Transform player;
    private Quaternion playerLastRotation;
    private Coroutine delayedAngleSet;

    public delegate void DestroyedAction();
    public static event DestroyedAction OnDestroyed;    
    
    public delegate void InitialAction();
    public static event InitialAction OnInit;

    void Start()
    {
        if (OnInit != null)
            OnInit();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateTargetAngle();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, playerLastRotation, Time.deltaTime * 2);

    }
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        //transform.position = transform.position * bias + Chase * (1 - bias);

        speed -= transform.forward.y * Time.deltaTime * 50;

        if (speed < 35)
        {
            speed = 35;
        }

        float TerrainHeightWherePlaneAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (TerrainHeightWherePlaneAre > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, TerrainHeightWherePlaneAre, transform.position.z);
        }
    }
    private void LateUpdate()
    {
        if (player && player.rotation!= transform.rotation)
        {
            UpdateTargetAngle();
        }
    }

    private void UpdateTargetAngle()
    {
        if (delayedAngleSet == null)
        {
            delayedAngleSet = StartCoroutine(UpdateRotation());
        }
    }

    IEnumerator UpdateRotation()
    {
        speed -= 1;
        yield return new WaitForSeconds(Delay);
        if (player)
        {
            playerLastRotation = player.rotation;
            speed += 1;
            transform.LookAt(player.position);
            delayedAngleSet = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Obsticle") //TODO XD
            BlowMe();

        if (collision.gameObject.tag == "Player")
            Destroy(collision.gameObject);
    }

    void BlowMe()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        if(OnDestroyed != null)
            OnDestroyed();
        Destroy(gameObject);
    }

}
