using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM_Base : MonoBehaviour
{

    [SerializeField] protected ZombieFSM _zombieFSM;
    private void Start()
    {
        if (!Initialized())
            Debug.LogError("Init() ����! ������Ʈ�� ã�� ���߽��ϴ�.");
    }
    private bool Initialized()
    {
        if (TryGetComponent(out ZombieFSM zombieFSM)) _zombieFSM = zombieFSM; else return false;

        return true;
    }
}
