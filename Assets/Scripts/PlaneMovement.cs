using UnityEngine;
using UnityEngine.UIElements;

public class PlaneMovement : MonoBehaviour
{
    public delegate void DestroyedAction();

    [SerializeField] private MFlight.MouseFlightController controller = null;

    public float mouseSensitivity;
    public float speed = 20;
    public float turnSpeed = 90;
    public GameObject ExplosionPrefab;

    public GameObject AfterlifeUI;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.LookAt(controller.MouseAimPos);

        speed -= transform.forward.y * Time.deltaTime * 50;

        if (speed < 35) speed = 35;


        var TerrainHeightWherePlaneAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (TerrainHeightWherePlaneAre > transform.position.y)
            transform.position = new Vector3(transform.position.x, TerrainHeightWherePlaneAre, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Enemy")
            BlowMe();
    }

    public static event DestroyedAction OnDestroyed;

    private void BlowMe()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        AfterlifeUI.SetActive(true);
        OnDestroyed();
        Destroy(gameObject);
    }
}