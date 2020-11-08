using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    public delegate void DestroyedAction();
    public GameObject ExplosionPrefab;
    public GameObject AfterlifeUI;

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
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }
}