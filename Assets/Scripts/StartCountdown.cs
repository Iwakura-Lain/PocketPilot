using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public GameObject Vehicles;

    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        text.text = "2";
        yield return new WaitForSeconds(1);
        text.text = "1";
        yield return new WaitForSeconds(1);
        text.text = "Start";
        Vehicles.SetActive(true);
        Destroy(gameObject);
    }
}