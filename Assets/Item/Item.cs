using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int itemCount = 0;

    public void Click()
    {
        Debug.Log("Click!!");
        UseItem();
    }

    private void UseItem()
    {
        //IsExistItem()
    }

    private bool IsExistItem()
    {
        return false;
    }
}
