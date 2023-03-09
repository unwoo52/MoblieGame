using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetCanvas
{
    bool GetCanvas(ref GameObject gameObject);
}
public class CanvasManagement : MonoBehaviour, IGetCanvas
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _quickSlot;
    [SerializeField] private GameObject _variableJoystick;
    [SerializeField] private GameObject _canvas;
    #region singleton
    private static CanvasManagement _instance = null;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static CanvasManagement Instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }
    #endregion
    public bool GetInventory(out Transform transform)
    {
        if (null != _inventory)
            transform = _inventory.transform;
        else
            transform = this.transform.parent.Find("Inventory");
        return transform != null;
    }
    public bool GetQuickSlot(out Transform transform)
    {
        if (null != _quickSlot)
            transform = _quickSlot.transform;
        else
            transform = this.transform.parent.Find("QuickSlot");
        return transform != null;
    }
    public bool GetVariableJoystick(out Transform transform)
    {
        if (null != _variableJoystick)
            transform = _variableJoystick.transform;
        else
            transform = this.transform.parent.Find("Variable Joystick");
        return transform != null;
    }

    public bool GetCanvas(ref GameObject gameObject)
    {
        gameObject = _canvas;
        if (gameObject == null) gameObject = transform.parent.gameObject;
        return gameObject != null;
    }
}