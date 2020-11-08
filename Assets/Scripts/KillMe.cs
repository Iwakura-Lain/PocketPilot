using UnityEngine;

public class KillMe : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 4);
    }
}