using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Delivery : MonoBehaviour, IInteractable
{
    public Transform player;
    public Image progressBar;
    public Text holdF;
    public GameObject cubePrefab;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        holdF = GameObject.Find("hold f").GetComponent<Text>();
        progressBar = GameObject.Find("progress").GetComponent<Image>();

    }
    void Update()
    {
        if ((int) Vector3.Distance(player.position, transform.position) < 5)
        {
            if (Inventory.Full)
            {
                holdF.text = "Hold F";
                holdF.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Interact();
                }
            }
        }
        else
        {
            holdF.enabled = false;
        }
        
    }

    public void Interact()
    {
        progressBar.DOFillAmount(1, 1).OnComplete(() =>
        {
            progressBar.fillAmount = 0;
            Inventory.Full = false;
            Instantiate(cubePrefab, transform.position, transform.rotation);
        });
    }
}
