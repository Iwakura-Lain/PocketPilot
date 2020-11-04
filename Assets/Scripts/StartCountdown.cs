using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StartCountdown : MonoBehaviour
{
    public GameObject Vehicles;

    Text text;
    void Start()
    {
        text = GetComponentInChildren<Text>();
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    IEnumerator Countdown()
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
