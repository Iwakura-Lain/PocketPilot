using UnityEngine;
using UnityEngine.UI;

public class RubicPiece : MonoBehaviour, IInteractable
{
    public Material normal;
    public Material highlighted;
    public Transform player;
    public MeshRenderer myRenderer;
    public Animator progressBar;
    public int myNumber;
    public Text holdF;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        holdF = GameObject.Find("hold f_pick").GetComponent<Text>();
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material = normal;
        progressBar = GameObject.Find("progress").GetComponent<Animator>();
    }

    void Update()
    {
        if ((int) Vector3.Distance(player.position, transform.position) < 5)
        {
            Messenger.AddListener("OnInteract", Interact);

            if (Inventory.Full)
            {
                holdF.text = "You can carry only one item at once!";
                holdF.enabled = true;
            }
            else
            {
                holdF.text = "Hold F";
                myRenderer.material = highlighted;
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
        Inventory.Full = true;
        Messenger.Broadcast("SpawnEnemies", 0); //spawns enemies on a certain set of positions
        Messenger.RemoveListener("OnInteract", Interact);
        Destroy(gameObject);
    }
}
