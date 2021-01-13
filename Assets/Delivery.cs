using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Delivery : MonoBehaviour, IInteractable
{
    public Transform player;
    public Animator progressBar;

    public Text holdF;
    public GameObject cubePrefab;
    private bool isFirst = true;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        holdF = GameObject.Find("hold f_deliver").GetComponent<Text>();
        progressBar = GameObject.Find("progress").GetComponent<Animator>();
    }
    void Update()
    {
        if ((int) Vector3.Distance(player.position, transform.position) < 5)
        {
            Messenger.AddListener("OnInteract", Interact);

            if (Inventory.Full)
            {
                holdF.text = "Hold F";
                holdF.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    progressBar.Play("Base Layer.progressBar", 0, 0);
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
        Messenger.RemoveListener("OnInteract", Interact);

        if (isFirst)
        {
            Messenger.Broadcast("StartLanding");
            Messenger.Broadcast("StopLanding");
            isFirst = false;
        }
        Inventory.Full = false;
        Instantiate(cubePrefab);
    }
}
