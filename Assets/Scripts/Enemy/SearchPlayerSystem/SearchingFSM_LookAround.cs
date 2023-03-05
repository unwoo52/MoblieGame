using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerData
{
    public GameObject Player;
    public float watchedGauge = 0f; //max���� ����, Tracking ����
    public TargetPlayerData(GameObject gameObject)
    {
        Player = gameObject;
    }
}

public class SearchingFSM_LookAround : MonoBehaviour
{
    private AnimContoller _anim;
    private float watchedValue = 5.0f; //�� �� ���� �������� ��

    private float watchedGagueMax = 100.0f;

    private float visionRange = 100.0f;

    [SerializeField] private LayerMask ObstacleMask;

    private List<TargetPlayerData> TargetPlayerList = new List<TargetPlayerData>();
    [SerializeField] private LayerMask TargetLayerMask;

    private AnimMovement animMovement;

    private WaitForSeconds WaitTimeIsLostTarget = new(3.0f);
    //get ray

    private void Start()
    {
        if (!Initialize())
            Debug.LogError("ERROR! ������Ʈ�� ã�� ���߽��ϴ�.");
    }


    private bool Initialize()
    {
        if (TryGetComponent(out AnimContoller animContoller)) _anim = animContoller; else return false;
        return true;
    }

    public void GetRay(Ray ray, GameObject gameObject)
    {
        TargetPlayerData targetPlayerData = FindTargetPlayerData(gameObject);

        AddWeight(ray, targetPlayerData);

        if (targetPlayerData.watchedGauge >= watchedGagueMax)
            StartTracking(gameObject);
    }

    #region .
    private TargetPlayerData FindTargetPlayerData(GameObject gameObject)
    {
        foreach (TargetPlayerData targetPlayerData in TargetPlayerList)
        {
            if (targetPlayerData.Player == gameObject)
            {
                return targetPlayerData;
            }
        }

        return AddTargetPlayerList(gameObject);
    }
    private void AddWeight(Ray ray, TargetPlayerData targetPlayerData)
    {
        if (!IsHitObstacle(ray))
            targetPlayerData.watchedGauge += watchedValue;
    }

    private TargetPlayerData AddTargetPlayerList(GameObject gameobject)
    {
        TargetPlayerData temp = new(gameobject);
        TargetPlayerList.Add(temp);

        return temp;
    }

    private bool IsHitObstacle(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * visionRange, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, visionRange, ObstacleMask))
        {
            return true;
        }

        return false;
    }

    private bool IsPlayerAlreadyInTargetPlayerList(GameObject gameObject, ref int listIndex)
    {
        listIndex = 0;
        foreach (TargetPlayerData targetPlayerData in TargetPlayerList)
        {
            if (targetPlayerData.Player == gameObject)
            {
                return true;
            }
            listIndex++;
        }
        return false;
    }

    #endregion

    public void LostInSight()
    {

    }

    private void StartTracking(GameObject gameObject)
    {

        _anim.OnLookat(gameObject.transform.position, 20);
        _anim.OnScream();
        //lookat

        //scream

        //use nav

        //make del, and if destinated do del
    }

    private void LostTarget()
    {
        //zombie FSM changestate idle
    }
    /*
    IEnumerator ReturnIsTargetLost(GameObject gameobject)
    {
        while (true)
        {
            //gameOb - this pos
            Vector3 dir = gameobject.transform.position - this.transform.position;
            //��� Ÿ�ٰ��� ���̸� Ž��
            Ray ray = new(transform.position, dir);

            if(Physics.Raycast(ray, out RaycastHit hit, zombieSight))

            //if Ÿ���� �ٽ� �������� lost����ġ �ʱ�ȭ
            break;


            //if �ð��� ��� �ʰ��Ǿ��ٸ�
            break;
        }
    }*/

    //Check
    private bool IsPlayerInsight(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, visionRange, TargetLayerMask)) { return true; }
        return false;
    }
}
