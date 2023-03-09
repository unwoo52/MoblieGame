using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBuildPlacementController : MonoBehaviour, ICancelbuild
{
    [SerializeField] private GameObject buildObject;
    private void Start()
    {
        if (!Initialize())
            Debug.Log("Fail Initialize");
    }
    private bool Initialize()
    {

        return true;
    }
    public void CancelBuild()
    {
        Destroy(buildObject);
    }
}