using UnityEngine;
using UnityEngine.UI;
using  DG.Tweening;

public class Waypoint : MonoBehaviour, IInteractable
{
    public Image img;
    public Transform target;
    public Transform deliveryPoint;
    public Text meter;
    // To adjust the position of the icon
    public Vector3 offset;
    
    public Animator progressBar;
    public Plane plane;

    public static bool isThereEnemiesAround;
    private  bool F_pressed;
   protected virtual void Start()
    {
        plane = FindObjectOfType<Plane>();
        Messenger.AddListener<Transform>("NewTarget", UpdateTarget);
        img = GameObject.Find("marker").GetComponent<Image>();
        meter = GameObject.Find("meter").GetComponent<Text>();
        progressBar = GameObject.Find("progress").GetComponent<Animator>();
        Messenger.AddListener("FirstPieceIsDelivered", TurnOff);
    }

   private void TurnOff()
   {
       this.enabled = false;
   }

   private void UpdateTarget(Transform newTarget)
    {
        target = newTarget;
    }
    protected virtual void Update()
    {
        // Giving limits to the icon so it sticks on the screen
        // Below calculations witht the assumption that the icon anchor point is in the middle
        // Minimum X position: half of the icon width
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        // Minimum Y position: half of the height
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        // Temporary variable to store the converted position from 3D world point to 2D screen point
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Check if the target is behind us, to only show the icon once the target is in front
        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            // Check if the target is on the left side of the screen
            if(pos.x < Screen.width / 2)
            {
                // Place it on the right (Since it's behind the player, it's the opposite)
                pos.x = maxX;
            }
            else
            {
                // Place it on the left side
                pos.x = minX;
            }
        }
        // Limit the X and Y positions
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        
        meter.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
        if ((int) Vector3.Distance(target.position, transform.position) < 5)
        {
            plane.OnLanding = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Messenger.AddListener("OnInteract", Interact);
                    progressBar.enabled = true;
                   // progressBar.Play("Base Layer.progressBar", 0, 0);
                }
        }
    }

    public void Interact()
    {
        Messenger.RemoveListener("OnInteract", Interact);
        
        print("Interact, waypoint");

        UpdateTarget(deliveryPoint);
        Messenger.Broadcast("CargoTaken");
        Messenger.Broadcast("SpawnEnemies", 0);
    }
}