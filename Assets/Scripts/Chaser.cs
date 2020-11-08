using System.Collections;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public delegate void DestroyedAction();

    public delegate void InitialAction();

    public float Delay;
    public float speed;

    public GameObject ExplosionPrefab;
    private Coroutine delayedAngleSet;

    private Transform player;
    private Quaternion playerLastRotation;

    private void Start()
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

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        //transform.position = transform.position * bias + Chase * (1 - bias);

        speed -= transform.forward.y * Time.deltaTime * 50;

        if (speed < 35) speed = 35;

        var TerrainHeightWherePlaneAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (TerrainHeightWherePlaneAre > transform.position.y)
            transform.position = new Vector3(transform.position.x, TerrainHeightWherePlaneAre, transform.position.z);
    }

    private void LateUpdate()
    {
        if (player && player.rotation != transform.rotation) UpdateTargetAngle();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Obsticle") //TODO XD
            BlowMe();

        if (collision.gameObject.tag == "Player")
            Destroy(collision.gameObject);
    }

    public static event DestroyedAction OnDestroyed;
    public static event InitialAction OnInit;

    private void UpdateTargetAngle()
    {
        if (delayedAngleSet == null) delayedAngleSet = StartCoroutine(UpdateRotation());
    }

    private IEnumerator UpdateRotation()
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

    private void BlowMe()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        if (OnDestroyed != null)
            OnDestroyed();
        Destroy(gameObject);
    }
}