using UnityEngine;
using UnityEngine.UI;
using  DG.Tweening;
public class Waypoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    public Text meter;
    // To adjust the position of the icon
    public Vector3 offset;
    
    public Text pressF;
    public Image progressBar;
    
    public delegate void LandedAction();
    public static event LandedAction OnFinishLanding;

    public static bool isThereEnemiesAround;
    void Start()
    {
        isThereEnemiesAround = true;
        img = GameObject.Find("marker").GetComponent<Image>();
        meter = GameObject.Find("meter").GetComponent<Text>();
        pressF = GameObject.Find("hold f").GetComponent<Text>();
        progressBar = GameObject.Find("progress").GetComponent<Image>();
    }
    private void Update()
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
        if ((int) Vector3.Distance(target.position, transform.position) < 3)
        {
            if (isThereEnemiesAround)
            {
                meter.color = img.color = Color.red;
                pressF.enabled = true;
                pressF.text = "you can not land if there are enemies around";
            }
            else
            {
                meter.color = img.color = Color.green; 
                pressF.text = "hold F tp land";
                Plane.OnLanding = pressF.enabled = true;
                if (Input.GetKey(KeyCode.F))
                {
                    progressBar.DOFillAmount(1, 2).OnComplete(() => { OnLand(); });
                }
            }
        }
        else
        {
            meter.color = img.color =  Color.white;
            Plane.OnLanding = pressF.enabled = false;
        }
    }

    void OnLand()
    {
         if (OnFinishLanding != null)
             OnFinishLanding();
    }
}