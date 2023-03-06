using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public struct ItemData
{
    public int itemCount;
    public StringBuilder itemName;
    public GameObject itemPrefab;
    public Sprite itemImage;
}

public class Item : MonoBehaviour
{
    private ItemData _itemData;
    public ItemData ItemData => _itemData;
    public void Initialize()
    {
        _itemData.itemName = new StringBuilder();
    }
    public bool SetItemData(string itemName, GameObject itemPrefab, Sprite itemImage)
    {
        _itemData.itemName.Append(itemName);
        _itemData.itemPrefab = itemPrefab;
        SetItemImage(itemImage);
        return true;
    }
    public void Click()
    {
        Debug.Log("Click!!");
        UseItem();
    }
    [ContextMenu("k")]
    private void AddItemCount()
    {
        _itemData.itemCount++;
    }


    /*codes*/ //���߿� ItemBehavior��� �θ� Ŭ������ ���� ��, ���.
    private bool SetItemImage(Sprite image)
    {
        _itemData.itemImage = image;

        if (!GetImageObject(out Transform ItemImageObj)) return false;
        if (!ItemImageObj.TryGetComponent(out UnityEngine.UI.Image imageCom)) return false;
        imageCom.sprite = image;

        return true;
    }
    protected bool GetImageObject(out Transform transform)
    {
        transform = this.transform.Find("Image");
        return true;
    }
    protected bool GetNameTextMashObject(out Transform transform)
    {
        transform = this.transform.Find("Item Name");
        return true;
    }
    protected bool GetCountTextMahObject(out Transform transform)
    {
        transform = this.transform.Find("Item Count");
        return true;
    }
    private void UseItem()
    {
        if (IsExistItem())
        {

        }
    }

    private bool IsExistItem()
    {
        return _itemData.itemCount > 0;
    }
}
