using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RubicPiece : MonoBehaviour, IInteractable
{
    public Transform player;
    public Animator progressBar;
    public int myNumber;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        progressBar = GameObject.Find("progress").GetComponent<Animator>();
    }

    void Update()
    {
        if ((int) Vector3.Distance(player.position, transform.position) < 5)
        {
            if (Inventory.Full)
            {
                return;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Messenger.AddListener("OnInteract", Interact);

                    progressBar.Play("Base Layer.progressBar", 0, 0);
                }
            }
        }

    }

     public void Interact()
    {
        Messenger.RemoveListener("OnInteract", Interact);
        Inventory.Full = true;
        Messenger.Broadcast("SpawnEnemies", myNumber); //spawns enemies on a certain set of positions
        Destroy(gameObject);
    }
}
