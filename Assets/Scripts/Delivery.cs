using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Delivery : MonoBehaviour, IInteractable
{
    public Transform player;
    public Animator progressBar;
    public Animator TeenDoor;
    public GameObject LevelCompleteAnim;
    public Text f_deliver;
    private bool isFirst = true;

    public MeshRenderer[] firstSet;
    public MeshRenderer[] secondSet;
    public MeshRenderer[] thirdSet;
    public MeshRenderer[] fourthSet;
    public Material[] rubicColors;
    private int pieceNumber = 0;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        f_deliver = GameObject.Find("hold f_deliver").GetComponent<Text>();
        progressBar = GameObject.Find("progress").GetComponent<Animator>();
    }
    void Update()
    {
        if (player && (int) Vector3.Distance(player.position, transform.position) < 5)
        {
            if (Inventory.Full)
            {
                f_deliver.text = "Hold F";
                f_deliver.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Messenger.AddListener("OnInteract", Interact);
                    progressBar.Play("Base Layer.progressBar", 0, 0);
                }
            }
        }
        else
        {
            f_deliver.enabled = false;
        }
        
    }

    public void Interact()
    {
        Messenger.RemoveListener("OnInteract", Interact);
        print("Interact, delivery");
        Inventory.Full = false;
        pieceNumber++;
        colouringCube(pieceNumber);
    }

    private void colouringCube(int setNumber)
    {
        switch (setNumber)
        {
            case 1:
                Messenger.Broadcast("StartLanding");
                Messenger.Broadcast("StopLanding");
                TeenDoor.enabled = true;
                isFirst = false;
                foreach (var rend in firstSet)
                {
                    rend.material = rubicColors[Random.Range(0, rubicColors.Length)];
                }

                break;
            case 2:
                foreach (var rend in secondSet)
                {
                    rend.material = rubicColors[Random.Range(0, rubicColors.Length)];
                }

                break;
            case 3:
                foreach (var rend in thirdSet)
                {
                    rend.material = rubicColors[Random.Range(0, rubicColors.Length)];
                }

                break;
            case 4:
                foreach (var rend in fourthSet)
                {
                    rend.material = rubicColors[Random.Range(0, rubicColors.Length)];
                }
                LevelCompleteAnim.SetActive(true);
                LevelsManager.isMainLevelCompleted = true;
                break;
        }
    }
}
