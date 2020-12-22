using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointPlay : Waypoint
{
    public Transform[] targets;
    public int currentTarget = 0;

    protected override void Start()
    {
        target = targets[0];
        base.Start();
    }
    private void UpdateTarget()
    {
        currentTarget++; 
        if(currentTarget < targets.Length)
            target = targets[currentTarget];
        else
        {
            
            return;
        }
    }
    protected override void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if(pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        
        meter.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
        if ((int) Vector3.Distance(target.position, transform.position) < 1)
        {
            meter.color = img.color = Color.green; 
                UpdateTarget();
        }
        else 
        {
            meter.color = img.color =  Color.white;
        }
        
    }
    
}

