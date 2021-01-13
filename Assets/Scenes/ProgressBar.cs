using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public void OnFilled()
    {
        Messenger.MarkAsPermanent("OnInteract");
        Messenger.Broadcast("OnInteract");
    }

}
