using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManagement : MonoBehaviour
{
    private List<ItemData> itemList = new();
    private BinaryFormatter binaryFormatter = new();
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject ItemPrefab;

    private void Start()
    {
        AddItem();
        AddItem();
        AddItem();
        AddItem();
        AddItem();
        AddItem();
    }

    /**/
    public string TestString;
    public GameObject TestPrefabs;
    public Sprite TestSprite;
    [ContextMenu("AddItem")]
    public void AddItem()
    {
        Transform scrollviewContentParent = scrollView.transform.GetChild(0).GetChild(0);

        GameObject InstanteObject = Instantiate(ItemPrefab);
        InstanteObject.transform.SetParent(scrollviewContentParent, false);
        InstanteObject.transform.localScale= Vector3.one;

        Item itemScript;
        InstanteObject.TryGetComponent(out itemScript);
        itemScript.Initialize();
        itemScript.SetItemData(TestString,TestPrefabs, TestSprite);
    }
    //-------
    public void SaveBinary<T>(string filePath, T data)
    {
        using (FileStream fileStream = File.Create(filePath))
        {
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }

    }

    public T LoadBinary<T>(string filePath)
    {
        T data = default;
        if (File.Exists(filePath))
        {
            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                data = (T)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
        }
        else
        {
            Debug.LogError(filePath + "파일이 존재하지 않습니다.");
        }
        return data;
    }
}
