using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private GameObject _cancelUI;
    [SerializeField] private GameObject _toggleUI;
    [SerializeField] private GameObject _particleObject;

    public GameObject ScrollView { get { return _scrollView; } }
    public GameObject CancelUI { get { return _cancelUI; } }
    public GameObject ToggleUI { get { return _toggleUI; } }

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        FindOnject(ref _scrollView, "Scroll View");
        FindOnject(ref _cancelUI, "Cancel UI");
        FindOnject(ref _toggleUI, "Toggle Swap Button");
    }

    private bool FindOnject(ref GameObject gameobject, string findName)
    {
        if (gameobject != null) return true;
        if (transform.Find(findName) == null) return false;

        gameobject = transform.Find(findName).gameObject;

        return true;
    }
}
