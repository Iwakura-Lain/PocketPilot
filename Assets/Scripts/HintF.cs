using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintF : MonoBehaviour
{
    private Text Ftext;
    private GameObject player;
    private GameObject[] rubics;
    void Start()
    {
        Ftext = GameObject.Find("hold f_pick").GetComponent<Text>();

        player = GameObject.FindWithTag("Player");
        rubics = GameObject.FindGameObjectsWithTag("Rubic");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (rubics[0] && (int) Vector3.Distance(player.transform.position, rubics[0].transform.position) < 5
                || rubics[1] && (int) Vector3.Distance(player.transform.position, rubics[1].transform.position) < 5
                || rubics[2] && (int) Vector3.Distance(player.transform.position, rubics[2].transform.position) < 5
                || rubics[3] && (int) Vector3.Distance(player.transform.position, rubics[3].transform.position) < 5)
            {
                if (Inventory.Full)
                {
                    Ftext.text = "You can carry only one item at once!";
                    Ftext.enabled = true;
                }
                else
                {
                    Ftext.text = "Hold F";
                    Ftext.enabled = true;
                }
            }
            else
            {
                Ftext.enabled = false;
            }
        }
    }
}
