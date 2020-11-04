using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float speed = 20;
    public float turnSpeed = 90;
    public GameObject ExplosionPrefab;
    public GameObject AfterlifeUI;

    public delegate void DestroyedAction();
    public static event DestroyedAction OnDestroyed;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 MoveCam = transform.position - transform.forward * 10 * transform.localScale.x + Vector3.up * 2 * transform.localScale.x;
        float bias = 0.65f;
        Camera.main.transform.position = Camera.main.transform.position * bias + MoveCam * (1 - bias);
        Camera.main.transform.LookAt(transform.position);

        transform.position += transform.forward * Time.deltaTime * speed;

        if (Input.GetAxis("Horizontal") > 0.0)
        { //if the right arrow is pressed
            transform.Rotate(0, turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, Space.World); //and then turn the plane
        }
        if (Input.GetAxis("Horizontal") < 0.0)
        { //if the left arrow is pressed
            transform.Rotate(0, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, Space.World); //The X-rotation turns the plane - the Z-rotation tilts it
        }
        if (Input.GetAxis("Vertical") > 0.0)
        { //if the up arrow is pressed
            transform.Rotate(turnSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetAxis("Vertical") < 0.0)
        { //if the down arrow is pressed
            transform.Rotate(-turnSpeed * Time.deltaTime, 0, 0);
        }

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
    void BlowMe()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        AfterlifeUI.SetActive(true);
        OnDestroyed();
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Enemy")
            BlowMe();
    }
}
