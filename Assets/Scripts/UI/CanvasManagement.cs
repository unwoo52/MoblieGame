using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasManagement : MonoBehaviour
{
    private bool GetInventory(out Transform transform)
    {
        transform = this.transform.parent.Find("Inventory");
        return transform != null;
    }

}
